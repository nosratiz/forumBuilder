using FluentValidation;
using FourmBuilder.Api.Core.Application.Forums.Command.UpdateForums;

namespace FourmBuilder.Api.Core.Validator.Forums
{
    public class UpdateForumCommandValidator : AbstractValidator<UpdateForumCommand>
    {
        public UpdateForumCommandValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty().NotNull();

            RuleFor(dto => dto.Name).NotEmpty().NotNull();
        }
    }
}