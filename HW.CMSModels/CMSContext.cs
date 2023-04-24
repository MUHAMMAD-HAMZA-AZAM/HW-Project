using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HW.CMSModels
{
    public partial class CMSContext : DbContext
    {
        public CMSContext()
        {
        }

        public CMSContext(DbContextOptions<CMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<PagesSeo> PagesSeos { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostStatus> PostStatuses { get; set; }
        public virtual DbSet<SitePage> SitePages { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Createdy).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Slug).HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentContent).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Createdy).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Createdy).HasMaxLength(50);

                entity.Property(e => e.ManuName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuDiscription).HasMaxLength(150);

                entity.Property(e => e.MenuTarget).HasMaxLength(100);

                entity.Property(e => e.MenuUrl).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<PagesSeo>(entity =>
            {
                entity.HasKey(e => e.PageId);

                entity.ToTable("PagesSeo");

                entity.Property(e => e.Canonical).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Keywords).HasMaxLength(1000);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OgTitle).HasMaxLength(500);

                entity.Property(e => e.PageTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MetaTags).HasMaxLength(1000);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PostTitle).HasMaxLength(300);

                entity.Property(e => e.Slug).HasMaxLength(300);

                entity.Property(e => e.Summary).HasMaxLength(1000);

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<PostStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("PostStatus");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<SitePage>(entity =>
            {
                entity.HasKey(e => e.PageId)
                    .HasName("PK_SeoPages");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PageName).HasMaxLength(50);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Createdy).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Slug).HasMaxLength(50);

                entity.Property(e => e.SubCategoryName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
