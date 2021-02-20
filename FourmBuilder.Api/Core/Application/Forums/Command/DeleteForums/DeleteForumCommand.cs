using System;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Forums.Command.DeleteForums
{
    public class DeleteForumCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}