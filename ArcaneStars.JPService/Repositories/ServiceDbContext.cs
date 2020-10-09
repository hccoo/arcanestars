using ArcaneStars.Service.Configurations;
using ArcaneStars.JPService.Domains.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ArcaneStars.Service.Repositories
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Recommend> Recommends { get; set; }
        public DbSet<RecommendMedia> RecommendMedias { get; set; }
        public DbSet<RecommendSpec> RecommendSpecs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new QuestionTagConfiguration());
            builder.ApplyConfiguration(new RecommendConfiguration());
            builder.ApplyConfiguration(new RecommendMediaConfiguration());
            builder.ApplyConfiguration(new RecommendSpecConfiguration());
        }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(80);
            builder.Property(c => c.UpdatedOn).HasColumnName("updated_on");
            builder.Property(c => c.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            builder.Property(c => c.RecommendId).HasColumnName("recommend_id").IsRequired();
            builder.Property(c => c.Suggestion).HasColumnName("suggestion").IsRequired();
            builder.Property(c => c.Experience).HasColumnName("experience").HasMaxLength(2000);
            builder.Property(c => c.Remark).HasColumnName("remark").HasMaxLength(1000);
        }
    }

    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(80);
            builder.Property(c => c.UpdatedOn).HasColumnName("updated_on");
            builder.Property(c => c.Subject).HasColumnName("subject").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Remark).HasColumnName("remark").HasMaxLength(1000);
        }
    }

    public class QuestionTagConfiguration : IEntityTypeConfiguration<QuestionTag>
    {
        public void Configure(EntityTypeBuilder<QuestionTag> builder)
        {
            builder.ToTable("question_tags");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(20).IsRequired();
            builder.Property(c => c.QuestionId).HasColumnName("question_id").IsRequired();
        }
    }

    public class RecommendConfiguration : IEntityTypeConfiguration<Recommend>
    {
        public void Configure(EntityTypeBuilder<Recommend> builder)
        {
            builder.ToTable("recommends");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(80);
            builder.Property(c => c.UpdatedOn).HasColumnName("updated_on");
            builder.Property(c => c.Description).HasColumnName("description").HasMaxLength(2000);
            builder.Property(c => c.GetUrl).HasColumnName("get_url").HasMaxLength(1000);
            builder.Property(c => c.Price).HasColumnName("price");
            builder.Property(c => c.QuestionId).HasColumnName("question_id").IsRequired();
            builder.Property(c => c.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        }
    }

    public class RecommendMediaConfiguration : IEntityTypeConfiguration<RecommendMedia>
    {
        public void Configure(EntityTypeBuilder<RecommendMedia> builder)
        {
            builder.ToTable("recommend_medias");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.MediaType).HasColumnName("media_type").IsRequired();
            builder.Property(c => c.RecommendId).HasColumnName("recommend_id").IsRequired();
            builder.Property(c => c.Url).HasColumnName("url").IsRequired();
            builder.Property(c => c.Title).HasColumnName("title").HasMaxLength(200);
        }
    }

    public class RecommendSpecConfiguration : IEntityTypeConfiguration<RecommendSpec>
    {
        public void Configure(EntityTypeBuilder<RecommendSpec> builder)
        {
            builder.ToTable("recommend_specs");
            builder.HasKey(c => new { c.Id });

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.CreatedOn).HasColumnName("created_on").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(80).IsRequired();
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(80);
            builder.Property(c => c.UpdatedOn).HasColumnName("updated_on");
            builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(80);
            builder.Property(c => c.Value).HasColumnName("value").HasMaxLength(1000).IsRequired();
            builder.Property(c => c.RecommendId).HasColumnName("recommend_id").IsRequired();
        }
    }

    public class DesignTimeServiceDbContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
    {
        public ServiceDbContext CreateDbContext(string[] args)
        {
            return new ServiceDbContext(new DbContextOptionsBuilder().UseMySql("Database='arcanestars_jpservice_db';Data Source='localhost';Port=3306;User Id='root';Password='!Qaz2wSX';charset='utf8';pooling=true").Options);
        }
    }
}
