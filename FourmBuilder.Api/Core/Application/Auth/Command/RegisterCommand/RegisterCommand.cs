using FourmBuilder.Api.Core.Application.Auth.Dto;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Auth.Command.RegisterCommand
{
    public class RegisterCommand : IRequest<Result<TokenDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string StudentNumber { get; set; }

        public string Mobile { get; set; }
        
        public UserType UserType { get; set; }
        public string Password { get; set; }
    }
}