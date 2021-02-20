using FluentValidation;
using FourmBuilder.Api.Core.Application.Forums.Command.CreatForums;

namespace FourmBuilder.Api.Core.Validator.Forums
{
    public class CreateForumCommandValidator : AbstractValidator<CreateForumCommand>
    {
        public CreateForumCommandValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull();

            RuleFor(dto => dto.Title).NotEmpty().NotNull();

             RuleFor(dto => dto.Description).NotEmpty().NotNull();
        }
    }
}