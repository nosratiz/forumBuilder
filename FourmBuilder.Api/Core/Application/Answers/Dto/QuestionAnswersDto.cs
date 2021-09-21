using System;

namespace FourmBuilder.Api.Core.Application.Answers.Dto
{
    public class QuestionAnswersDto
    {
        public Guid QuestionId { get; set; }

        public string Answers { get; set; }
    }
}