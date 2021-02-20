using System.Linq;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Questions.Command.UpdateQuestion;
using FourmBuilder.Common.Enum;

namespace FourmBuilder.Api.Core.Validator.Forums
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.QuestionId).NotEmpty().NotNull();

            RuleFor(dto => dto.Question).NotEmpty().NotNull();

            RuleFor(dto => dto.ForumId).NotEmpty().NotNull();

            RuleFor(dto => dto.ForumType).NotEmpty().NotNull();


            RuleFor(dto => dto)
                .Must(ValidOptionsQuestion).WithMessage("Answer object should not null or empty");
        }

        private bool ValidOptionsQuestion(UpdateQuestionCommand updateQuestionCommand)
        {
            if (updateQuestionCommand.ForumType >= ForumType.MultiChoice)
                if (updateQuestionCommand.Answers == null || updateQuestionCommand.Answers.Any() == false)
                    return false;

            return true;
        }

        private bool ValidDropDownQuestion(UpdateQuestionCommand updateQuestionCommand)
        {
            if (updateQuestionCommand.ForumType == ForumType.Image ||
                updateQuestionCommand.ForumType == ForumType.DropDown)
                if (string.IsNullOrWhiteSpace(updateQuestionCommand.RawData))
                    return false;

            return true;
        }
    }
}