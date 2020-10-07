using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class Question : Entity<long>
    {
        public string Subject { get; set; }

        public string Remark { get; set; }

        [NotMapped]
        public IEnumerable<QuestionTag> Tags { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public void GenerateId()
        {
            Id = IDGenerator.GenerateId();
        }
    }

    public static class QuestionFactory
    {
        public static Question CreateInstance(string subject, string remark, string operatedBy, IEnumerable<string> tags = null)
        {
            var now = DateTime.Now;

            var instance = new Question
            {
                Subject = subject,
                CreatedBy = operatedBy,
                CreatedOn = now,
                Remark = remark,
                UpdatedBy = operatedBy,
                UpdatedOn = now
            };
            instance.GenerateId();

            if (tags != null && tags.Any())
                instance.Tags = tags.Select(item => new QuestionTag { Name = item, QuestionId = instance.Id }).ToList();

            return instance;
        }
    }
}
