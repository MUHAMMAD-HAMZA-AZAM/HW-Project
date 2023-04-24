using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.PackagesAndPaymentsModels
{
    public partial class PackagesAndPaymentsContext : DbContext
    {
        public PackagesAndPaymentsContext()
        {
        }

        public PackagesAndPaymentsContext(DbContextOptions<PackagesAndPaymentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountCategory> AccountCategory { get; set; }
        public virtual DbSet<AccountReceivablePayment> AccountReceivablePayment { get; set; }
        public virtual DbSet<AccountReceivablePaymentDetail> AccountReceivablePaymentDetail { get; set; }
        public virtual DbSet<AccountReceivablePaymentSummary> AccountReceivablePaymentSummary { get; set; }
        public virtual DbSet<AccountSubCategory> AccountSubCategory { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Arpayments> Arpayments { get; set; }
        public virtual DbSet<CategoriesPerOrder> CategoriesPerOrder { get; set; }
        public virtual DbSet<CategoryCommissionSetup> CategoryCommissionSetup { get; set; }
        public virtual DbSet<CouponTypes> CouponTypes { get; set; }
        public virtual DbSet<Coupons> Coupons { get; set; }
        public virtual DbSet<FiscalPeriod> FiscalPeriod { get; set; }
        public virtual DbSet<JazzCashAcknowledgementReceipt> JazzCashAcknowledgementReceipt { get; set; }
        public virtual DbSet<JazzCashMerchantDetails> JazzCashMerchantDetails { get; set; }
        public virtual DbSet<JazzcashSentRequest> JazzcashSentRequest { get; set; }
        public virtual DbSet<JournalEntryHeader> JournalEntryHeader { get; set; }
        public virtual DbSet<JournalEntryLine> JournalEntryLine { get; set; }
        public virtual DbSet<LeadgerTransection> LeadgerTransection { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<PromotionOnPackages> PromotionOnPackages { get; set; }
        public virtual DbSet<PromotionRedemptions> PromotionRedemptions { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<PromotionsTypes> PromotionsTypes { get; set; }
        public virtual DbSet<Redemptions> Redemptions { get; set; }
        public virtual DbSet<ReferalCode> ReferalCode { get; set; }
        public virtual DbSet<Referral> Referral { get; set; }
        public virtual DbSet<ReferralDetails> ReferralDetails { get; set; }
        public virtual DbSet<SubAccount> SubAccount { get; set; }
        public virtual DbSet<SupplierPromotions> SupplierPromotions { get; set; }
        public virtual DbSet<SupplierPromotionsTypes> SupplierPromotionsTypes { get; set; }
        public virtual DbSet<TradesmanCommissionOverride> TradesmanCommissionOverride { get; set; }
        public virtual DbSet<TradesmanJobReceipts> TradesmanJobReceipts { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetail { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherBook> VoucherBook { get; set; }
        public virtual DbSet<VoucherBookAllocation> VoucherBookAllocation { get; set; }
        public virtual DbSet<VoucherBookLeaves> VoucherBookLeaves { get; set; }
        public virtual DbSet<VoucherBookPages> VoucherBookPages { get; set; }
        public virtual DbSet<VoucherCategory> VoucherCategory { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }
        public virtual DbSet<WithdrawalRequest> WithdrawalRequest { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "GL");

                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.AccountName).HasMaxLength(50);

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccountCategory>(entity =>
            {
                entity.ToTable("AccountCategory", "GL");

                entity.Property(e => e.AccountCategoryCode).HasMaxLength(50);

                entity.Property(e => e.AccountCategoryName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccountReceivablePayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalDeliveryCost).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.TotalOrderCost).HasColumnType("decimal(20, 2)");
            });

            modelBuilder.Entity<AccountReceivablePaymentDetail>(entity =>
            {
                entity.HasKey(e => e.PaymentDetailId);

                entity.Property(e => e.ItemCost).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ItemDeliveryCost).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.TotalItemCost).HasColumnType("decimal(20, 2)");
            });

            modelBuilder.Entity<AccountReceivablePaymentSummary>(entity =>
            {
                entity.HasKey(e => e.PaymentSummaryId);

                entity.Property(e => e.TotalDeliveryPayment).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.TotalItemPayment).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.TotalPayment).HasColumnType("decimal(20, 2)");
            });

            modelBuilder.Entity<AccountSubCategory>(entity =>
            {
                entity.ToTable("AccountSubCategory", "GL");

                entity.Property(e => e.AccountSubCategoryCode).HasMaxLength(50);

                entity.Property(e => e.AccountSubCategoryName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType", "GL");

                entity.Property(e => e.AccounTypeName).HasMaxLength(50);

                entity.Property(e => e.AccountTypeCode).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Arpayments>(entity =>
            {
                entity.HasKey(e => e.ArpaymentId)
                    .HasName("PK__ARPaymen__FCA28DB17EAE0D9E");

                entity.ToTable("ARPayments");

                entity.Property(e => e.ArpaymentId).HasColumnName("ARPaymentId");

                entity.Property(e => e.Amount).HasColumnType("decimal(15, 6)");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentStatus)
                    .HasColumnName("Payment_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CategoriesPerOrder>(entity =>
            {
                entity.HasKey(e => e.CatagoryPerOrderId)
                    .HasName("PK__Categori__0109B93772C12551");

                entity.Property(e => e.AspnetUserId).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CategoryCommissionSetup>(entity =>
            {
                entity.Property(e => e.CommisionAmount).HasColumnType("money");

                entity.Property(e => e.CommissionEndDate).HasColumnType("datetime");

                entity.Property(e => e.CommissionPercentage).HasColumnType("money");

                entity.Property(e => e.CommissionStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CouponTypes>(entity =>
            {
                entity.HasKey(e => e.CouponTypeId)
                    .HasName("PK__CouponTy__095BED9B24EA9C20");

                entity.Property(e => e.CouponTypeCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CouponTypeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Coupons>(entity =>
            {
                entity.HasKey(e => e.CouponId)
                    .HasName("PK__Coupons__384AF1BA9EE06936");

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CouponIdGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CouponeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FiscalPeriod>(entity =>
            {
                entity.ToTable("FiscalPeriod", "GL");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JazzCashAcknowledgementReceipt>(entity =>
            {
                entity.ToTable("JazzCashAcknowledgementReceipt", "Logs");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AuthCode).HasMaxLength(100);

                entity.Property(e => e.BankId)
                    .HasColumnName("BankID")
                    .HasMaxLength(100);

                entity.Property(e => e.BillReference).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Language).HasMaxLength(100);

                entity.Property(e => e.MerchantId)
                    .HasColumnName("MerchantID")
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ppmbf1)
                    .HasColumnName("ppmbf_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf2)
                    .HasColumnName("ppmbf_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf3)
                    .HasColumnName("ppmbf_3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf4)
                    .HasColumnName("ppmbf_4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf5)
                    .HasColumnName("ppmbf_5")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf1)
                    .HasColumnName("ppmpf_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf2)
                    .HasColumnName("ppmpf_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf3)
                    .HasColumnName("ppmpf_3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf4)
                    .HasColumnName("ppmpf_4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf5)
                    .HasColumnName("ppmpf_5")
                    .HasMaxLength(100);

                entity.Property(e => e.ResponseCode).HasMaxLength(100);

                entity.Property(e => e.ResponseMessage).HasMaxLength(100);

                entity.Property(e => e.RetreivalReferenceNo).HasMaxLength(100);

                entity.Property(e => e.SecureHash).HasMaxLength(100);

                entity.Property(e => e.SettlementExpiry).HasMaxLength(100);

                entity.Property(e => e.SubMerchantId).HasMaxLength(100);

                entity.Property(e => e.TxnCurrency).HasMaxLength(100);

                entity.Property(e => e.TxnDateTime).HasMaxLength(100);

                entity.Property(e => e.TxnRefNo).HasMaxLength(100);

                entity.Property(e => e.TxnType).HasMaxLength(100);

                entity.Property(e => e.Version).HasMaxLength(100);
            });

            modelBuilder.Entity<JazzCashMerchantDetails>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.PpAmount)
                    .HasColumnName("pp_Amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PpBankId)
                    .HasColumnName("pp_BankID")
                    .HasMaxLength(20);

                entity.Property(e => e.PpBillReference)
                    .HasColumnName("pp_BillReference")
                    .HasMaxLength(20);

                entity.Property(e => e.PpDescription)
                    .HasColumnName("pp_Description")
                    .HasMaxLength(500);

                entity.Property(e => e.PpLanguage)
                    .HasColumnName("pp_Language")
                    .HasMaxLength(5);

                entity.Property(e => e.PpMerchantId)
                    .HasColumnName("pp_MerchantID")
                    .HasMaxLength(20);

                entity.Property(e => e.PpPassword)
                    .HasColumnName("pp_Password")
                    .HasMaxLength(20);

                entity.Property(e => e.PpProductId)
                    .HasColumnName("pp_ProductID")
                    .HasMaxLength(10);

                entity.Property(e => e.PpReturnUrl)
                    .HasColumnName("pp_ReturnURL")
                    .HasMaxLength(250);

                entity.Property(e => e.PpSecureHash)
                    .HasColumnName("pp_SecureHash")
                    .HasMaxLength(250);

                entity.Property(e => e.PpSubMerchantId)
                    .HasColumnName("pp_SubMerchantID")
                    .HasMaxLength(20);

                entity.Property(e => e.PpTxnCurrency)
                    .HasColumnName("pp_TxnCurrency")
                    .HasMaxLength(5);

                entity.Property(e => e.PpTxnDateTime)
                    .HasColumnName("pp_TxnDateTime")
                    .HasMaxLength(20);

                entity.Property(e => e.PpTxnExpiryDateTime)
                    .HasColumnName("pp_TxnExpiryDateTime")
                    .HasMaxLength(20);

                entity.Property(e => e.PpTxnRefNo)
                    .HasColumnName("pp_TxnRefNo")
                    .HasMaxLength(20);

                entity.Property(e => e.PpTxnType)
                    .HasColumnName("pp_TxnType")
                    .HasMaxLength(10);

                entity.Property(e => e.PpVersion)
                    .HasColumnName("pp_Version")
                    .HasMaxLength(5);

                entity.Property(e => e.Ppmpf1)
                    .HasColumnName("ppmpf_1")
                    .HasMaxLength(10);

                entity.Property(e => e.Ppmpf2)
                    .HasColumnName("ppmpf_2")
                    .HasMaxLength(10);

                entity.Property(e => e.Ppmpf3)
                    .HasColumnName("ppmpf_3")
                    .HasMaxLength(10);

                entity.Property(e => e.Ppmpf4)
                    .HasColumnName("ppmpf_4")
                    .HasMaxLength(10);

                entity.Property(e => e.Ppmpf5)
                    .HasColumnName("ppmpf_5")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<JazzcashSentRequest>(entity =>
            {
                entity.ToTable("JazzcashSentRequest", "Logs");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AuthCode).HasMaxLength(100);

                entity.Property(e => e.BankId)
                    .HasColumnName("BankID")
                    .HasMaxLength(100);

                entity.Property(e => e.BillReference).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Language).HasMaxLength(100);

                entity.Property(e => e.MerchantId)
                    .HasColumnName("MerchantID")
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ppmbf1)
                    .HasColumnName("ppmbf_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf2)
                    .HasColumnName("ppmbf_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf3)
                    .HasColumnName("ppmbf_3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf4)
                    .HasColumnName("ppmbf_4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmbf5)
                    .HasColumnName("ppmbf_5")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf1)
                    .HasColumnName("ppmpf_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf2)
                    .HasColumnName("ppmpf_2")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf3)
                    .HasColumnName("ppmpf_3")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf4)
                    .HasColumnName("ppmpf_4")
                    .HasMaxLength(100);

                entity.Property(e => e.Ppmpf5)
                    .HasColumnName("ppmpf_5")
                    .HasMaxLength(100);

                entity.Property(e => e.ResponseCode).HasMaxLength(100);

                entity.Property(e => e.ResponseMessage).HasMaxLength(100);

                entity.Property(e => e.RetreivalReferenceNo).HasMaxLength(100);

                entity.Property(e => e.SecureHash).HasMaxLength(100);

                entity.Property(e => e.SettlementExpiry).HasMaxLength(100);

                entity.Property(e => e.SubMerchantId).HasMaxLength(100);

                entity.Property(e => e.TxnCurrency).HasMaxLength(100);

                entity.Property(e => e.TxnDateTime).HasMaxLength(100);

                entity.Property(e => e.TxnRefNo).HasMaxLength(100);

                entity.Property(e => e.TxnType).HasMaxLength(100);

                entity.Property(e => e.Version).HasMaxLength(100);
            });

            modelBuilder.Entity<JournalEntryHeader>(entity =>
            {
                entity.ToTable("JournalEntryHeader", "GL");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Narration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PostedDate).HasColumnType("datetime");

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JournalEntryLine>(entity =>
            {
                entity.ToTable("JournalEntryLine", "GL");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Debit).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.TaxCredit).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxDebit).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<LeadgerTransection>(entity =>
            {
                entity.ToTable("LeadgerTransection", "GL");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Credit).HasColumnType("money");

                entity.Property(e => e.Debit).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReffrenceDocumentType).HasMaxLength(100);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCFEC6E3596");

                entity.Property(e => e.AspnetUserId).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountPercentPrice).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.DiscountedTotalApplicableJobs).HasColumnName("Discounted_Total_Applicable_Jobs");

                entity.Property(e => e.DiscountedTotalCategories).HasColumnName("Discounted_Total_Categories");

                entity.Property(e => e.DiscountedValidityDays).HasColumnName("Discounted_Validity_Days");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTotal).HasColumnType("decimal(15, 6)");

                entity.Property(e => e.OriginalSalePrice).HasColumnType("decimal(15, 6)");

                entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(15, 6)");

                entity.Property(e => e.TotalApplicableJobs).HasColumnName("Total_Applicable_Jobs");

                entity.Property(e => e.TotalCategories).HasColumnName("Total_Categories");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValidityDays).HasColumnName("Validity_Days");
            });

            modelBuilder.Entity<PackageType>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus).HasMaxLength(15);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PackageTypeCode).HasMaxLength(10);

                entity.Property(e => e.PackageTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Packages>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("PK__Packages__322035CC5A556485");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PackageCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PackageName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SalePrice).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.TotalApplicableJobs).HasColumnName("Total_Applicable_Jobs");

                entity.Property(e => e.TotalCategories).HasColumnName("Total_Categories");

                entity.Property(e => e.TradePrice).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValidityDays).HasColumnName("Validity_Days");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.Property(e => e.PaymentStatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PromotionOnPackages>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountPercentPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountedTotalApplicableJobs).HasColumnName("Discounted_Total_Applicable_Jobs");

                entity.Property(e => e.DiscountedTotalCategories).HasColumnName("Discounted_Total_Categories");

                entity.Property(e => e.DiscountedValidityDays).HasColumnName("Discounted_Validity_Days");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.OriginalSalePrice).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.TotalApplicableJobs).HasColumnName("Total_Applicable_Jobs");

                entity.Property(e => e.TotalCategories).HasColumnName("Total_Categories");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValidityDays).HasColumnName("Validity_Days");
            });

            modelBuilder.Entity<PromotionRedemptions>(entity =>
            {
                entity.Property(e => e.RedeemBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RedeemOn).HasColumnType("datetime");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Promotions>(entity =>
            {
                entity.HasKey(e => e.PromotionId)
                    .HasName("PK__Promotio__52C42FCF853C2660");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountInAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountInCategories).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountInDays).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountInJobs).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountInPercentage).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountPercentPrice).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PermotionsForAll).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermotionsForNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermotionsForOld).HasDefaultValueSql("((0))");

                entity.Property(e => e.PromoStartDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionEndDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PromotionsTypes>(entity =>
            {
                entity.HasKey(e => e.PromotionTypeId)
                    .HasName("PK__Promotio__01055CE83F20B6B1");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Admin')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PromotionTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionTypeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
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
                entity.HasKey(e => e.ReferralId);

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
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

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

            modelBuilder.Entity<SubAccount>(entity =>
            {
                entity.ToTable("SubAccount", "GL");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.EntityStatus).HasMaxLength(1);

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SubAccountName).HasMaxLength(150);

                entity.Property(e => e.SubAccountNo).HasMaxLength(50);

                entity.Property(e => e.SupplierName).HasMaxLength(50);

                entity.Property(e => e.TradesmanName).HasMaxLength(50);
            });

            modelBuilder.Entity<SupplierPromotions>(entity =>
            {
                entity.HasKey(e => e.PromotionId)
                    .HasName("PK__Promotion__52C42FCF853C2660");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountPercentPrice).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PromoStartDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionEndDate).HasColumnType("datetime");

                entity.Property(e => e.PromotionName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SupplierPromotionsTypes>(entity =>
            {
                entity.HasKey(e => e.PromotionTypeId)
                    .HasName("PK__SupplierPromotionsTypes");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus)
                    .HasColumnName("Entity_Status")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PromotionTypeName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TradesmanCommissionOverride>(entity =>
            {
                entity.Property(e => e.CommissionEndDate).HasColumnType("datetime");

                entity.Property(e => e.CommissionOverrideAmount).HasColumnType("money");

                entity.Property(e => e.CommissionOverridePercentage).HasColumnType("decimal(3, 0)");

                entity.Property(e => e.CommissionStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReferencePerson)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TradesmanJobReceipts>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Commission).HasColumnType("money");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DiscountedAmount).HasColumnType("money");

                entity.Property(e => e.ModifiedBy).HasMaxLength(450);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NetPayableToTradesman).HasColumnType("money");

                entity.Property(e => e.OtherCharges).HasColumnType("money");

                entity.Property(e => e.PaidViaWallet).HasColumnType("money");

                entity.Property(e => e.PayableAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceCharges).HasColumnType("money");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InitiateTime).HasColumnType("datetime");

                entity.Property(e => e.MerchantId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseMessage)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RetreivalReferenceNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubMerchantId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidUntil).HasColumnType("datetime");

                entity.Property(e => e.VoucherCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.VoucherDiscount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<VoucherBook>(entity =>
            {
                entity.Property(e => e.BookLevelAmountDiscount).HasColumnType("money");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.VoucherBookName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherBookNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VoucherBookAllocation>(entity =>
            {
                entity.Property(e => e.AssigneeFirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AssigneeLastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmployDesignation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalDesignation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalPersonOccupation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoucherBookLeaves>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DiscountedAmount).HasColumnType("money");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PersentageDiscount).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.Property(e => e.VoucherNo)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<VoucherBookPages>(entity =>
            {
                entity.HasKey(e => e.VoucherbookPageId)
                    .HasName("PK__VoucherB__8CA61D5D0FA8BED8");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoucherCategory>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.VoucherCategoryCode)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherCategoryName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VoucherType>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.VoucherTypeCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherTypeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WithdrawalRequest>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.SupplierName).HasMaxLength(50);

                entity.Property(e => e.TradesmanName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
