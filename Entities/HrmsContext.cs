using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class HrmsContext : DbContext
    {
        public HrmsContext() { }

        public HrmsContext(DbContextOptions<HrmsContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<BlackListToken> BlackListTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            base.OnConfiguring(optionsBuilder);
    }
}
