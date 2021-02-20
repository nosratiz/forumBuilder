using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Forums.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Core.Application.Questions.Queries
{
    public class GetQuestionQuery : IRequest<Result<ForumQuestionDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, Result<ForumQuestionDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public GetQuestionQueryHandler(IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result<ForumQuestionDto>> Handle(GetQuestionQuery request,
            CancellationToken cancellationToken)
        {
            var forum = await _forumRepository.GetAsync(x => x.ForumQuestions.Any(q => q.Id == request.Id),
                cancellationToken);


            if (forum is null)
                return Result<ForumQuestionDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.QuestionNotFound)));

            var question = forum.ForumQuestions.FirstOrDefault(x => x.Id == request.Id);

            return Result<ForumQuestionDto>.SuccessFul(_mapper.Map<ForumQuestionDto>(question));
        }
    }
}