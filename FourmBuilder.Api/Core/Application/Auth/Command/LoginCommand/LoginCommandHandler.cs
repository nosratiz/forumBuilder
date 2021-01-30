using System;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Auth.Dto;
using FourmBuilder.Api.Core.Interfaces;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Auth.Command.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(IMongoRepository<User> userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Email.ToLower() == request.Email.ToLower()  && x.IsDelete == false, cancellationToken);

            if (user is null)
                return Result<TokenDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.InvalidUserNameOrPassword)));

            if (PasswordManagement.CheckPassword(request.Password, user.Password) == false)
                return Result<TokenDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.InvalidUserNameOrPassword)));

            if (user.IsMobileConfirm == false)
                return Result<TokenDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.AccountDeactivate)));
            

            var result = await _tokenGenerator.Generate(user, cancellationToken);

            return Result<TokenDto>.SuccessFul(new TokenDto
            {
                AccessToken = result.Data.AccessToken,
                RoleName = result.Data.RoleName
            });
        }
    }
}