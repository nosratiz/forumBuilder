using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FourmBuilder.Common.Helper.Claims
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            IsAuthenticated = UserId != null;
            FullName = httpContextAccessor.HttpContext?.User?.FindFirstValue("fullName");
            RoleName = httpContextAccessor.HttpContext?.User?.FindFirstValue("RoleName");
            OrganizationId =(httpContextAccessor.HttpContext?.User?.FindFirstValue("organizationId"));
        }

        public string UserId { get; }
        public string FullName { get; }
        public string RoleName { get; set; }
        public string OrganizationId { get; set; }
        public bool IsAuthenticated { get; }
    }
}