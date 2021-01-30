using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Users.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Users.Queries
{
    public class GetAccountInfoQuery : IRequest<Result<UserDto>>
    {
    }

    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQuery, Result<UserDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAccountInfoQueryHandler(IMongoRepository<User> userRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(Guid.Parse(_currentUserService.UserId), cancellationToken);

            if (user is null)
                return Result<UserDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            return Result<UserDto>.SuccessFul(_mapper.Map<UserDto>(user));
        }
    }
}
