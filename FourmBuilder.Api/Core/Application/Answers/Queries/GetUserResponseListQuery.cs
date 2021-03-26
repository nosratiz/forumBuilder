using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Mongo;
using MediatR;

namespace FourmBuilder.Api.Core.Answers.Queries
{
    public class GetUserResponseListQuery : IRequest<List<UserResponse>>
    {
        public Guid ForumId { get; set; }
    }


    public class GetUserResponseListQueryHandler : IRequestHandler<GetUserResponseListQuery, List<UserResponse>>
    {
        private readonly IMongoRepository<UserResponse> _userResponseRepository;

        public GetUserResponseListQueryHandler(IMongoRepository<UserResponse> userResponseRepository)
        {
            _userResponseRepository = userResponseRepository;
        }

        public async Task<List<UserResponse>> Handle(GetUserResponseListQuery request,
            CancellationToken cancellationToken)
        {
            return
                await _userResponseRepository.FindAsync(x => x.Forum.Id == request.ForumId, cancellationToken);
        }
    }
}