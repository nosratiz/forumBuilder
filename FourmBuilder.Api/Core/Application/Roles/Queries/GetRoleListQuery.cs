using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Roles.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Mongo;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Roles.Queries
{
    public class GetRoleListQuery : IRequest<List<RoleDto>>
    {

    }

    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, List<RoleDto>>
    {
        private readonly IMongoRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleListQueryHandler(IMongoRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.FindAsync(x => x.IsDeleted == false, cancellationToken);

            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}