using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class RecommendMediaRepository : Repository<RecommendMedia, long>, IRecommendMediaRepository
    {
        public RecommendMediaRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
