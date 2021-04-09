using System;
using System.Collections.Generic;
using FourmBuilder.Api.Core.Answers.Dto;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Answers.Command
{
    public class UpdateAnswersCommand : IRequest<Result>
    {
        public Guid ForumId { get; set; }

        public List<QuestionAnswersDto> QuestionAnswers { get; set; }
    }
}       