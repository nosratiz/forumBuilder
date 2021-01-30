using System;

namespace FourmBuilder.Api.Core.Application.Users.Dto
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Guid UserId { get; set; }
    }
}