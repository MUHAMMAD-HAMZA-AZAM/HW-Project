using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.CommunicationModels
{
    public partial class CommunicationContext : DbContext
    {
        public CommunicationContext()
        {
        }

        public CommunicationContext(DbContextOptions<CommunicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CallRequest> CallRequest { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailType> EmailType { get; set; }
        public virtual DbSet<InappChat> InappChat { get; set; }
        public virtual DbSet<Otpsms> Otpsms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 110.39.179.18,15751; Initial Catalog = Communication; User ID = sa; Password =@Syst123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CallRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.BccEmail).HasMaxLength(40);

                entity.Property(e => e.CcEmails).HasMaxLength(40);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextRetriedDate).HasColumnType("datetime");

                entity.Property(e => e.RecieverEmailId).HasMaxLength(40);

                entity.Property(e => e.RetriedDate).HasColumnType("datetime");

                entity.Property(e => e.SenderEmailId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.SentOn).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.Property(e => e.EmailTypeId).ValueGeneratedNever();

                entity.Property(e => e.EmailType1)
                    .IsRequired()
                    .HasColumnName("EmailType")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<InappChat>(entity =>
            {
                entity.Property(e => e.ChatKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChatRoom)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Otpsms>(entity =>
            {
                entity.HasKey(e => e.SmsId);

                entity.ToTable("OTPSms");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(150);

                entity.Property(e => e.RecieverMobileNumber).HasMaxLength(15);

                entity.Property(e => e.SentOn).HasColumnType("datetime");
            });
        }
    }
}
