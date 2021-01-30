using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetRoleList(CancellationToken cancellationToken)
        {
            var roles = await Mediator.Send(new GetRoleListQuery(),cancellationToken);

            return Ok(roles);
        }
    }
}
