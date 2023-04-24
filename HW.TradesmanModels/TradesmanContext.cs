using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class TradesmanContext : DbContext
    {
        public TradesmanContext()
        {
        }

        public TradesmanContext(DbContextOptions<TradesmanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MetaTag> MetaTags { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillSet> SkillSets { get; set; }
        public virtual DbSet<SubSkill> SubSkills { get; set; }
        public virtual DbSet<Tradesman> Tradesmen { get; set; }
        public virtual DbSet<TradesmanAd> TradesmanAds { get; set; }
        public virtual DbSet<TradesmanRegBySalesman> TradesmanRegBySalesmen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MetaTag>(entity =>
            {
                entity.Property(e => e.MetaTagId).ValueGeneratedNever();

                entity.Property(e => e.Content).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MetaTags).HasMaxLength(1000);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OgTitle).HasMaxLength(200);

                entity.Property(e => e.SeoPageTitle).HasMaxLength(200);

                entity.Property(e => e.SkillIconPath).HasMaxLength(50);

                entity.Property(e => e.SkillTitle).HasMaxLength(100);

                entity.Property(e => e.Slug).HasMaxLength(100);
            });

            modelBuilder.Entity<SkillSet>(entity =>
            {
                entity.ToTable("SkillSet");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubSkill>(entity =>
            {
                entity.ToTable("SubSkill");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.MetaTags)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Slug)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubSkillPrice).HasColumnType("money");

                entity.Property(e => e.SubSkillTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VisitCharges).HasColumnType("money");
            });

            modelBuilder.Entity<Tradesman>(entity =>
            {
                entity.ToTable("Tradesman");

                entity.Property(e => e.AddressLine).HasMaxLength(250);

                entity.Property(e => e.Area).HasMaxLength(30);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasMaxLength(20)
                    .HasColumnName("CNIC");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CompanyRegNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailAddress).HasMaxLength(40);

                entity.Property(e => e.FirstName).HasMaxLength(18);

                entity.Property(e => e.GpsCoordinates).HasMaxLength(100);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsOrganization).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).HasMaxLength(18);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PublicId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CCityCodeId')");

                entity.Property(e => e.ShopAddress).HasMaxLength(250);

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<TradesmanAd>(entity =>
            {
                entity.HasKey(e => e.TradesmanAdsId);

                entity.Property(e => e.ActiveFrom).HasColumnType("datetime");

                entity.Property(e => e.ActiveTo).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanRegBySalesman>(entity =>
            {
                entity.ToTable("TradesmanRegBySalesman");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
