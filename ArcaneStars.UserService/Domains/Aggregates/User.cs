using ArcaneStars.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.UserService.Domains.Aggregates
{
    public class User : Entity<int>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
