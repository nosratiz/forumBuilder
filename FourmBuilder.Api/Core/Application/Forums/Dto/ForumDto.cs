using System;
using System.Collections.Generic;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Enum;

namespace FourmBuilder.Api.Core.Application.Forums.Dto
{
    public class ForumDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public bool IsActive { get; set; }

        public string Link { get; set; }

        public DateTime CreateDate { get; set; }

        public List<ForumQuestionDto> ForumQuestions { get; set; }
    }

    public class ForumQuestionDto
    {
        public Guid Id { get; set; }

        public int QuestionNumber { get; set; }

        public string Question { get; set; }

        public ForumType ForumType { get; set; }

        public bool IsRequired { get; set; }

        public string RawData { get; set; }

        public List<ForumOptionsDto> Options { get; set; }

    }

    public class ForumOptionsDto
    {
        public Guid Id { get; set; }

        public string Options { get; set; }
    }
}