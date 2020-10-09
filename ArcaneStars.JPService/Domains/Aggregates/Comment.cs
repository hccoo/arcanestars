using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class Comment : Entity<long>
    {
        /// <summary>
        /// 您的实际经历
        /// </summary>
        public string Experience { get; set; }

        public Suggestion Suggestion { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public long RecommendId { get; set; }

        public void GenerateId()
        {
            Id = IDGenerator.GenerateId();
        }
    }

    public enum Suggestion
    {
        PRO = 1,
        CON = 2
    }

    public static class CommentFactory
    {
        public static Comment CreateInstance(string title, string remark, string experience, Suggestion suggestion, string operatedBy)
        {
            var now = DateTime.Now;

            var instance = new Comment
            {
                CreatedBy = operatedBy,
                CreatedOn = now,
                UpdatedOn = now,
                UpdatedBy = operatedBy,
                Title = title,
                Experience = experience,
                Remark = remark,
                Suggestion = suggestion
            };

            instance.GenerateId();

            return instance;
        }
    }
}
