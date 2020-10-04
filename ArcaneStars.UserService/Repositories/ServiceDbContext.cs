using ArcaneStars.Service.Configurations;
using ArcaneStars.UserService.Domains.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ArcaneStars.Service.Repositories
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var conn = "Database = 'user_service_db'; Data Source = 'localhost'; Port = 3306; User Id = 'root'; Password = '!Qaz2wSX'; charset = 'utf8'; pooling = true";
        //    var conn = IocProvider.GetService<IServiceConfigurationAgent>()?.ConnectionString;
            
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseMySql(conn);
        //}
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.UserName).HasColumnName("user_name").HasMaxLength(80).IsRequired();
            builder.Property(c => c.Mobile).HasColumnName("mobile").HasMaxLength(20).IsRequired();
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.Password).HasColumnName("password").HasMaxLength(500);
            builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(200);
            builder.Property(c => c.NickName).HasColumnName("nickname").HasMaxLength(80);
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(80);
            builder.Property(c => c.UpdatedOn).HasColumnName("updated_on");
        }
    }

    public class DesignTimeServiceDbContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
    {
        public ServiceDbContext CreateDbContext(string[] args)
        {
            return new ServiceDbContext(new DbContextOptionsBuilder().UseMySql("Database='user_service_db';Data Source='localhost';Port=3306;User Id='root';Password='!Qaz2wSX';charset='utf8';pooling=true").Options);
        }
    }
}
