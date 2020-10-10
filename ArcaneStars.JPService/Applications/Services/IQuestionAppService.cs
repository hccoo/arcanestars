using ArcaneStars.Infrastructure;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Infrastructure.Exceptions;
using ArcaneStars.JPService.Applications.Dtos;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;
using AutoMapper;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcaneStars.JPService.Applications.Services
{
    public interface IQuestionAppService : IApplicationServiceContract
    {
        void AddQuestion(QuestionDto question, string operatedBy);

        void UpdateQuestion(QuestionDto question, string operatedBy);

        PagingResultDto<QuestionDto> Get(string key, string tag, int pageIndex, int pageSize);

        QuestionDto Get(long id);
    }

    public class QuestionAppService : ApplicationServiceContract, IQuestionAppService
    {
        readonly IEventBus _eventBus;
        readonly IMapper _mapper;
        readonly IQuestionRepository _questionRepository;
        readonly IQuestionTagRepository _questionTagRepository;

        public QuestionAppService(
            IMapper mapper,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus,
            IQuestionRepository questionRepository,
            IQuestionTagRepository questionTagRepository) : base(dbUnitOfWork)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionTagRepository = questionTagRepository;
        }

        public void AddQuestion(QuestionDto question, string operatedBy)
        {
            var entity = QuestionFactory.CreateInstance(question.Subject, question.Remark, operatedBy);
            _questionRepository.Add(entity);
            var tags = question.Tags;
            foreach (var item in tags)
            {
                var tag = QuestionTagFactory.CreateInstance(item.Name, entity.Id);
                _questionTagRepository.Add(tag);
            }
            _dbUnitOfWork.Commit();
        }

        public QuestionDto Get(long id)
        {
            var question = _questionRepository.Get(id);
            if (question == null) return null;

            var entityTags = _questionTagRepository.GetFiltered(o => o.QuestionId == id).ToList();
            var tags = entityTags.Select(item => _mapper.Map<QuestionTagDto>(item)).ToList();
            var result = _mapper.Map<QuestionDto>(question);
            result.Tags = tags;
            return result;
        }

        public PagingResultDto<QuestionDto> Get(string key, string tag, int pageIndex, int pageSize)
        {
            var questions = _questionRepository.Get(key, tag, pageIndex, pageSize, out long total).ToList();
            var data = questions.Select(q => _mapper.Map<QuestionDto>(q)).ToList();
            return new PagingResultDto<QuestionDto>
            {
                Code = BusinessCode.Successed,
                Data = data,
                Message = string.Empty,
                Total = total
            };
        }

        public void UpdateQuestion(QuestionDto question, string operatedBy)
        {
            var entity = _questionRepository.Get(question.Id);
            if (entity == null) throw new AppServiceException("No object found.");

            entity.Remark = string.IsNullOrEmpty(question.Remark) ? entity.Remark : question.Remark;
            entity.Subject = string.IsNullOrEmpty(question.Subject) ? entity.Subject : question.Subject;
            entity.UpdatedBy = operatedBy;
            entity.UpdatedOn = DateTime.Now;

            var entityTags = _questionTagRepository.GetFiltered(o => o.QuestionId == question.Id).ToList();
            if (entityTags != null && entityTags.Any())
            {
                foreach (var item in entityTags)
                {
                    _questionTagRepository.Remove(item);
                }
            }
            IEnumerable<QuestionTagDto> tags = question.Tags ?? new List<QuestionTagDto>();
            foreach (var item in tags)
            {
                var tag = QuestionTagFactory.CreateInstance(item.Name, question.Id);
                _questionTagRepository.Add(tag);
            }
            _questionRepository.Update(entity);
            _dbUnitOfWork.Commit();
        }

    }
}
