using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Repositories
{
    public interface IQuestionRepository : IRepository<Question, long>
    {
        IEnumerable<Question> Get(string key, string tag, int pageIndex, int pageSize, out long total);
    }
}
