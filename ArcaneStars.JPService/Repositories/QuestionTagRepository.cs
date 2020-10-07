using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class QuestionTagRepository : Repository<QuestionTag, long>, IQuestionTagRepository
    {
        public QuestionTagRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
