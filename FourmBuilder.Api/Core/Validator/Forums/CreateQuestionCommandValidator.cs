using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Questions.Command.CreateQuestion;
using FourmBuilder.Common.Enum;

namespace FourmBuilder.Api.Core.Validator.Forums
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto => dto.Question).NotEmpty().NotNull();

            RuleFor(dto => dto.ForumId).NotEmpty().NotNull();

            RuleFor(dto => dto.ForumType).NotEmpty().NotNull();

            RuleFor(dto => dto)
                .Must(ValidOptionsQuestion).WithMessage("Answer object should not null or empty");
        }

        private bool ValidOptionsQuestion(CreateQuestionCommand createQuestionCommand)
        {
            if (createQuestionCommand.ForumType >= ForumType.MultiChoice)
                if (createQuestionCommand.Answers == null || createQuestionCommand.Answers.Any() == false)
                    return false;

            return true;
        }

        private bool ValidDropDownQuestion(CreateQuestionCommand createQuestionCommand)
        {
            if (createQuestionCommand.ForumType == ForumType.Image ||
                createQuestionCommand.ForumType == ForumType.DropDown)
                if (string.IsNullOrWhiteSpace(createQuestionCommand.RawData))
                    return false;

            return true;
        }
    }
}