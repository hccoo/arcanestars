using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class RecommendSpec : Entity<long>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public long RecommendId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }

    public static class RecommendSpecFactory
    {
        public static RecommendSpec CreateInstance(string name, string value, long recommendId, string operatedBy)
        {
            var now = DateTime.Now;
            var instance = new RecommendSpec
            {
                CreatedBy = operatedBy,
                CreatedOn = now,
                Name = name,
                UpdatedBy = operatedBy,
                UpdatedOn = now,
                Value = value,
                RecommendId = recommendId
            };

            return instance;
        }
    }
}
