using System;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Users.Command.CreateUser;
using FourmBuilder.Api.Core.Application.Users.Command.DeleteUser;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateUser;
using FourmBuilder.Api.Core.Application.Users.Queries;
using FourmBuilder.Common.Helper.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetUserList([FromQuery] PagingOptions pagingOptions,CancellationToken cancellationToken)
            => Ok(await Mediator.Send(new GetUserPagedListQuery
            {
                Page = pagingOptions.Page,
                Limit = pagingOptions.Limit,
                Query = pagingOptions.Query
            },cancellationToken));


        [HttpGet("{id}", Name = "UserInfo")]
        public async Task<IActionResult> Get(Guid id,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserQuery { Id = id },cancellationToken);

            return result.ApiResult;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(createUserCommand,cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("UserInfo", new { id = result.Data.Id }), result.Data);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteUserCommand { Id = id },cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateUserCommand,cancellationToken);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();
        }


        [HttpGet("{id}/ForumList")]
        public async Task<IActionResult> GetForum(Guid id,[FromQuery] PagingOptions pagingOptions)
        {
            var result = await Mediator.Send(new GetUserForumPagedListQuery
            {
                UserId = id,
                Page = pagingOptions.Page,
                Limit = pagingOptions.Limit,
                Query = pagingOptions.Query
            });

            return result.ApiResult;
        }



    }
}
