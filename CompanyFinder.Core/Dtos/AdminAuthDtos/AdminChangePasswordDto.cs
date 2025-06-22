using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.AdminAuthDtos
{
    public class AdminChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords are not same.")]
        public string ConfirmNewPassword { get; set; }
        public string StatusMessage { get; set; }
    }
}
