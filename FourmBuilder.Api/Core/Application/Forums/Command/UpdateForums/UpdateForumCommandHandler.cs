using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Forums.Command.UpdateForums
{
    public class UpdateForumCommandHandler : IRequestHandler<UpdateForumCommand, Result>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public UpdateForumCommandHandler(IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateForumCommand request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.Id, cancellationToken);

            if (fourm is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            _mapper.Map(request, fourm);

            await _forumRepository.UpdateAsync(fourm);

            return Result.SuccessFul();
        }
    }
}