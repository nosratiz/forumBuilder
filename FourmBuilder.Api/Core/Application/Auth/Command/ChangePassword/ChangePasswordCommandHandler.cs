using System;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Auth.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IMongoRepository<User> userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Id == Guid.Parse(_currentUserService.UserId), cancellationToken);

            if (user is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            if (PasswordManagement.CheckPassword(request.CurrentPassword, user.Password) == false)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.WrongPassword)));

            if (request.CurrentPassword == request.NewPassword)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.PasswordUsedBefore)));

            user.Password = PasswordManagement.HashPass(request.NewPassword);

            await _userRepository.UpdateAsync(user);


            return Result.SuccessFul();
        }
    }
}