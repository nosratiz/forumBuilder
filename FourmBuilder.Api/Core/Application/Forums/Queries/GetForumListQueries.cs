using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.Pagination;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Types;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Forums.Queries
{
    public class GetForumListQueries : PagingOptions, IRequest<PagedResult<ForumListDto>>
    {
    }

    public class GetForumListQueriesHandler : IRequestHandler<GetForumListQueries, PagedResult<ForumListDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;


        public GetForumListQueriesHandler(IMongoRepository<Forum> forumRepository, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<ForumListDto>> Handle(GetForumListQueries request, CancellationToken cancellationToken)
        {
            var forums = _forumRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Query))
                forums = forums.Where(x => x.Name.Contains(request.Query));

            if (Role.User == _currentUserService.RoleName)
                forums = forums.Where(x => x.UserId == Guid.Parse(_currentUserService.UserId));
            
            var ForumList = await _forumRepository.BrowseAsync(forums, request);
            
            return _mapper.Map<PagedResult<ForumListDto>>(ForumList);
            
        }
    }
}