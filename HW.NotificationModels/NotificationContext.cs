using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.NotificationModels
{
    public partial class NotificationContext : DbContext
    {
        public NotificationContext()
        {
        }

        public NotificationContext(DbContextOptions<NotificationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NotificationLogging> NotificationLogging { get; set; }
        public virtual DbSet<NotificationRole> NotificationRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<NotificationLogging>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsRecived).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PayLoad).IsUnicode(false);

                entity.Property(e => e.ReadAt).HasColumnType("datetime");

                entity.Property(e => e.ReasonToAbort).HasMaxLength(100);

                entity.Property(e => e.ReceivedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<NotificationRole>(entity =>
            {
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
