using System;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Forums.Command.UpdateForums
{
    public class UpdateForumCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public bool IsPublic { get; set; }
        
        public bool IsActive { get; set; }
    }
}