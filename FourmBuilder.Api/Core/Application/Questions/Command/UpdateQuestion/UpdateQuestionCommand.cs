using System;
using System.Collections.Generic;
using FourmBuilder.Api.Core.Application.Questions.Dto;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Questions.Command.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<Result>
    {
        public Guid ForumId { get; set; }

        public Guid QuestionId { get; set; }

        public string Question { get; set; }

        public ForumType ForumType { get; set; }

        public bool IsRequired { get; set; }
        public string RawData { get; set; }
        public List<CreateOptionDto> Answers { get; set; }
    }
}