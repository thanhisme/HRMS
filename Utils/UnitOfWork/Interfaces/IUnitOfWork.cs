using Microsoft.EntityFrameworkCore;

namespace Utils.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public DbSet<TEntity> Repository<TEntity>() where TEntity : class;

        public DbContext GetDbContext();

        public Task<int> SaveChangesAsync();

        public void Dispose();
    }
}
