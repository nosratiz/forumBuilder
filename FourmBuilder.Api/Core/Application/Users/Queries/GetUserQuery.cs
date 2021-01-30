using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Users.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Users.Queries
{
    public class GetUserQuery : IRequest<Result<UserDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IMongoRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id, cancellationToken);

            if (user is null)
                return Result<UserDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));


            return Result<UserDto>.SuccessFul(_mapper.Map<UserDto>(user));
        }
    }
}