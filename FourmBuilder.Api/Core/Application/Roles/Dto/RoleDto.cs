using System;

namespace FourmBuilder.Api.Core.Application.Roles.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}