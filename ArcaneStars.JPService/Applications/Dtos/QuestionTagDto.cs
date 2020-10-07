using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.JPService.Applications.Dtos
{
    public class QuestionTagDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long QuestionId { get; set; }
    }
}
