using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.PromotionsModels
{
    public partial class PromotionsContext : DbContext
    {
        public PromotionsContext()
        {
        }

        public PromotionsContext(DbContextOptions<PromotionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Leafs> Leafs { get; set; }
        public virtual DbSet<PromotionRedemptions> PromotionRedemptions { get; set; }
        public virtual DbSet<Redemptions> Redemptions { get; set; }
        public virtual DbSet<ReferalCode> ReferalCode { get; set; }
        public virtual DbSet<Referral> Referral { get; set; }
        public virtual DbSet<ReferralDetails> ReferralDetails { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 110.39.179.18,15751; Initial Catalog = Promotions; User ID = sa; Password =@Syst123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.AssignedOn)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.BookCode)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.AssignedOn).HasColumnType("datetime");

                entity.Property(e => e.AssignedTo)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Leafs>(entity =>
            {
                entity.HasKey(e => e.LeafId)
                    .HasName("PK__Leafs__7363144B5286A72F");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.RedeemedBy).HasMaxLength(450);

                entity.Property(e => e.RedeemedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<PromotionRedemptions>(entity =>
            {
                entity.Property(e => e.RedeemBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RedeemOn).HasColumnType("datetime");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Redemptions>(entity =>
            {
                entity.Property(e => e.RedeemBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RedeemOn).HasColumnType("datetime");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ReferalCode>(entity =>
            {
                entity.HasKey(e => e.ReferralId)
                    .HasName("PK_Referals");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReferralCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReferredUser)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RefferalAmount).HasColumnType("money");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.EndedAt).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StartingFrom).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReferralDetails>(entity =>
            {
                entity.HasKey(e => e.ReferralDetailId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.VoucherId).ValueGeneratedNever();

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidUntil).HasColumnType("datetime");

                entity.Property(e => e.VoucherCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.VoucherDiscount).HasColumnType("decimal(18, 0)");
            });
        }
    }
}
