using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Auth.Dto;
using FourmBuilder.Api.Core.Interfaces;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;

namespace FourmBuilder.Api.Core.Application.Auth.Command.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<TokenDto>>
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMongoRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;

        public RegisterCommandHandler(IMongoRepository<User> userRepository, IMapper mapper,
            IMongoRepository<Role> roleRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<TokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var role = await _roleRepository.GetAsync(x => x.Name == Role.User, cancellationToken);

            user.Role = role;

            await _userRepository.AddAsync(user);
            
            var result = await _tokenGenerator.Generate(user, cancellationToken);

            return Result<TokenDto>.SuccessFul(new TokenDto
            {
                AccessToken = result.Data.AccessToken,
                RoleName = Role.User
            });
        }
    }
}