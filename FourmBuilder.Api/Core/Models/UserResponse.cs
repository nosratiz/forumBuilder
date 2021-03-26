using System;
using System.Collections.Generic;
using FourmBuilder.Common.Types;

namespace FourmBuilder.Api.Core.Models
{
    public class UserResponse : IIdentifiable
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; }

        public virtual Forum Forum { get; set; }

        public List<string> Answers { get; set; }

        public DateTime CreateDate { get; set; }
    }
}