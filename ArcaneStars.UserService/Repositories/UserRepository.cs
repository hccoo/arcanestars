using ArcaneStars.Service.Repositories;
using ArcaneStars.UserService.Domains.Aggregates;
using ArcaneStars.UserService.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.UserService.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(IDbContextProvider provider) : base(provider) { }
    }
}
