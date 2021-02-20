using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Questions.Command.DeleteQuestions
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result>
    {
        private readonly IMongoRepository<Forum> _forumRepository;

        public DeleteQuestionCommandHandler(IMongoRepository<Forum> forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public async Task<Result> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.ForumId, cancellationToken);

            if (fourm is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            var question = fourm.ForumQuestions.FirstOrDefault(x => x.Id == request.QuestionId);

            if (question is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.QuestionNotFound)));

            fourm.ForumQuestions.Remove(question);

            int number = 1;
            fourm.ForumQuestions?.ForEach(q => { q.QuestionNumber = number++; });

            await _forumRepository.UpdateAsync(fourm);

            return Result.SuccessFul();
        }
    }
}