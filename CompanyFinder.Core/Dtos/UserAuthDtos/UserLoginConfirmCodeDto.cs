
namespace CompanyFinder.Core.Dtos.UserAuthDtos
{
    public class UserLoginConfirmCodeDto
    {
        public string Email { get; set; }
        public int LoginConfirmCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
