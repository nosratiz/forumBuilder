using System;
using System.Collections.Generic;
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

namespace FourmBuilder.Api.Core.Application.Questions.Command.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<ForumQuestionDto>>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result<ForumQuestionDto>> Handle(CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.ForumId, cancellationToken);

            if (fourm is null)
                return Result<ForumQuestionDto>.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));

            List<ForumQuestion> forumQuestions = fourm.ForumQuestions ??= new List<ForumQuestion>();
            List<ForumOptions> forumOptionsList = new List<ForumOptions>();
            
            
            var question = _mapper.Map<ForumQuestion>(request);
            question.QuestionNumber = (int) (fourm.ForumQuestions?.Count + 1);

            request.Answers?.ForEach(dto =>
            {
                forumOptionsList.Add(new ForumOptions
                {
                    Id = Guid.NewGuid(),
                    Options = dto.Option
                });
            });

            question.Options = forumOptionsList;

            forumQuestions.Add(question);

            fourm.ForumQuestions = forumQuestions;

            await _forumRepository.UpdateAsync(fourm);

            return Result<ForumQuestionDto>.SuccessFul(_mapper.Map<ForumQuestionDto>(question));
        }
    }
}