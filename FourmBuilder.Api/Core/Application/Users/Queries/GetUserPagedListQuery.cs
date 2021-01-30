using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Users.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Helper.Pagination;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Types;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Users.Queries
{
    public class GetUserPagedListQuery : PagingOptions, IRequest<PagedResult<UserDto>>
    {
        public UserType? UserType { get; set; }

        public Guid? RoleId { get; set; }
    }


    public class GetUserPagedListQueryHandler : IRequestHandler<GetUserPagedListQuery, PagedResult<UserDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserPagedListQueryHandler(IMongoRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDto>> Handle(GetUserPagedListQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetAll(x => x.IsDelete == false);

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                users = users.Where(x => x.FirstName.Contains(request.Query) ||
                                         x.LastName.Contains(request.Query) ||
                                         x.Email.Contains(request.Query) ||
                                         x.Mobile == request.Query);
            }

            if (request.UserType.HasValue)
                users = users.Where(x => x.UserType == request.UserType);

            if (request.RoleId.HasValue)
                users = users.Where(x => x.Role.Id == request.RoleId);
            
            

            var userList = await _userRepository.BrowseAsync(users, request);

            
            return _mapper.Map<PagedResult<UserDto>>(userList);
        }
    }
}