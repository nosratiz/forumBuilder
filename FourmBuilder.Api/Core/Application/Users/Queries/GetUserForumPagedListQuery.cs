using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Pagination;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using FourmBuilder.Common.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Users.Queries
{
    public class GetUserForumPagedListQuery : PagingOptions, IRequest<Result<PagedResult<ForumDto>>>
    {
        public Guid UserId { get; set; }
    }

    public class
        GetUserForumPagedListQueryHandler : IRequestHandler<GetUserForumPagedListQuery, Result<PagedResult<ForumDto>>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public GetUserForumPagedListQueryHandler(IMongoRepository<User> userRepository,
            IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<ForumDto>>> Handle(GetUserForumPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                return Result<PagedResult<ForumDto>>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));
            }

            var forums = _forumRepository.GetAll(x => x.UserId == request.UserId);
            
            if (!string.IsNullOrWhiteSpace(request.Query))
                forums = forums.Where(x => x.Name.Contains(request.Query));

            var forumList = await _forumRepository.BrowseAsync(forums, request);

            return Result<PagedResult<ForumDto>>
                .SuccessFul(_mapper.Map<PagedResult<ForumDto>>(forumList));
        }
    }
}