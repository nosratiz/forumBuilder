using FluentValidation;
using FourmBuilder.Api.Core.Application.Auth.Command.ChangePassword;

namespace FourmBuilder.Api.Core.Validator.Auth
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(dto => dto.CurrentPassword)
                .NotNull()
                .NotEmpty();


            RuleFor(dto => dto.NewPassword)
                .NotNull().NotEmpty().MinimumLength(3);
        }
    }
}