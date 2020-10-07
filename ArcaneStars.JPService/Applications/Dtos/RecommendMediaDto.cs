using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class RecommendMediaDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long RecommendId { get; set; }

        public string Url { get; set; }

        public RecommendMediaType MediaType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
