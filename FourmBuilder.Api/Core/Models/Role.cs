using System;
using FourmBuilder.Common.Types;

namespace FourmBuilder.Api.Core.Models
{
    public class Role : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVital { get; set; }
        public bool IsDeleted { get; set; }


        public static string Admin = "Admin";

        public static string Manager = "Manager";

        public static string User = "User";

  
    }
}