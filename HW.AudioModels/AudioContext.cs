using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.AudioModels
{
    public partial class AudioContext : DbContext
    {
        public AudioContext()
        {
        }

        public AudioContext(DbContextOptions<AudioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BidAudio> BidAudio { get; set; }
        public virtual DbSet<DisputeAudio> DisputeAudio { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BidAudio>(entity =>
            {
                entity.Property(e => e.Audio).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DisputeAudio>(entity =>
            {
                entity.Property(e => e.Audio).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });
        }
    }
}
