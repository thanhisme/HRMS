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

        public virtual DbSet<Policy> Policies { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<DayOffRequest> DayOffRequests { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<OvertimeRequest> OvertimeRequests { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Salary> Salaries { get; set; }

        public virtual DbSet<RewardFineHistory> RewardFineHistories { get; set; }

        public virtual DbSet<WorkingInfo> WorkingInfos { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Timekeeping> Timekeepings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .HasMany(p => p.SubPolicies)
                .WithOne(p => p.Parent)
                .HasForeignKey(p => p.ParentId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.PartnerA)
                .WithMany()
                .HasForeignKey(c => c.PartnerAId)
                .OnDelete(DeleteBehavior.NoAction); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.PartnerB)
                .WithMany()
                .HasForeignKey(c => c.PartnerBId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkingInfo>()
                .HasOne(wi => wi.Employee)
                .WithMany(u => u.WorkingInfos)
                .HasForeignKey(wi => wi.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
