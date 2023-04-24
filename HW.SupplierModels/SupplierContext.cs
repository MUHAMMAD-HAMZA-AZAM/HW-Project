using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HW.SupplierModels
{
    public partial class SupplierContext : DbContext
    {
        public SupplierContext()
        {
        }

        public SupplierContext(DbContextOptions<SupplierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdsStatus> AdsStatus { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<CustomerFeedBack> CustomerFeedBacks { get; set; }
        public virtual DbSet<BankDetails> BankDetails { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<BulkOrdering> BulkOrdering { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<FeaturedSuppliers> FeaturedSuppliers { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderCancellationReason> OrderCancellationReason { get; set; }
        public virtual DbSet<OrderCancellationRequest> OrderCancellationRequest { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductCatalog> ProductCatalog { get; set; }
        public virtual DbSet<ProductCatalogAttribute> ProductCatalogAttribute { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductCategoryAttribute> ProductCategoryAttribute { get; set; }
        public virtual DbSet<ProductCategoryGroup> ProductCategoryGroup { get; set; }
        public virtual DbSet<ProductInventory> ProductInventory { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public virtual DbSet<ProductVariant> ProductVariant { get; set; }
        public virtual DbSet<ReturnAddress> ReturnAddress { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddress { get; set; }
        public virtual DbSet<ShippingCost> ShippingCost { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierAds> SupplierAds { get; set; }
        public virtual DbSet<SupplierOrders> SupplierOrders { get; set; }
        public virtual DbSet<SupplierProductTags> SupplierProductTags { get; set; }
        public virtual DbSet<SupplierRegBySalesman> SupplierRegBySalesman { get; set; }
        public virtual DbSet<SupplierRole> SupplierRole { get; set; }
        public virtual DbSet<SupplierSlider> SupplierSlider { get; set; }
        public virtual DbSet<SupplierSubCategory> SupplierSubCategory { get; set; }
        public virtual DbSet<WherehouseAddress> WherehouseAddress { get; set; }
        public virtual DbSet<SocialMediaLinks> SocialMediaLinks { get; set; }
        public virtual DbSet<FreeShipping> FreeShipping { get; set; }
        public virtual DbSet<SuppliersCommision> SuppliersCommision { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AdsStatus>(entity =>
            {
                entity.Property(e => e.AdsStatusId).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerFeedBack>(entity =>
            {
              entity.ToTable("CustomerFeedBack");

              entity.Property(e => e.CreatedBy).HasMaxLength(100);

              entity.Property(e => e.CreatedOn).HasColumnType("datetime");

              entity.Property(e => e.Description).HasMaxLength(250);

              entity.Property(e => e.ModifiedBy).HasMaxLength(100);

              entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BankDetails>(entity =>
            {
                entity.Property(e => e.AccountNumber).HasMaxLength(50);

                entity.Property(e => e.AccountTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.ChequeImageName).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Banks>(entity =>
            {
                entity.HasKey(e => e.BankId)
                    .HasName("PK__Banks__AA08CB13F9564EBC");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BulkOrdering>(entity =>
            {
                entity.Property(e => e.BulkPrice).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<FeaturedSuppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.Property(e => e.BusinessAddress).HasMaxLength(250);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(15);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(20);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RegistrationNumber).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderCancellationReason>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReasonName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<OrderCancellationRequest>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.0))");

                entity.Property(e => e.DiscountedAmount).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.PromotionAmount).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ShippingAmount).HasColumnType("money");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.TotalShippingCost).HasColumnType("money");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(70);
            });

            modelBuilder.Entity<ProductCatalog>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.AuthorizedProduct).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Slug).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<ProductCatalogAttribute>(entity =>
            {
                entity.Property(e => e.AttributeValue).HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SeoDescription).HasMaxLength(1000);

                entity.Property(e => e.SeoTitle).HasMaxLength(500);

                entity.Property(e => e.Slug).HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCategoryAttribute>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductCategoryGroup>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.SeoDescription).HasMaxLength(1000);

                entity.Property(e => e.SeoTitle).HasMaxLength(500);

                entity.Property(e => e.Slug).HasMaxLength(100);
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.0))");

                entity.Property(e => e.DiscountedPrice).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(20, 2)");
            });

            modelBuilder.Entity<ProductSubCategory>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SeoDescription).HasMaxLength(1000);

                entity.Property(e => e.SeoTitle).HasMaxLength(500);

                entity.Property(e => e.Slug).HasMaxLength(100);

                entity.Property(e => e.SubCategoryCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.Property(e => e.ColorName).HasMaxLength(30);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HexCode).HasMaxLength(10);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReturnAddress>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IsWarehouseAddress).HasDefaultValueSql("((0))");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<ShippingCost>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.BusinessAddress).HasMaxLength(250);

                entity.Property(e => e.BusinessDescription).HasMaxLength(500);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(30);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(40);

                entity.Property(e => e.FirstName).HasMaxLength(18);

                entity.Property(e => e.GpsCoordinates).HasMaxLength(100);

                entity.Property(e => e.HoilidayEnd).HasColumnType("datetime");

                entity.Property(e => e.HolidayMode).HasDefaultValueSql("((0))");

                entity.Property(e => e.HolidayStart).HasColumnType("datetime");

                entity.Property(e => e.IdbackImage).HasColumnName("IDBackImage");

                entity.Property(e => e.IdbackImageName)
                    .HasColumnName("IDBackImageName")
                    .HasMaxLength(100);

                entity.Property(e => e.IdfrontImage).HasColumnName("IDFrontImage");

                entity.Property(e => e.IdfrontImageName)
                    .HasColumnName("IDFrontImageName")
                    .HasMaxLength(100);

                entity.Property(e => e.InChargePerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InchargePersonEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InchargePersonMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActiv).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(18);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ntnnumber)
                    .HasColumnName("NTNNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryTrade).HasMaxLength(300);

                entity.Property(e => e.PublicId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CCityCodeId')");

                entity.Property(e => e.RegistrationNumber).HasMaxLength(50);

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<SupplierAds>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.ActiveFrom).HasColumnType("datetime");

                entity.Property(e => e.ActiveTo).HasColumnType("datetime");

                entity.Property(e => e.AdDescription).HasMaxLength(600);

                entity.Property(e => e.AdReference)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('AD45VM')");

                entity.Property(e => e.AdTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SupplierOrders>(entity =>
            {
                entity.Property(e => e.TrackingId).HasMaxLength(30);
            });

            modelBuilder.Entity<SupplierProductTags>(entity =>
            {
                entity.HasKey(e => e.TagId);

                entity.Property(e => e.TagName).HasMaxLength(80);
            });

            modelBuilder.Entity<SupplierRole>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierSlider>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageName).HasMaxLength(150);

                entity.Property(e => e.ImagePath).HasMaxLength(150);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SupplierSubCategory>(entity =>
            {
                entity.HasKey(e => e.ProductSetId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<WherehouseAddress>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
     
        }
    }
}
