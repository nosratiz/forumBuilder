using System;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Forums.Command.Activation
{
    public class ActivationForumCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}