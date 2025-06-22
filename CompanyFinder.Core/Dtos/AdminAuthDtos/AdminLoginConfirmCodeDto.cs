
using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Dtos.AdminAuthDtos
{
    public class AdminLoginConfirmCodeDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public int LoginConfirmCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
