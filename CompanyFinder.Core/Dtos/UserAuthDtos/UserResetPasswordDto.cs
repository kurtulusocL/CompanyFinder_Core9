﻿
namespace CompanyFinder.Core.Dtos.UserAuthDtos
{
    public class UserResetPasswordDto
    {
        public string Email { get; set; }        
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Code { get; set; }
    }
}
