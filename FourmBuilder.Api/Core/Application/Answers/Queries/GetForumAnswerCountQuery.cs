using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FourmBuilder.Api.Core.Answers.Queries
{
    public class GetForumAnswerCountQuery : IRequest<int>
    {
        public Guid ForumId { get; set; }
    }

    public class GetForumAnswerCountQueryHandler : IRequestHandler<GetForumAnswerCountQuery, int>
    {
        private readonly IMongoRepository<UserResponse> _userResponseRepository;

        public GetForumAnswerCountQueryHandler(IMongoRepository<UserResponse> userResponseRepository)
        {
            _userResponseRepository = userResponseRepository;
        }

        public Task<int> Handle(GetForumAnswerCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userResponseRepository.GetAll()
                .Count(x => x.Forum.Id == request.ForumId));
        }
    }
}