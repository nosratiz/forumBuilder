using System;
using System.Collections.Generic;
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

namespace FourmBuilder.Api.Core.Application.Questions.Command.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Result>
    {
        private readonly IMongoRepository<Forum> _forumRepository;
        private readonly IMapper _mapper;

        public UpdateQuestionCommandHandler(IMongoRepository<Forum> forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var fourm = await _forumRepository.GetAsync(request.ForumId, cancellationToken);

            if (fourm is null)
                return Result.Failed(
                    new BadRequestObjectResult(new ApiMessage(ResponseMessage.ForumNotFound)));


            var question = fourm.ForumQuestions.FirstOrDefault(x => x.Id == request.QuestionId);

            if (question is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.QuestionNotFound)));

            List<ForumOptions> forumOptionsList = new List<ForumOptions>();

            _mapper.Map(request, question);

            request.Answers?.ForEach(dto =>
            {
                forumOptionsList.Add(new ForumOptions
                {
                    Id = Guid.NewGuid(),
                    Options = dto.Option
                });
            });

            question.Options = forumOptionsList;

            await _forumRepository.UpdateAsync(fourm);

            return Result.SuccessFul();
        }
    }
}