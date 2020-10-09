using ArcaneStars.Infrastructure;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Infrastructure.Exceptions;
using ArcaneStars.JPService.Applications.Dtos;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;
using AutoMapper;
using Snowflake.Core;
using System;
using System.Linq;

namespace ArcaneStars.JPService.Applications.Services
{
    public interface IRecommendAppService : IApplicationServiceContract
    {
        void AddRecommend(RecommendDto model, string operatedBy);

        void UpdateRecommend(RecommendDto model, string operatedBy);

        PagingResultDto<RecommendDto> Get(int pageIndex, int pageSize, long questionId);

        RecommendDto Get(long id);
    }

    public class RecommendAppService : ApplicationServiceContract, IRecommendAppService
    {
        readonly IEventBus _eventBus;
        readonly IMapper _mapper;
        readonly IRecommendRepository _recommendRepository;
        readonly IRecommendMediaRepository _recommendMediaRepository;
        readonly IRecommendSpecRepository _recommendSpecRepository;

        public RecommendAppService(
            IMapper mapper,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus,
            IRecommendRepository recommendRepository,
            IRecommendMediaRepository recommendMediaRepository,
            IRecommendSpecRepository recommendSpecRepository) : base(dbUnitOfWork)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _recommendMediaRepository = recommendMediaRepository;
            _recommendRepository = recommendRepository;
            _recommendSpecRepository = recommendSpecRepository;
        }

        public void AddRecommend(RecommendDto model, string operatedBy)
        {
            var recommend = RecommendFactory.CreateInstance(model.Title, model.GetUrl, model.Price, model.Description, operatedBy);
            _recommendRepository.Add(recommend);
            if (model.Medias != null && model.Medias.Any())
            {
                foreach (var item in model.Medias)
                {
                    var media = RecommendMediaFactory.CreateInstance(item.Title, recommend.Id, item.Url, item.MediaType, operatedBy);
                    _recommendMediaRepository.Add(media);
                }
            }
            if (model.Specs != null && model.Specs.Any())
            {
                foreach (var item in model.Specs)
                {
                    var spec = RecommendSpecFactory.CreateInstance(item.Name, item.Value,recommend.Id, operatedBy);
                    _recommendSpecRepository.Add(spec);
                }
            }
            _dbUnitOfWork.Commit();
        }

        public PagingResultDto<RecommendDto> Get(int pageIndex, int pageSize, long questionId)
        {
            var result = new PagingResultDto<RecommendDto>();
            var data = _recommendRepository.GetFiltered(o => o.QuestionId == questionId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(item => _mapper.Map<RecommendDto>(item));
            result.Data = data;
            result.Code = BusinessCode.Successed;
            result.Message = string.Empty;
            result.Total = _recommendRepository.GetFiltered(o => o.QuestionId == questionId).Count();
            return result;
        }

        public RecommendDto Get(long id)
        {
            var entity = _recommendRepository.Get(id);
            var result = _mapper.Map<RecommendDto>(entity);
            var specs = _recommendSpecRepository.GetFiltered(o => o.RecommendId == entity.Id).Select(item=>_mapper.Map<RecommendSpecDto>(item)).ToList();
            var medias = _recommendMediaRepository.GetFiltered(o => o.RecommendId == entity.Id).Select(item=>_mapper.Map<RecommendMediaDto>(item)).ToList();
            result.Specs = specs;
            result.Medias = medias;
            return result;
        }

        public void UpdateRecommend(RecommendDto model, string operatedBy)
        {
            var entity = _recommendRepository.Get(model.Id);
            if (entity == null) throw new AppServiceException("No recommend found.");

            entity.Description = string.IsNullOrEmpty(model.Description) ? entity.Description : model.Description;
            entity.GetUrl = string.IsNullOrEmpty(model.GetUrl) ? entity.GetUrl : model.GetUrl;
            entity.Price = model.Price == null ? entity.Price : model.Price;
            entity.Title = string.IsNullOrEmpty(model.Title) ? entity.Title : model.Title;

            var medias = _recommendMediaRepository.GetFiltered(o => o.RecommendId == entity.Id).ToList();
            var specs = _recommendSpecRepository.GetFiltered(o => o.RecommendId == entity.Id).ToList();

            if (medias != null && medias.Any())
            {
                foreach (var item in medias)
                {
                    _recommendMediaRepository.Remove(item);
                }
            }
            if (specs != null && specs.Any())
            {
                foreach (var item in specs)
                {
                    _recommendSpecRepository.Remove(item);
                }
            }
            if (model.Medias != null && model.Medias.Any())
            {
                foreach (var item in model.Medias)
                {
                    var media = RecommendMediaFactory.CreateInstance(item.Title, model.Id, item.Url, item.MediaType, operatedBy);
                    _recommendMediaRepository.Add(media);
                }
            }
            if (model.Specs != null && model.Specs.Any())
            {
                foreach (var item in model.Specs)
                {
                    var spec = RecommendSpecFactory.CreateInstance(item.Name, item.Value, model.Id, operatedBy);
                    _recommendSpecRepository.Add(spec);
                }
            }
            _dbUnitOfWork.Commit();
        }
    }
}
