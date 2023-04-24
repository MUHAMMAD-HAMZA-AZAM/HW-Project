using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.ImageModels
{
    public partial class ImageContext : DbContext
    {
        public ImageContext()
        {
        }

        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogImage> BlogImage { get; set; }
        public virtual DbSet<CustomerProfileImage> CustomerProfileImage { get; set; }
        public virtual DbSet<DisputeImages> DisputeImages { get; set; }
        public virtual DbSet<FeaturedSupplierImages> FeaturedSupplierImages { get; set; }
        public virtual DbSet<IdTypeImage> IdTypeImage { get; set; }
        public virtual DbSet<JobImages> JobImages { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<RsaAdImage> RsaAdImage { get; set; }
        public virtual DbSet<RsaNicImage> RsaNicImage { get; set; }
        public virtual DbSet<RsaTcImage> RsaTcImage { get; set; }
        public virtual DbSet<SupplierAdImage> SupplierAdImage { get; set; }
        public virtual DbSet<SupplierLogos> SupplierLogos { get; set; }
        public virtual DbSet<SupplierNicImage> SupplierNicImage { get; set; }
        public virtual DbSet<SupplierPcImage> SupplierPcImage { get; set; }
        public virtual DbSet<SupplierProductImage> SupplierProductImage { get; set; }
        public virtual DbSet<SupplierProfileImage> SupplierProfileImage { get; set; }
        public virtual DbSet<TradesmanAdImage> TradesmanAdImage { get; set; }
        public virtual DbSet<TradesmanNicImage> TradesmanNicImage { get; set; }
        public virtual DbSet<TradesmanProfileImage> TradesmanProfileImage { get; set; }
        public virtual DbSet<TradesmanSkillImage> TradesmanSkillImage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BlogImage>(entity =>
            {
                entity.Property(e => e.BlogImage1)
                    .IsRequired()
                    .HasColumnName("BlogImage");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerProfileImage>(entity =>
            {
                entity.HasKey(e => e.ProfileImageId)
                    .HasName("PK_ProfileImage_1");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProfileImage).IsRequired();
            });

            modelBuilder.Entity<DisputeImages>(entity =>
            {
                entity.HasKey(e => e.DisputeImageId)
                    .HasName("PK_DisputeImage");

                entity.Property(e => e.BidImage).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<FeaturedSupplierImages>(entity =>
            {
                entity.HasKey(e => e.ImageId);
            });

            modelBuilder.Entity<IdTypeImage>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobImages>(entity =>
            {
                entity.HasKey(e => e.BidImageId)
                    .HasName("PK_Image");

                entity.Property(e => e.BidImage).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductImages>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.FilePath).HasMaxLength(250);
            });

            modelBuilder.Entity<RsaAdImage>(entity =>
            {
                entity.HasKey(e => e.AdImageId)
                    .HasName("PK__RsaAdIma__A736F609D874AB9F");

                entity.Property(e => e.AdImage).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<RsaNicImage>(entity =>
            {
                entity.HasKey(e => e.NicImageId)
                    .HasName("PK__RsaNicIm__D594F798FD309AD9");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NicImage).IsRequired();
            });

            modelBuilder.Entity<RsaTcImage>(entity =>
            {
                entity.HasKey(e => e.TcImageId)
                    .HasName("PK__RsaTcIma__6B5B0E2FC80AB145");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Tcimage)
                    .IsRequired()
                    .HasColumnName("TCImage");
            });

            modelBuilder.Entity<SupplierAdImage>(entity =>
            {
                entity.HasKey(e => e.AdImageId)
                    .HasName("PK__Supplier__A736F609AF68FCB4");

                entity.Property(e => e.AdImage).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Test).HasColumnName("test");
            });

            modelBuilder.Entity<SupplierLogos>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierNicImage>(entity =>
            {
                entity.HasKey(e => e.NicImageId)
                    .HasName("PK__Supplier__D594F7989E0D0472");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NicImage).IsRequired();
            });

            modelBuilder.Entity<SupplierPcImage>(entity =>
            {
                entity.HasKey(e => e.PcimageId)
                    .HasName("PK__Supplier__66DF1287F7D7310E");

                entity.Property(e => e.PcimageId).HasColumnName("PCImageId");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Pcimage)
                    .IsRequired()
                    .HasColumnName("PCImage");
            });

            modelBuilder.Entity<SupplierProductImage>(entity =>
            {
                entity.HasKey(e => e.ProductImageId)
                    .HasName("PK__Supplier__07B2B1B8CDC6EB24");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProductImage).IsRequired();
            });

            modelBuilder.Entity<SupplierProfileImage>(entity =>
            {
                entity.HasKey(e => e.ProfileImageId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanAdImage>(entity =>
            {
                entity.HasKey(e => e.AdImageId)
                    .HasName("PK__Tradesma__A736F60963B96817");

                entity.Property(e => e.AdImage).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanNicImage>(entity =>
            {
                entity.HasKey(e => e.NicImageId)
                    .HasName("PK__Tradesma__D594F798C5C6DC27");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NicImage).IsRequired();
            });

            modelBuilder.Entity<TradesmanProfileImage>(entity =>
            {
                entity.HasKey(e => e.ProfileImageId)
                    .HasName("PK_ProfileImage");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanSkillImage>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SkillImage).IsRequired();
            });
        }
    }
}
