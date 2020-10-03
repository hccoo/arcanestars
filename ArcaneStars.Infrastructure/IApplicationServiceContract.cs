using System;

namespace ArcaneStars.Infrastructure
{
    public interface IApplicationServiceContract : IDisposable
    {
    }

    public class ApplicationServiceContract : DisposableObject
    {
        protected readonly IDbUnitOfWork _dbUnitOfWork;

        public ApplicationServiceContract(IDbUnitOfWork dbUnitOfWork)
        {
            _dbUnitOfWork = dbUnitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
