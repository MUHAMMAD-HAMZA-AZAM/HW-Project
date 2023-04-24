using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.EstateAgentModels
{
    public partial class EstateAgentContext : DbContext
    {
        public EstateAgentContext()
        {
        }

        public EstateAgentContext(DbContextOptions<EstateAgentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EstateAgent> EstateAgent { get; set; }
        public virtual DbSet<EstateAgentAds> EstateAgentAds { get; set; }
        public virtual DbSet<EstateAgentProperties> EstateAgentProperties { get; set; }
        public virtual DbSet<EstateAgentTradeCategory> EstateAgentTradeCategory { get; set; }
        public virtual DbSet<EstateAgentTrades> EstateAgentTrades { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstateAgent>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.AgencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NoOfEmployees).HasMaxLength(20);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<EstateAgentAds>(entity =>
            {
                entity.Property(e => e.ActiveFrom).HasColumnType("datetime");

                entity.Property(e => e.ActiveTo).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EstateAgentProperties>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AreaAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PlotSize)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Town).HasMaxLength(30);
            });

            modelBuilder.Entity<EstateAgentTradeCategory>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EstateAgentTrades>(entity =>
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
