using System;
using System.Collections.Generic;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Types;

namespace FourmBuilder.Api.Core.Models
{
    public class Forum : IIdentifiable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public bool IsActive { get; set; }

        public string Link { get; set; }

        public DateTime CreateDate { get; set; }

        public List<ForumQuestion> ForumQuestions { get; set; }
    }

    public class ForumQuestion
    {
        public Guid Id { get; set; }

        public int QuestionNumber { get; set; }

        public string Question { get; set; }

        public ForumType ForumType { get; set; }

        public bool IsRequired { get; set; }

        public string RawData { get; set; }
        public List<ForumOptions> Options { get; set; }
    }

    public class ForumOptions
    {
        public Guid Id { get; set; }

        public string Options { get; set; }
    }
}