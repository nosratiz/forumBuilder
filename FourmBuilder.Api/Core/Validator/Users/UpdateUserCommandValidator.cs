using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateUser;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;

namespace FourmBuilder.Api.Core.Validator.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IMongoRepository<User> _userRepository;
        public UpdateUserCommandValidator(IMongoRepository<User> userRepository)
        {
            CascadeMode = CascadeMode.Stop;
            _userRepository = userRepository;

            RuleFor(dto => dto.Id).NotEmpty();

            RuleFor(dto => dto.FirstName)
                .NotEmpty().NotNull()
                .MinimumLength(2);

            RuleFor(dto => dto.LastName)
                .NotEmpty().NotNull()
                .MinimumLength(2);
            
          
            RuleFor(dto => dto.Email)
                .NotEmpty().NotNull().EmailAddress().MaximumLength(50);
            

            RuleFor(dto => dto.StudentNumber)
                .NotEmpty().NotNull();

            RuleFor(dto => dto.UserType).NotEmpty().NotNull();


            RuleFor(dto => dto).MustAsync(ValidMobile)
                .WithMessage(ResponseMessage.MobileAlreadyExist);

      
            RuleFor(dto => dto)
                .MustAsync(ValidMobile)
                .WithMessage(ResponseMessage.MobileAlreadyExist)
                .MustAsync(ValidEmailAddress)
                .WithMessage(ResponseMessage.EmailAlreadyExist);


        }


        private async Task<bool> ValidEmailAddress(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
            => !await _userRepository.ExistsAsync(x => x.Email == updateUserCommand.Email && x.Id != updateUserCommand.Id && x.IsDelete == false, cancellationToken);


        private async Task<bool> ValidMobile(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(updateUserCommand.Mobile))
                if (await _userRepository.ExistsAsync(x => x.IsDelete == false && x.Mobile == updateUserCommand.Mobile && x.Id != updateUserCommand.Id, cancellationToken))
                    return false;

            return true;
        }
    }
}