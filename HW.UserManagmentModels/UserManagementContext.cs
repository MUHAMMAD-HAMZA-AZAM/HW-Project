using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.UserManagmentModels
{
    public partial class UserManagementContext : DbContext
    {
        public UserManagementContext()
        {
        }

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivePromotion> ActivePromotion { get; set; }
        public virtual DbSet<Agreements> Agreements { get; set; }
        public virtual DbSet<ApplicationSetting> ApplicationSetting { get; set; }
        public virtual DbSet<ApplicationSettingDetail> ApplicationSettingDetail { get; set; }
        public virtual DbSet<Campaigns> Campaigns { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Distance> Distance { get; set; }
        public virtual DbSet<Faqs> Faqs { get; set; }
        public virtual DbSet<OneTimePassword> OneTimePassword { get; set; }
        public virtual DbSet<Salesman> Salesman { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<TooltipForm> TooltipForm { get; set; }
        public virtual DbSet<TooltipFormDetail> TooltipFormDetail { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<TestimonialsType>TestimonialsTypes { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 110.39.179.18,15751; Initial Catalog = UserManagement; User ID = sa; Password = @Syst123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ActivePromotion>(entity =>
            {
                entity.HasKey(e => e.PromotionId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.PromotionEndDate)
                    .HasColumnName("promotionEndDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PromotionStartDate)
                    .HasColumnName("promotionStartDate")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Agreements>(entity =>
            {
                entity.HasKey(e => e.AgreementId);

                entity.Property(e => e.AgreementsText).HasMaxLength(1000);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Header).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SettingName).HasMaxLength(255);
            });

            modelBuilder.Entity<ApplicationSettingDetail>(entity =>
            {
                entity.HasKey(e => e.ApplictaionSettingDetailId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SettingKeyName).HasMaxLength(100);

                entity.Property(e => e.SettingKeyValue).HasMaxLength(100);
            });

            modelBuilder.Entity<Campaigns>(entity =>
            {
                entity.HasKey(e => e.CampaignId)
                    .HasName("PK_Compaigns");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<Distance>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Faqs>(entity =>
            {
                entity.HasKey(e => e.FaqId)
                    .HasName("PK_FAQs");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FaqsText).HasMaxLength(1000);

                entity.Property(e => e.Header).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<OneTimePassword>(entity =>
            {
                entity.HasKey(e => e.OtpId)
                    .HasName("PK_Otp");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SecretKey).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Salesman>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MobifiedBy).HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TooltipForm>(entity =>
            {
                entity.HasKey(e => e.FormId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TooltipFormDetail>(entity =>
            {
                entity.HasKey(e => e.FormDetailId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.CityId).HasDefaultValueSql("('64')");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(60);
            });
            modelBuilder.Entity<TestimonialsType>(entity =>
            {
                entity.HasKey(e => e.Id)
                 .HasName("PK_TestimonialsTypes");
                entity.Property(e => e.Type).HasMaxLength(50);

            });

            modelBuilder.Entity<Testimonial> (entity =>
            {
                entity.HasKey(e => e.TestimonialsId)
                    .HasName("PK_Testimonials");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(150);

                entity.Property(e => e.Active).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserType);

                entity.Property(e => e.Testimonialtype);
    

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });
        }
    }
}
