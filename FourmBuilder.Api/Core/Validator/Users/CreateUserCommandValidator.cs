using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Users.Command.CreateUser;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;

namespace FourmBuilder.Api.Core.Validator.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IMongoRepository<User> _userRepository;
        public CreateUserCommandValidator(IMongoRepository<User> userRepository)
        {
            CascadeMode = CascadeMode.Stop;
            _userRepository = userRepository;

            RuleFor(dto => dto.FirstName)
                .NotEmpty().NotNull()
                .MinimumLength(2);

            RuleFor(dto => dto.LastName)
                .NotEmpty().NotNull()
                .MinimumLength(2);

            RuleFor(dto => dto.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .EmailAddress().WithMessage(ResponseMessage.InvalidEmailFormat);

            RuleFor(dto => dto.Mobile)
                .NotEmpty()
                .NotNull();

            RuleFor(dto => dto).MustAsync(ValidMobile)
                .WithMessage(ResponseMessage.MobileAlreadyExist)
                .MustAsync(ValidEmailAddress).WithMessage(ResponseMessage.EmailAlreadyExist);
        }


        private async Task<bool> ValidEmailAddress(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        => !await _userRepository.ExistsAsync(x => x.Email == createUserCommand.Email && x.IsDelete == false, cancellationToken);


        private async Task<bool> ValidMobile(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(createUserCommand.Mobile))
                if (await _userRepository.ExistsAsync(x => x.IsDelete == false && x.Mobile == createUserCommand.Mobile, cancellationToken))
                    return false;

            return true;
        }

    }
}