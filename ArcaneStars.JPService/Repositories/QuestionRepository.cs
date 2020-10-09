using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Question> Get(string key, string tag, int pageIndex, int pageSize, out long total)
        {
            total = ServiceDbContext.QuestionTags.GroupJoin(ServiceDbContext.Questions, tag => tag.QuestionId, question => question.Id, (tag, question) => new { Tag = tag, Question = question })
                                                 .Where(o => o.Tag.Name == tag)
                                                 .SelectMany(combination => combination.Question.DefaultIfEmpty(), (tag, question) => question)
                                                 .Where(g => g.Subject.Contains(key)).Count();

            return ServiceDbContext.QuestionTags.GroupJoin(ServiceDbContext.Questions, tag => tag.QuestionId, question => question.Id, (tag, question) => new { Tag = tag, Question = question })
                                                .Where(o=>o.Tag.Name==tag)
                                                .SelectMany(combination => combination.Question.DefaultIfEmpty(), (tag, question) => question)
                                                .Where(g=>g.Subject.Contains(key))
                                                .Skip((pageIndex-1)*pageSize)
                                                .Take(pageSize)
                                                .ToList();
        }
    }
}
