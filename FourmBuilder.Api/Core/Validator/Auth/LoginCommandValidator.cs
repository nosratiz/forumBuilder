using FluentValidation;
using FourmBuilder.Api.Core.Application.Auth.Command.LoginCommand;

namespace FourmBuilder.Api.Core.Validator.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {

            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(dto => dto.Password).NotNull().NotEmpty();
        }
    }
}