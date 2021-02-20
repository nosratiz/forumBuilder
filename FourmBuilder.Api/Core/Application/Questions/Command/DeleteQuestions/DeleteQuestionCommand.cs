using System;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Questions.Command.DeleteQuestions
{
    public class DeleteQuestionCommand : IRequest<Result>
    {
        public Guid ForumId { get; set; }

        public Guid QuestionId { get; set; }
    }
}