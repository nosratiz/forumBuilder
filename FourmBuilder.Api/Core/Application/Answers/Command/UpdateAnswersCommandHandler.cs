using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Claims;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Answers.Command
{
    public class UpdateAnswersCommandHandler : IRequestHandler<UpdateAnswersCommand, Result>
    {
        private readonly IMongoRepository<UserResponse> _UserResponseRepository;
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMongoRepository<User> _userRepository;
        private readonly ICurrentUserService _currentUser;

        public UpdateAnswersCommandHandler(IMongoRepository<UserResponse> userResponseRepository,
            IMongoRepository<Forum> forumRepository, ICurrentUserService currentUser,
            IMongoRepository<User> userRepository)
        {
            _UserResponseRepository = userResponseRepository;
            _forumRepository = forumRepository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UpdateAnswersCommand request, CancellationToken cancellationToken)
        {
            var forum = await _forumRepository.GetAsync(request.ForumId, cancellationToken);

            if (forum is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            User user = null;
            if (_currentUser.IsAuthenticated)
            {
                user = await _userRepository.GetAsync(Guid.Parse(_currentUser.UserId), cancellationToken);
            }

            foreach (var questionAnswers in request.QuestionAnswers)
            {
                if (forum.ForumQuestions.Any(x => x.Id == questionAnswers.QuestionId) == false)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.QuestionNotFound)));
            }

            var userResponse = new UserResponse
            {
                Forum = forum,
                User = user,
                CreateDate = DateTime.Now.AddMinutes(220),
                Answers = request.QuestionAnswers.Select(x => x.Answers).ToList()
            };

            await _UserResponseRepository.AddAsync(userResponse);

            return Result.SuccessFul();
        }
    }
}