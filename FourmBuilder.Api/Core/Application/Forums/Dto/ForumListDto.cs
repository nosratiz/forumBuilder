using System;

namespace FourmBuilder.Api.Core.Application.Forums.Dto
{
    public class ForumListDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public bool IsActive { get; set; }

        public string Link { get; set; }

        public DateTime CreateDate { get; set; }
    }
}