using ArcaneStars.Service.Domain;
using ArcaneStars.UserService.Domains.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.UserService.Domains.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
