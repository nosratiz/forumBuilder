using System;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Types;

namespace FourmBuilder.Api.Core.Models
{
    public class User : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ActiveCode { get; set; }
        public string Mobile { get; set; }

        public string StudentNumber { get; set; }

        public bool IsEmailConfirm { get; set; }
        public bool IsMobileConfirm { get; set; }
        public bool IsDelete { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ExpiredCode { get; set; }
        
        public UserType UserType { get; set; }

        public virtual Role Role { get; set; }
        
    }
}