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
    public interface ICommentAppService : IApplicationServiceContract
    {
        void AddComment(CommentDto model, string operatedBy);

        void UpdateComment(CommentDto model, string operatedBy);

        void DeleteComment(long id);

        /// <summary>
        /// get paging result
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mark">PRO or CON or ALL or empty</param>
        /// <returns></returns>
        PagingResultDto<CommentDto> Get(long recommendId, int pageIndex, int pageSize, string mark);

        CommentDto Get(long id);
    }

    public class CommentAppService : ApplicationServiceContract, ICommentAppService
    {
        readonly IEventBus _eventBus;
        readonly IMapper _mapper;
        readonly ICommentRepository _commentRepository;

        public CommentAppService(
            IMapper mapper,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus,
            ICommentRepository commentRepository) : base(dbUnitOfWork)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public void AddComment(CommentDto model, string operatedBy)
        {
            var comment = CommentFactory.CreateInstance(model.Title, model.Remark, model.Experience, model.Suggestion ?? Suggestion.CON,model.RecommendId, operatedBy);
            _commentRepository.Add(comment);
            _dbUnitOfWork.Commit();
        }

        public void DeleteComment(long id)
        {
            var comment = _commentRepository.Get(id);
            if (comment == null) throw new AppServiceException("No object found.");
            _commentRepository.Remove(comment);
            _dbUnitOfWork.Commit();
        }

        public PagingResultDto<CommentDto> Get(long recommendId, int pageIndex, int pageSize, string mark)
        {
            var suggestion = mark == "PRO" ? Suggestion.PRO : Suggestion.CON;
            var querable = _commentRepository.GetFiltered(o => o.RecommendId == recommendId);
            if (mark == "PRO") querable = querable.Where(o => o.Suggestion == Suggestion.PRO);
            if (mark == "CON") querable = querable.Where(o => o.Suggestion == Suggestion.CON);

            var data = querable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(item=>_mapper.Map<CommentDto>(item)).ToList();
            var total = querable.Count();
            return new PagingResultDto<CommentDto>
            {
                Code = BusinessCode.Successed,
                Data = data,
                Message = string.Empty,
                Total = total
            };
        }

        public CommentDto Get(long id)
        {
            var comment = _commentRepository.Get(id);
            return _mapper.Map<CommentDto>(comment);
        }

        public void UpdateComment(CommentDto model, string operatedBy)
        {
            var comment = _commentRepository.Get(model.Id);
            comment.Experience = string.IsNullOrEmpty(model.Experience) ? comment.Experience : model.Experience;
            comment.Remark = string.IsNullOrEmpty(model.Remark) ? comment.Remark : model.Remark;
            comment.Suggestion = model.Suggestion == null ? comment.Suggestion : model.Suggestion.Value;
            comment.Title = string.IsNullOrEmpty(model.Title) ? comment.Title : model.Title;
            comment.UpdatedBy = operatedBy;
            comment.UpdatedOn = DateTime.Now;
            _commentRepository.Update(comment);
            _dbUnitOfWork.Commit();
        }
    }
}
