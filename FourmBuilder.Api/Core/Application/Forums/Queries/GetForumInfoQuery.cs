using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Forums.Queries
{
    public class GetForumInfoQuery : IRequest<Result<ForumDto>>
    {
        public Guid Id { get; set; }
    }
    
    public class GetForumInfoQueryHandler : IRequestHandler<GetForumInfoQuery,Result<ForumDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public GetForumInfoQueryHandler(IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result<ForumDto>> Handle(GetForumInfoQuery request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.Id, cancellationToken);

            if (fourm is null)
                return Result<ForumDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            return Result<ForumDto>.SuccessFul(_mapper.Map<ForumDto>(fourm));
        }
    }
}