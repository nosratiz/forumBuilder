using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Auth.Command.ChangePassword;
using FourmBuilder.Api.Core.Application.Auth.Command.LoginCommand;
using FourmBuilder.Api.Core.Application.Auth.Command.RegisterCommand;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateProfile;
using FourmBuilder.Api.Core.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    public class AccountController : BaseController
    {


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(loginCommand,cancellationToken);

            return result.ApiResult;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(registerCommand,cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Ok(result.Data);
        }


  
        

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(changePasswordCommand,cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }

        

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAccountInfoQuery(),cancellationToken);

            return result.ApiResult;
        }

        [Authorize]
        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommand updateProfileCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateProfileCommand,cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }
    }
}
