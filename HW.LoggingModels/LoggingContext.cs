using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.LoggingModels
{
    public partial class LoggingContext : DbContext
    {
        public LoggingContext()
        {
        }

        public LoggingContext(DbContextOptions<LoggingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExceptionLogs> ExceptionLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 192.168.100.40,15751; Initial Catalog = Logging; User ID = sa; Password = @Syst123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<ExceptionLogs>(entity =>
            {
                entity.HasKey(e => e.ExceptionId);

                entity.Property(e => e.CpuType).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.ErrorMessage).HasMaxLength(2000);

                entity.Property(e => e.ErrorScreen).HasMaxLength(150);

                entity.Property(e => e.ErrorType).HasMaxLength(100);

                entity.Property(e => e.MobleModel).HasMaxLength(100);

                entity.Property(e => e.OsVersion).HasMaxLength(100);
            });
        }
    }
}
