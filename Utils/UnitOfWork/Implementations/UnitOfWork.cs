using Microsoft.EntityFrameworkCore;
using Utils.UnitOfWork.Interfaces;

namespace Utils.UnitOfWork.Implementations
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;

        private bool _disposed = false;

        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                var repository = _dbContext.Set<TEntity>();

                _repositories.Add(typeof(TEntity), repository);
            }

            return (DbSet<TEntity>)_repositories[typeof(TEntity)];
        }
        
        public DbContext GetDbContext()
        {
            return _dbContext;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
