using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Auth.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }


}