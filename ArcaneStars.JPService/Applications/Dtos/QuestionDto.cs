using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class QuestionDto
    {
        public long Id { get; set; }

        public string Subject { get; set; }

        public string Remark { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
