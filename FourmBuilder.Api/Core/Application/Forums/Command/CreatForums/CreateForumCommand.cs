using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Common.Result;
using MediatR;
using NotImplementedException = System.NotImplementedException;

namespace FourmBuilder.Api.Core.Application.Forums.Command.CreatForums
{
    public class CreateForumCommand : IRequest<Result<ForumListDto>>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public bool IsPublic { get; set; }
    }
}