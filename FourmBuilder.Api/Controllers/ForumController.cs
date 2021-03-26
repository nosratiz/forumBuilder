using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Answers.Queries;
using FourmBuilder.Api.Core.Application.Forums.Command.CreatForums;
using FourmBuilder.Api.Core.Application.Forums.Command.DeleteForums;
using FourmBuilder.Api.Core.Application.Forums.Command.UpdateForums;
using FourmBuilder.Api.Core.Application.Forums.Queries;
using FourmBuilder.Api.Core.Excel;
using FourmBuilder.Common.Helper.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    public class ForumController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions)
            => Ok(await Mediator.Send(new GetForumListQueries
            {
                Page = pagingOptions.Page,
                Limit = pagingOptions.Limit,
                Query = pagingOptions.Query
            }));


        [HttpGet("{id}", Name = "GetForumInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            var result = await Mediator.Send(new GetForumInfoQuery {Id = id});

            return result.ApiResult;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateForumCommand createForumCommand)
        {
            var result = await Mediator.Send(createForumCommand);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetForumInfo", new {id = result.Data.Id}), result.Data);
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateForumCommand updateForumCommand)
        {
            var result = await Mediator.Send(updateForumCommand);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteForumCommand {Id = id});

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpGet("ForumAnswerCount/{id}")]
        public async Task<IActionResult> GetAnswerCount(Guid id, CancellationToken cancellationToken)
            => Ok(new {count = await Mediator.Send(new GetForumAnswerCountQuery {ForumId = id}, cancellationToken)});


        [HttpGet("ExcelReport/{id}")]
        public async Task<IActionResult> GetExcelReport(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetForumInfoQuery {Id = id}, cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            var UserResponse = await Mediator.Send(new GetUserResponseListQuery {ForumId = id}, cancellationToken);

            var url = ReportGenerator.GenerateForumAnswers(UserResponse, result.Data
                .ForumQuestions.Select(x => x.Question).ToList());

            return Ok(new {link = url});
        }
        
        
    }
}