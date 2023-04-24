using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.VideoModels
{
    public partial class VideoContext : DbContext
    {
        public VideoContext()
        {
        }

        public VideoContext(DbContextOptions<VideoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<JobQuotationVideo> JobQuotationVideo { get; set; }
        public virtual DbSet<SupplierAdVideos> SupplierAdVideos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<JobQuotationVideo>(entity =>
            {
                entity.HasKey(e => e.VideoId)
                    .HasName("PK_Video");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Video).IsRequired();
            });

            modelBuilder.Entity<SupplierAdVideos>(entity =>
            {
                entity.HasKey(e => e.AdVideoId)
                    .HasName("PK__Supplier__A736F609AF68FCB4");

                entity.Property(e => e.AdVideo).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.VideoName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
