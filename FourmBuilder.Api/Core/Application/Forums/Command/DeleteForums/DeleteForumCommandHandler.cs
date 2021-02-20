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

namespace FourmBuilder.Api.Core.Application.Forums.Command.DeleteForums
{
    public class DeleteForumCommandHandler : IRequestHandler<DeleteForumCommand, Result>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteForumCommandHandler(IMongoRepository<Forum> forumRepository,
            ICurrentUserService currentUserService)
        {
            _forumRepository = forumRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteForumCommand request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.Id, cancellationToken);

            if (fourm is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            if (fourm.UserId != Guid.Parse(_currentUserService.UserId) || _currentUserService.RoleName != Role.Admin)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.DeleteAccessDenied)));

            await _forumRepository.DeleteAsync(fourm.Id);

            return Result.SuccessFul();
        }
    }
}