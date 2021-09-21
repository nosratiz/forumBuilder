using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Forums.Command.Activation
{
    public class ActivationForumCommandHandler : IRequestHandler<ActivationForumCommand,Result>
    {
        private readonly IMongoRepository<Forum> _forumRepository;

        public ActivationForumCommandHandler(IMongoRepository<Forum> forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public async Task<Result> Handle(ActivationForumCommand request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.Id, cancellationToken);

            if (fourm is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            fourm.IsActive = !fourm.IsActive;

           await _forumRepository.UpdateAsync(fourm);
            
            return Result.SuccessFul();
        }
    }
}