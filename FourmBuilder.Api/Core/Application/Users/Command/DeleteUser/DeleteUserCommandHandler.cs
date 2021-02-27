using System;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Users.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(IMongoRepository<User> userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id, cancellationToken);

            if (user is null)
                return Result.Failed(
                    new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            if (user.Role.Name == Role.Admin)
                return Result.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.DeleteUserAdminNotAllowed)));

            var currentUser = await _userRepository.GetAsync(Guid.Parse(_currentUserService.UserId), cancellationToken);

            user.IsDelete = true;

            await _userRepository.UpdateAsync(user);

            return Result.SuccessFul();
        }
    }
}