using ArcaneStars.Service.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArcaneStars.Service.Repositories
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Verification> Verifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new VerificationConfiguration());
        }
    }

    public class VerificationConfiguration : IEntityTypeConfiguration<Verification>
    {
        public void Configure(EntityTypeBuilder<Verification> builder)
        {
            builder.ToTable("verifications");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.BizCode).HasColumnName("biz_code").IsRequired();
            builder.Property(c => c.Code).HasColumnName("code").HasMaxLength(80).IsRequired();
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.ExpiredOn).HasColumnName("expired_on").IsRequired();
            builder.Property(c => c.IsSuspend).HasColumnName("is_suspend").IsRequired();
            builder.Property(c => c.IsUsed).HasColumnName("is_used").IsRequired();
            builder.Property(c => c.LastUpdOn).HasColumnName("last_upd_on");
            builder.Property(c => c.To).HasColumnName("to").HasMaxLength(80).IsRequired();
        }
    }

    public class DesignTimeServiceDbContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
    {
        public ServiceDbContext CreateDbContext(string[] args)
        {
            return new ServiceDbContext(new DbContextOptionsBuilder().UseMySql("Database='arcanestars_verificationservice_db';Data Source='localhost';Port=3306;User Id='root';Password='!Qaz2wSX';charset='utf8';pooling=true").Options);
        }
    }
}
