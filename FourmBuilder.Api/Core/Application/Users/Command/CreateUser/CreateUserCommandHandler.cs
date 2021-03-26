using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Users.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Users.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMongoRepository<Role> _roleRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;


        public CreateUserCommandHandler(IMongoRepository<User> userRepository, IMapper mapper,
            IMongoRepository<Role> roleRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            
            var roleName = request.IsAdmin ? Role.Admin : Role.User;

            var role = await _roleRepository.GetAsync(x => x.Name == roleName, cancellationToken);

            user.Role = role;

            await _userRepository.AddAsync(user);


            return Result<UserDto>.SuccessFul(_mapper.Map<UserDto>(user));
        }
    }
}