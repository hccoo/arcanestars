using System;
using System.Collections.Generic;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;

namespace ArcaneStars.Service.Repositories
{
    public class CommentRepository : Repository<Comment, long>, ICommentRepository
    {
        public CommentRepository(IDbContextProvider provider) : base(provider)
        {

        }
    }
}
