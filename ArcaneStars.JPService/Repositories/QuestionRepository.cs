using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class QuestionRepository : Repository<Question, long>, IQuestionRepository
    {
        public QuestionRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
