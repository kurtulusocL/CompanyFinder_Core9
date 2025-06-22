using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string? OtherText { get; set; }
        public decimal? Price { get; set; }
        public bool IsCommentable { get; set; }
        public bool IsQuestionable { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? AvailableDate { get; set; }
        public string ImageUrl { get; set; }

        public int ProductCategoryId { get; set; }
        public int? ProductSubcategoryId { get; set; }
        public int? CompanyId { get; set; }
        public string UserId { get; set; }
       
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SaveContents { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
