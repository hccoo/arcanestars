using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class Recommend : Entity<long>
    {
        public string Title { get; set; }

        public string GetUrl { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public void GenerateId()
        {
            Id = IDGenerator.GenerateId();
        }
    }

    public static class RecommendFactory
    {
        public static Recommend CreateInstance(string title, string url, decimal? price, string description, string operatedBy)
        {
            var now = DateTime.Now;
            var instance = new Recommend
            {
                Title = title,
                GetUrl = url,
                CreatedBy = operatedBy,
                CreatedOn = now,
                Description = description,
                Price = price,
                UpdatedBy = operatedBy,
                UpdatedOn = now
            };

            instance.GenerateId();

            return instance;
        }
    }
}
