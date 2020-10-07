using ArcaneStars.Infrastructure;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Infrastructure.Exceptions;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;
using AutoMapper;
using Snowflake.Core;
using System;
using System.Linq;

namespace ArcaneStars.JPService.Applications.Services
{
    public interface IQuestionAppService : IApplicationServiceContract
    {
        
    }

    public class QuestionAppService : ApplicationServiceContract, IQuestionAppService
    {
        readonly IEventBus _eventBus;
        readonly IMapper _mapper;

        public QuestionAppService(
            IMapper mapper,
            IDbUnitOfWork dbUnitOfWork,
            IEventBus eventBus) : base(dbUnitOfWork)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

    }
}
