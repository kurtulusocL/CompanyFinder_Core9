using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.AdminAuthDtos
{
    public class AdminRegisterDto
    {
        [Required]
        public string NameSurname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public string Gender { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Are Not Same")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAcceptedPolicies { get; set; }
    }
}
