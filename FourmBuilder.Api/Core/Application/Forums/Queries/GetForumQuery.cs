using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Forums.Queries
{
    public class GetForumQuery : IRequest<Result<ForumDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetForumQueryHandler : IRequestHandler<GetForumQuery, Result<ForumDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetForumQueryHandler(IMongoRepository<Forum> forumRepository, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }


        public async Task<Result<ForumDto>> Handle(GetForumQuery request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.Id, cancellationToken);

            if (fourm is null)
                return Result<ForumDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            return Result<ForumDto>.SuccessFul(_mapper.Map<ForumDto>(fourm));
        }
    }
}