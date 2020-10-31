using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.JPService.Domains.Repositories;
using ArcaneStars.Infrastructure.UtilityExtensions;
using MySql.Data.MySqlClient;

namespace ArcaneStars.Service.Repositories
{
    public class QuestionRepository : Repository<Question, long>, IQuestionRepository
    {
        public QuestionRepository(IDbContextProvider provider) : base(provider)
        {

        }

        public IEnumerable<Question> Get(string key, string tag, int pageIndex, int pageSize, out long total)
        {
            //var countSql = @" select count(1) from question_tags qt left join questions q on qt.question_id=q.id where 1=1 ";
            //if (!string.IsNullOrEmpty(key)) countSql += @" and subject like @key ";
            //if (!string.IsNullOrEmpty(tag)) countSql += @" and qt.name = @tag ";

            //total = ServiceDbContext.Database.QuerySingleVal<long>(countSql, new MySqlParameter("@key", "%" + key + "%"), new MySqlParameter("@tag", tag));

            //var querySql = @" select q.id Id, q.subject Subject, q.remark Remark,q.created_by CreatedBy, q.created_on CreatedOn, q.updated_by UpdatedBy, q.updated_on UpdatedOn  from question_tags qt left join questions q on qt.question_id=q.id where 1=1 ";
            //if (!string.IsNullOrEmpty(key)) querySql += @" and subject like @key ";
            //if (!string.IsNullOrEmpty(tag)) querySql += @" and qt.name = @tag ";

            //var questions = ServiceDbContext.Database.SqlQuery<Question>(querySql, new MySqlParameter("@key", "%" + key + "%"), new MySqlParameter("@tag", tag)).ToList();

            //return questions;

            var countSql = @" select count(1) from question_tags qt left join questions q on qt.question_id=q.id where 1=1 ";
            if (!string.IsNullOrEmpty(key)) countSql += $@" and subject like '%{key}%' ";
            if (!string.IsNullOrEmpty(tag)) countSql += $@" and qt.name = '{tag}' ";

            total = ServiceDbContext.Database.QuerySingleVal<long>(countSql);

            var querySql = @" select q.id Id, q.subject Subject, q.remark Remark,q.created_by CreatedBy, q.created_on CreatedOn, q.updated_by UpdatedBy, q.updated_on UpdatedOn  from question_tags qt left join questions q on qt.question_id=q.id where 1=1 ";
            if (!string.IsNullOrEmpty(key)) querySql += $@" and subject like '%{key}%' ";
            if (!string.IsNullOrEmpty(tag)) querySql += $@" and qt.name = '{tag}' ";

            var questions = ServiceDbContext.Database.SqlQuery<Question>(querySql).ToList();

            return questions;

            //total = ServiceDbContext.QuestionTags.GroupJoin(ServiceDbContext.Questions, tag => tag.QuestionId, question => question.Id, (tag, question) => new { Tag = tag, Question = question })
            //                                     .Where(o => (tag == null || tag == "") || o.Tag.Name == tag)
            //                                     .SelectMany(combination => combination.Question.DefaultIfEmpty(), (tag, question) => question)
            //                                     .Where(g => string.IsNullOrEmpty(key) || g.Subject.Contains(key)).Count();

            //return ServiceDbContext.QuestionTags.GroupJoin(ServiceDbContext.Questions, tag => tag.QuestionId, question => question.Id, (tag, question) => new { Tag = tag, Question = question })
            //                                    .Where(o => string.IsNullOrEmpty(tag) || o.Tag.Name == tag)
            //                                    .SelectMany(combination => combination.Question.DefaultIfEmpty(), (tag, question) => question)
            //                                    .Where(g => string.IsNullOrEmpty(key) || g.Subject.Contains(key))
            //                                    .Skip((pageIndex - 1) * pageSize)
            //                                    .Take(pageSize)
            //                                    .ToList();
        }
    }
}
