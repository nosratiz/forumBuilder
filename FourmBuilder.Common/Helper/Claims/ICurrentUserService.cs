namespace FourmBuilder.Common.Helper.Claims
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string FullName { get; }
        bool IsAuthenticated { get; }
        string RoleName { get; }
        public string OrganizationId { get; set; }
    }
}