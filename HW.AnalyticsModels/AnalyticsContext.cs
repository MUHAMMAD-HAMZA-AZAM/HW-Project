using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.AnalyticsModels
{
    public partial class AnalyticsContext : DbContext
    {
        public AnalyticsContext()
        {
        }

        public AnalyticsContext(DbContextOptions<AnalyticsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Analytics> Analytics { get; set; }
        //public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Os> Os { get; set; }
        public virtual DbSet<VersionControl> VersionControl { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Analytics>(entity =>
            {
                entity.Property(e => e.ApplicaitonVersion)
                    .HasMaxLength(50);
                    //.HasDefaultValueSql("('1.0.0')");

                entity.Property(e => e.ApplicationType)
                    .HasMaxLength(20);

                entity.Property(e => e.Browser).HasMaxLength(50);

                entity.Property(e => e.BrowserVersion).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CountryCapital).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50);

                //entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Device)
                    .HasMaxLength(50);

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.Ip)
                    .HasMaxLength(20);

                entity.Property(e => e.IpLocation)
                    .HasMaxLength(50);

                entity.Property(e => e.Os)
                    .HasMaxLength(20);

                entity.Property(e => e.OsVersion)
                    .HasMaxLength(20);

                entity.Property(e => e.Platform)
  
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Application1)
                    .IsRequired()
                    //.HasColumnName("Application")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Os>(entity =>
            {
                //entity.ToTable("OS");

                entity.Property(e => e.Os1)
                    .IsRequired()
                    //.HasColumnName("OS")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VersionControl>(entity =>
            {
                //entity.Property(e => e.Os).HasColumnName("OS");

                entity.Property(e => e.VersionCode)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
