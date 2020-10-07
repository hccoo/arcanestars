using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Domains.Aggregates
{
    public class RecommendMedia : Entity<long>
    {
        public string Title { get; set; }

        public long RecommendId { get; set; }

        public string Url { get; set; }

        public RecommendMediaType MediaType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public enum RecommendMediaType
    {
        Image = 1,
        Video = 2
    }

    public static class RecommendMediaFactory
    {
        public static RecommendMedia CreateInstance(string title, long recommendId, string url, RecommendMediaType type, string operatedBy)
        {
            var now = DateTime.Now;

            var instance = new RecommendMedia
            {
                CreatedBy = operatedBy,
                CreatedOn = now,
                MediaType = type,
                RecommendId = recommendId,
                Title = title,
                Url = url
            };

            return instance;
        }
    }
}
