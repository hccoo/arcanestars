using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class QuestionTag : Entity<long>
    {
        public string Name { get; set; }

        public long QuestionId { get; set; }
    }

    public static class QuestionTagFactory
    {
        public static QuestionTag CreateInstance(string name, long questionId)
        {
            return new QuestionTag { Name = name, QuestionId = questionId };
        }
    }
}
