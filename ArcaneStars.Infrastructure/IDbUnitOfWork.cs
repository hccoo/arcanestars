﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ArcaneStars.Infrastructure
{
    public interface IDbUnitOfWork : IUnitOfWork, IDisposable
    {
        void BeginTransaction();

        void Complete();

        DbContext DatabaseContext { get; }
    }
}
