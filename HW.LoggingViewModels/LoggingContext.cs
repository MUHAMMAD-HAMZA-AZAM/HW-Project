using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.LoggingViewModels
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<ExceptionLogs>(entity =>
            {
                entity.HasKey(e => e.ExceptionId);

                entity.Property(e => e.CpuType).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.ErrorScreen).IsUnicode(false);

                entity.Property(e => e.ErrorType).IsUnicode(false);

                entity.Property(e => e.MobleModel).IsUnicode(false);

                entity.Property(e => e.OsVersion).IsUnicode(false);
            });
        }
    }
}
