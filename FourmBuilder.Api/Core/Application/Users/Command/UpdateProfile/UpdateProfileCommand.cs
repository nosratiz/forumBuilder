using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Users.Command.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public string Avatar { get; set; }
    }
}