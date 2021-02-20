using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Auth.Command.RegisterCommand;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;

namespace FourmBuilder.Api.Core.Validator.Auth
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IMongoRepository<User> _userRepository;

        public RegisterUserCommandValidator(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;

            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(dto => dto.LastName).NotNull().NotEmpty().MinimumLength(2);

            RuleFor(dto => dto.Mobile)
                .MustAsync(ValidMobileAddress)
                .WithMessage(ResponseMessage.MobileAlreadyExist)
                .Must(ValidMobileNumber).WithMessage(ResponseMessage.MobileFormatInvalid);


            RuleFor(dto => dto.Email)
                .NotEmpty().NotNull().EmailAddress().MaximumLength(50);

            RuleFor(dto => dto.Password)
                .NotNull().NotEmpty()
                .MinimumLength(6).MaximumLength(20);

            RuleFor(dto => dto.StudentNumber).NotEmpty().NotNull();

            RuleFor(dto => dto.UserType).NotEmpty().NotNull();


            RuleFor(dto => dto)
                .MustAsync(ValidEmailAddress).WithMessage(ResponseMessage.EmailAlreadyExist);
        }


        private async Task<bool>
            ValidEmailAddress(RegisterCommand registerCommand, CancellationToken cancellationToken) =>
            !await _userRepository.ExistsAsync(x => x.IsDelete == false && x.Email == registerCommand.Email,
                cancellationToken);

        private async Task<bool> ValidMobileAddress(string mobile, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                if (await _userRepository.ExistsAsync(x => x.IsDelete == false && x.Mobile == mobile,
                    cancellationToken))
                    return false;
            }

            return true;
        }

        private bool ValidMobileNumber(string mobile)
        {
            if (!string.IsNullOrWhiteSpace(mobile))
                if (!mobile.StartsWith("09"))
                    return false;

            return true;
        }
    }
}