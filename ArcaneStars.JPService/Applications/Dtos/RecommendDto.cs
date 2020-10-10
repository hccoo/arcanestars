using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class RecommendDto
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public string GetUrl { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public long QuestionId { get; set; }

        public IEnumerable<RecommendMediaDto> Medias { get; set; }

        public IEnumerable<RecommendSpecDto> Specs { get; set; }
    }
}
