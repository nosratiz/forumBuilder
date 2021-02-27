using System;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Users.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
   
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string StudentNumber { get; set; }

        public UserType UserType { get; set; }
        public string Mobile { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}