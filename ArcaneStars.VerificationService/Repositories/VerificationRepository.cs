using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.Service.Domain.Aggregates;
using ArcaneStars.Service.Domain.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class VerificationRepository : Repository<Verification, int>, IVerificationRepository
    {
        public VerificationRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
