using CompanyFinder.Entities.Entities;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdCompany> AdCompanies { get; set; }
        public DbSet<AdTarget> AdTargets { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentAnswer> AppointmentAnswers { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Blocked> Blockeds { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<CancelMembership> CancelMemberships { get; set; }
        public DbSet<CancelMembershipReason> CancelMembershipReasons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentAnswer> CommentAnswers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyCategory> CompanyCategories { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanyForm> CompanyForms { get; set; }
        public DbSet<CompanyFormAnswer> CompanyFormAnswers { get; set; }
        public DbSet<CompanyMessage> CompanyMessages { get; set; }
        public DbSet<CompanyPartnership> CompanyPartnerships { get; set; }
        public DbSet<CompanySubcategory> CompanySubcategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<DataPolicy> DataPolicies { get; set; }
        public DbSet<DropInformation> DropInformations { get; set; }
        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FormCategory> FormCategories { get; set; }
        public DbSet<Frequently> Frequentlies { get; set; }
        public DbSet<Hit> Hits { get; set; }
        public DbSet<LayoutInfo> LayoutInfos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<PricingCategory> PricingCategories { get; set; }
        public DbSet<PricingContact> PricingContacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Question> QuestionAnswers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportAnswer> ReportAnswers { get; set; }
        public DbSet<ReportCategory> ReportCategories { get; set; }
        public DbSet<SavedContent> SaveContents { get; set; }
        public DbSet<SectorNews> SectorNewses { get; set; }
        public DbSet<OurServices> OurServiceses { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<UserAgreement> UserAgreements { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var navigation in entityType.GetNavigations())
                {
                    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
                }
            }

            modelBuilder.Entity<UserSession>().HasOne(us => us.User).WithMany(u => u.UserSessions).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>().HasOne(us => us.User).WithMany(u => u.Appointments).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog>().HasOne(us => us.User).WithMany(u => u.Blogs).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CancelMembership>().HasOne(us => us.User).WithMany(u => u.CancelMemberships).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>().HasOne(us => us.User).WithMany(u => u.Comments).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentAnswer>().HasOne(us => us.User).WithMany(u => u.CommentAnswers).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>().HasOne(us => us.User).WithMany(u => u.Companies).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyMessage>().HasOne(us => us.User).WithMany(u => u.CompanyMessages).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyPartnership>().HasOne(us => us.User).WithMany(u => u.CompanyPartnerships).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyFormAnswer>().HasOne(us => us.User).WithMany(u => u.CompanyFormAnswers).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hit>().HasOne(us => us.User).WithMany(u => u.Hits).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>().HasOne(us => us.User).WithMany(u => u.Likes).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasOne(us => us.User).WithMany(u => u.Products).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileImage>().HasOne(us => us.User).WithMany(u => u.ProfileImages).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>().HasOne(us => us.User).WithMany(u => u.Questions).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionAnswer>().HasOne(us => us.User).WithMany(u => u.QuestionAnswers).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>().HasOne(us => us.User).WithMany(u => u.Reports).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SavedContent>().HasOne(us => us.User).WithMany(u => u.SavedContents).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ToDo>().HasOne(us => us.User).WithMany(u => u.ToDos).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Blog_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Appointment_CompanyId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Appointment_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Appointment_IsActive_IsDeleted");
            });

            modelBuilder.Entity<AppointmentAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_AppointmentAnswer_Id").IsUnique();
                entity.HasIndex(e => e.AppointmentId).HasDatabaseName("IX_AppointmentAnswer_AppointmentId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_AppointmentAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Audit_Id").IsUnique();
                entity.HasIndex(e => e.MacAddress).HasDatabaseName("IX_Audit_MacAddress");
                entity.HasIndex(e => new { e.MacAddress, e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Audit_MacAddress_IsActive_IsDeleted");
                entity.HasIndex(e => new { e.LocalIpAddress, e.RemoteIpAddress, e.IpAddressVPN }).HasDatabaseName("IX_Audit_LocalIpAddress_RemoteIpAddress_IpAddressVPN");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Audit_IsActive_IsDeleted");
            });

            modelBuilder.Entity<BlackList>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_BlackList_Id").IsUnique();
                entity.HasIndex(e => e.MacAddress).HasDatabaseName("IX_BlackList_MacAddress");
                entity.HasIndex(e => e.IpAddress).HasDatabaseName("IX_BlackList_IpAddress");
                entity.HasIndex(e => new { e.LocalIpAddress, e.RemoteIpAddress, e.IpAddressVPN }).HasDatabaseName("IX_BlackList_LocalIpAddress_RemoteIpAddress_IpAddressVPN");
                entity.HasIndex(e => e.AuditId).HasDatabaseName("IX_BlackList_AuditId");
                entity.HasIndex(e => e.ExpirationDate).HasDatabaseName("IX_BlackList_ExpirationDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BlackList_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Blog_Id").IsUnique();
                entity.HasIndex(e => e.BlogCategoryId).HasDatabaseName("IX_Blog_BlogCategoryId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Blog_CompanyId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Blog_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Blog_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Blocked>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Blocked_Id").IsUnique();
                entity.HasIndex(e => e.MacAddress).HasDatabaseName("IX_Blocked_MacAddress");
                entity.HasIndex(e => e.IpAddress).HasDatabaseName("IX_Blocked_IpAddress");
                entity.HasIndex(e => new { e.LocalIpAddress, e.RemoteIpAddress, e.IpAddressVPN }).HasDatabaseName("IX_Blocked_LocalIpAddress_RemoteIpAddress_IpAddressVPN");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BlackList_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CancelMembership>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CancelMembership_Id").IsUnique();
                entity.HasIndex(e => e.CancelMembershipReasonId).HasDatabaseName("IX_CancelMembership_CancelMembershipReasonId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_CancelMembership_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CancelMembership_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Company_Id").IsUnique();
                entity.HasIndex(e => e.CountryId).HasDatabaseName("IX_Company_CountryId");
                entity.HasIndex(e => e.CityId).HasDatabaseName("IX_Company_CityId");
                entity.HasIndex(e => e.CompanyCategoryId).HasDatabaseName("IX_Company_CompanyCategoryId");
                entity.HasIndex(e => e.CompanySubcategoryId).HasDatabaseName("IX_Company_CompanySubcategoryId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Company_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Company_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyContact_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyContact_CompanyId");
                entity.HasIndex(e => e.IsHide).HasDatabaseName("IX_CompanyContact_IsHide");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyContact_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyMessage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyMessage_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyMessage_CompanyId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_CompanyMessage_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyMessage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyPartnership>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyPartnership_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyPartnership_CompanyId");
                entity.HasIndex(e => e.ProductCategoryId).HasDatabaseName("IX_CompanyPartnership_ProductCategoryId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_CompanyPartnership_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyPartnership_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyForm>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyForm_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyForm_CompanyId");
                entity.HasIndex(e => e.FormCategoryId).HasDatabaseName("IX_CompanyForm_FormCategoryId");               
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyForm_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyFormAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyFormAnswer_Id").IsUnique();
                entity.HasIndex(e => e.CompanyFormId).HasDatabaseName("IX_CompanyFormAnswer_CompanyFormId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_CompanyFormAnswer_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyFormAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Comment_Id").IsUnique();
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Comment_BlogId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Comment_CompanyId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Comment_ProductId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Comment_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Comment_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CommentAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CommentAnswer_Id").IsUnique();
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_CommentAnswer_CommentId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_CommentAnswer_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CommentAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Customer_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Customer_CompanyId");
                entity.HasIndex(e => e.CustomerStatusId).HasDatabaseName("IX_Customer_CustomerStatusId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Customer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Hit>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Hit_Id").IsUnique();
                entity.HasIndex(e => e.AdId).HasDatabaseName("IX_Hit_AdId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Hit_BlogId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Hit_ProductId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Hit_CompanyId");
                entity.HasIndex(e => e.CompanyPartnershipId).HasDatabaseName("IX_Hit_CompanyPartnershipId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Hit_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Hit_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Like_Id").IsUnique();
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Like_BlogId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Like_CompanyId");
                entity.HasIndex(e => e.CompanyPartnershipId).HasDatabaseName("IX_Like_CompanyPartnershipId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Like_ProductId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Like_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Like_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Picture_Id").IsUnique();
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Picture_BlogId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Picture_CompanyId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Picture_ProductId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Picture_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Product_Id").IsUnique();
                entity.HasIndex(e => e.ProductCategoryId).HasDatabaseName("IX_Product_ProductCategoryId");
                entity.HasIndex(e => e.ProductSubcategoryId).HasDatabaseName("IX_Product_ProductSubcategoryId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Product_CompanyId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Product_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Product_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ProfileImage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ProfileImage_Id").IsUnique();
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_ProfileImage_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ProfileImage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Question_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Question_CompanyId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Question_ProductId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Question_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Question_IsActive_IsDeleted");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_QuestionAnswer_Id").IsUnique();
                entity.HasIndex(e => e.QuestionId).HasDatabaseName("IX_QuestionAnswer_QuestionId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_QuestionAnswer_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_QuestionAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Report_Id").IsUnique();
                entity.HasIndex(e => e.ReportCategoryId).HasDatabaseName("IX_Report_ReportCategoryId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Report_BlogId");
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_Report_CommentId");
                entity.HasIndex(e => e.CommentAnswerId).HasDatabaseName("IX_Report_CommentAnswerId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Report_CompanyId");
                entity.HasIndex(e => e.CompanyPartnershipId).HasDatabaseName("IX_Report_CompanyPartnershipId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_Report_ProductId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_Report_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Report_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SavedContent>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SavedContent_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_SavedContent_CompanyId");
                entity.HasIndex(e => e.CompanyPartnershipId).HasDatabaseName("IX_SavedContent_CompanyPartnershipId");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_SavedContent_ProductId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_SavedContent_BlogId");
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_SavedContent_UserId");
                entity.HasIndex(e => e.SavedDate).HasDatabaseName("IX_SavedContent_SavedDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SavedContent_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SectorNews>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SectorNews_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SectorNews_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ToDo_Id").IsUnique();
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_ToDo_UserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ToDo_IsActive_IsDeleted");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_User_Id").IsUnique();
                entity.HasIndex(e => e.NormalizedEmail).HasDatabaseName("IX_User_NormalizedEmail");
                entity.HasIndex(e => e.NormalizedUserName).HasDatabaseName("IX_User_NormalizedUserName");
                entity.HasIndex(e => e.NameSurname);
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_User_IsActive_IsDeleted");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserSession_Id").IsUnique();
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_UserSession_UserId");
                entity.HasIndex(e => e.Username).HasDatabaseName("IX_UserSession_Username");
                entity.HasIndex(e => e.LoginDate).HasDatabaseName("IX_UserSession_LoginDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_UserSession_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Country_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Country_IsActive_IsDeleted");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_City_Id").IsUnique();
                entity.HasIndex(e => e.CountryId).HasDatabaseName("IX_City_CountryId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_City_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanySubcategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanySubcategory_Id").IsUnique();
                entity.HasIndex(e => e.CompanyCategoryId).HasDatabaseName("IX_CompanySubcategory_CompanyCategoryId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanySubcategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ProductCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ProductCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ProductSubcategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ProductSubcategory_Id").IsUnique();
                entity.HasIndex(e => e.ProductCategoryId).HasDatabaseName("IX_ProductSubcategory_ProductCategoryId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ProductSubcategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_BlogCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BlogCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<FormCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_FormCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_FormCategory_IsActive_IsDeleted");
            });
        }
    }
}