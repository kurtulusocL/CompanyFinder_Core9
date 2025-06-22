using CompanyFinder.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CompanyFinder.Entities.Entities.AppUser
{
    public class User : IdentityUser, IEntity
    {
        public string NameSurname { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? Gender { get; set; }
        public string? Title { get; set; }
        public int? ConfirmCode { get; set; }
        public bool IsAcceptedPolicies { get; set; }
        public bool IsLoginConfirmCode { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? SuspendedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CancelMembership> CancelMemberships { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentAnswer> CommentAnswers { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanyMessage> CompanyMessages { get; set; }
        public virtual ICollection<CompanyPartnership> CompanyPartnerships { get; set; }
        public virtual ICollection<CompanyFormAnswer> CompanyFormAnswers { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProfileImage> ProfileImages { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }

        public User()
        {
            EmailConfirmed = true;
            PhoneNumberConfirmed = true;
        }
    }
}
