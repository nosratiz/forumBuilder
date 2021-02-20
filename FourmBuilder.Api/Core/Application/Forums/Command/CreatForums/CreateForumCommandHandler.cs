using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Forums.Command.CreatForums
{
    public class CreateForumCommandHandler : IRequestHandler<CreateForumCommand, Result<ForumListDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateForumCommandHandler(IMongoRepository<Forum> forumRepository, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<ForumListDto>> Handle(CreateForumCommand request, CancellationToken cancellationToken)
        {
            var forum = _mapper.Map<Forum>(request);

            forum.UserId = Guid.Parse(_currentUserService.UserId);

            await _forumRepository.AddAsync(forum);

            return Result<ForumListDto>.SuccessFul(_mapper.Map<ForumListDto>(forum));
        }
    }
}