using System;
using FourmBuilder.Common.Enum;

namespace FourmBuilder.Api.Core.Application.Users.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public string Mobile { get; set; }
        public string StudentNumber { get; set; }
        public UserType UserType { get; set; }

        public string RoleName { get; set; }
        

        public bool IsEmailConfirm { get; set; }
        public bool IsMobileConfirm { get; set; }

        public DateTime RegisterDate { get; set; }

    }
}