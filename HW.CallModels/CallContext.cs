using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.CallModels
{
    public partial class CallContext : DbContext
    {
        public CallContext()
        {
        }

        public CallContext(DbContextOptions<CallContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CallType> CallType { get; set; }
        public virtual DbSet<EstateAgentCallLog> EstateAgentCallLog { get; set; }
        public virtual DbSet<SupplierCallLog> SupplierCallLog { get; set; }
        public virtual DbSet<TradesmanCallLog> TradesmanCallLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<CallType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EstateAgentCallLog>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ToCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SupplierCallLog>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ToCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TradesmanCallLog>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });
        }
    }
}
