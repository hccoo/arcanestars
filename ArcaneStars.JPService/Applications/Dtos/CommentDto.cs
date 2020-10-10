using ArcaneStars.JPService.Domains.Aggregates;
using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class CommentDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 您的实际经历
        /// </summary>
        public string Experience { get; set; }

        public Suggestion? Suggestion { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public long RecommendId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
