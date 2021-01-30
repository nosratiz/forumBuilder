using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateProfile;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;

namespace FourmBuilder.Api.Core.Validator.Auth
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMongoRepository<User> _userRepository;
        public UpdateProfileCommandValidator(ICurrentUserService currentUserService, IMongoRepository<User> userRepository)
        {
            _currentUserService = currentUserService;
            _userRepository = userRepository;

            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Mobile)
                .NotNull()
                .NotEmpty()
                .MustAsync(ValidMobile)
                .WithMessage(ResponseMessage.MobileAlreadyExist);


            RuleFor(dto => dto.FirstName)
                .NotNull()
                .NotEmpty();


            RuleFor(dto => dto.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(dto => dto.Email)
                .EmailAddress()
                .MustAsync(ValidEmail).WithMessage(ResponseMessage.EmailAlreadyExist);

        }


        private async Task<bool> ValidMobile(string mobile, CancellationToken cancellation)
        {
            if (await _userRepository.ExistsAsync(x =>
                x.Mobile == mobile && x.IsDelete == false && x.Id != Guid.Parse(_currentUserService.UserId), cancellation))
                return false;

            return true;
        }

        private async Task<bool> ValidEmail(string email, CancellationToken cancellation)
        {
            if (await _userRepository.ExistsAsync(x =>
                x.IsDelete == false && x.Email == email && x.Id != Guid.Parse(_currentUserService.UserId), cancellation))
                return false;
            return true;
        }
    }
}
