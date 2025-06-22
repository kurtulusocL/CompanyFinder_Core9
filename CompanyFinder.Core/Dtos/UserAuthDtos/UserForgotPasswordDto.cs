using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.UserAuthDtos
{
    public class UserForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
