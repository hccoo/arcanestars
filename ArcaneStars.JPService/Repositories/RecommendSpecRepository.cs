using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class RecommendSpecRepository : Repository<RecommendSpec, long>, IRecommendSpecRepository
    {
        public RecommendSpecRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
