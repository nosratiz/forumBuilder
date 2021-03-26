using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Answers.Command;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    public class AnswersController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> SendAnswers(UpdateAnswersCommand updateAnswersCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateAnswersCommand, cancellationToken);

            if (result.Success==false)
            {
                return result.ApiResult;
            }

            return NoContent();
        }
        


    }
}