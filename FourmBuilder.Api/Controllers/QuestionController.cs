using System;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Forums.Command.UpdateForums;
using FourmBuilder.Api.Core.Application.Questions.Command.CreateQuestion;
using FourmBuilder.Api.Core.Application.Questions.Command.DeleteQuestions;
using FourmBuilder.Api.Core.Application.Questions.Command.UpdateQuestion;
using FourmBuilder.Api.Core.Application.Questions.Queries;
using FourmBuilder.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    public class QuestionController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionCommand createQuestionCommand)
        {
            var result = await Mediator.Send(createQuestionCommand);

            if (result.Success == false)
                return result.ApiResult;

            return Ok(result.Data);
        }

        [HttpPut("Remove")]
        public async Task<IActionResult> RemoveQuestion(DeleteQuestionCommand deleteQuestionCommand)
        {
            var result = await Mediator.Send(deleteQuestionCommand);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateForumOptions(UpdateQuestionCommand updateQuestionCommand)
        {
            var result = await Mediator.Send(updateQuestionCommand);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetQuestionQuery {Id = id});

            return result.ApiResult;
        }
    }
}