using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class RecommendRepository : Repository<Recommend, long>, IRecommendRepository
    {
        public RecommendRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
