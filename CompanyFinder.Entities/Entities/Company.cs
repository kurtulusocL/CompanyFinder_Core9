using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime FoundationDate { get; set; }
        public string? Slogan { get; set; }

        [Url, AllowNull]
        public string? WebsiteUrl { get; set; }
        public bool IsCommentable { get; set; }
        public string Logo { get; set; }

        public int CountryId { get; set; }
        public int? CityId { get; set; }
        public int CompanyCategoryId { get; set; }
        public int? CompanySubcategoryId { get; set; }
        public string UserId { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual CompanyCategory CompanyCategory { get; set; }
        public virtual CompanySubcategory CompanySubcategory { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanyForm> CompanyForms { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CompanyMessage> CompanyMessages { get; set; }
        public virtual ICollection<CompanyPartnership> CompanyPartnerships { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SaveContents { get; set; }
    }
}
