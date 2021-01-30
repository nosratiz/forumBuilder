using FourmBuilder.Api.Core.Application.Auth.Dto;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Auth.Command.LoginCommand
{
    public class LoginCommand : IRequest<Result<TokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}