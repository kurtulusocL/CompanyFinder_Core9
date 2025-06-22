using CompanyFinder.Core.Dtos.UserAuthDtos;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAuthCompanyService
    {
        Task<bool> LoginAsync(UserLoginDto login);
        Task<bool> LoginWithConfirmCodeAsync(UserLoginDto login);
        Task<bool> RegisterAsync(UserRegisterDto model);
        Task<bool> ConfirmMailAsync(UserConfirmCodeDto model, string value);
        Task<bool> LoginConfirmMailAsync(UserLoginConfirmCodeDto model, string value);
        Task<UserChangePasswordDto> GetChangePasswordAsync();
        Task<bool> ChangePasswordAsync(UserChangePasswordDto model);
        Task<bool> ResetPassword(UserResetPasswordDto model, string code);
        Task<UserUpdateProfileDto> GetUpdateProfileAsync(UserUpdateProfileDto model);
        Task<bool> UpdateProfileAsync(UserUpdateProfileDto model);
        Task<bool> LogoutAsync();
    }
}
