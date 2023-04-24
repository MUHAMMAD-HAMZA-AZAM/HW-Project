using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.JobModels
{
    public partial class JobContext : DbContext
    {
        public JobContext()
        {
        }

        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bids> Bids { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<CsjobRemarks> CsjobRemarks { get; set; }
        public virtual DbSet<CsjobStatus> CsjobStatus { get; set; }
        public virtual DbSet<Dispute> Dispute { get; set; }
        public virtual DbSet<DisputeStatus> DisputeStatus { get; set; }
        public virtual DbSet<EsclateOption> EsclateOption { get; set; }
        public virtual DbSet<EsclateRequest> EsclateRequest { get; set; }
        public virtual DbSet<FacebookLeads> FacebookLeads { get; set; }
        public virtual DbSet<FavoriteTradesman> FavoriteTradesman { get; set; }
        public virtual DbSet<JobAddress> JobAddress { get; set; }
        public virtual DbSet<JobAuthorizer> JobAuthorizer { get; set; }
        public virtual DbSet<JobContactInfo> JobContactInfo { get; set; }
        public virtual DbSet<JobDetail> JobDetail { get; set; }
        public virtual DbSet<JobFeedback> JobFeedback { get; set; }
        public virtual DbSet<JobHistory> JobHistory { get; set; }
        public virtual DbSet<JobPrice> JobPrice { get; set; }
        public virtual DbSet<JobPropertyRelationShip> JobPropertyRelationShip { get; set; }
        public virtual DbSet<JobQuotation> JobQuotation { get; set; }
        public virtual DbSet<JobTown> JobTown { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SupplierFeedback> SupplierFeedback { get; set; }
        public virtual DbSet<TradesmanFeedback> TradesmanFeedback { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Bids>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradesmanName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CsjobRemarks>(entity =>
            {
                entity.HasKey(e => e.RemarksId);

                entity.ToTable("CSJobRemarks");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CsjobStatus>(entity =>
            {
                entity.ToTable("CSJobStatus");

                entity.Property(e => e.CsjobStatusId).HasColumnName("CSJobStatusId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CsjobStatusName)
                    .HasColumnName("CSJobStatusName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Dispute>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DisputeStatus>(entity =>
            {
                entity.Property(e => e.DisputeStatusId).ValueGeneratedNever();

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

            modelBuilder.Entity<EsclateOption>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EsclateRequest>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Approve).HasDefaultValueSql("((0))");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<FavoriteTradesman>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifyBy).HasMaxLength(50);

                entity.Property(e => e.ModifyOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobAddress>(entity =>
            {
                entity.Property(e => e.AddressLine).HasMaxLength(250);

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.GpsCoordinates)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<JobAuthorizer>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<JobContactInfo>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.PropertyRelationship)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<JobDetail>(entity =>
            {
                entity.Property(e => e.AdditionalCharges).HasColumnType("money");

                entity.Property(e => e.Budget).HasColumnType("money");

                entity.Property(e => e.ChargesDescription).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerMessage).HasMaxLength(300);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedCommission).HasColumnType("money");

                entity.Property(e => e.MaterialCharges).HasColumnType("money");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OtherCharges).HasColumnType("money");

                entity.Property(e => e.ServiceCharges).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TotalJobValue).HasColumnType("money");

                entity.Property(e => e.TradesmanBudget).HasColumnType("money");
            });

            modelBuilder.Entity<JobFeedback>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ToCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<JobHistory>(entity =>
            {
                entity.Property(e => e.DeletedBy).HasMaxLength(50);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.JobQuotationId).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<JobPrice>(entity =>
            {
                entity.HasKey(e => e.PriceId);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobPropertyRelationShip>(entity =>
            {
                entity.HasKey(e => e.PropertyRelationshipId)
                    .HasName("PK_PropertyRelationShip");

                entity.Property(e => e.PropertyRelationshipName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobQuotation>(entity =>
            {
                entity.Property(e => e.AuthorizeJob).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CsjobStatusId).HasColumnName("CSJobStatusId");

                entity.Property(e => e.CustomerMessage).HasMaxLength(300);

                entity.Property(e => e.EstimatedCommission).HasColumnType("money");

                entity.Property(e => e.GpsCoordinates).HasMaxLength(100);

                entity.Property(e => e.JobAddress).HasMaxLength(250);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OtherCharges).HasColumnType("money");

                entity.Property(e => e.ServiceCharges).HasColumnType("money");

                entity.Property(e => e.VisitCharges).HasColumnType("money");

                entity.Property(e => e.WorkBudget).HasColumnType("money");

                entity.Property(e => e.WorkDescription).HasMaxLength(150);

                entity.Property(e => e.WorkStartDate).HasColumnType("datetime");

                entity.Property(e => e.WorkTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<JobTown>(entity =>
            {
                entity.HasKey(e => e.TownId);

                entity.Property(e => e.TownName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.MethodName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("date");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierFeedback>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanFeedback>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });
        }
    }
}
