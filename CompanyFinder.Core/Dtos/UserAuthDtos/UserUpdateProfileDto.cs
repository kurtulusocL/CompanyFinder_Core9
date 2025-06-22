using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.UserAuthDtos
{
    public class UserUpdateProfileDto
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public bool IsLoginConfirmCode { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedDate { get; set; }
    }
}
