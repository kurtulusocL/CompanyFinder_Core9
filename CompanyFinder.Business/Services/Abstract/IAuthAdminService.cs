using CompanyFinder.Core.Dtos.AdminAuthDtos;
using CompanyFinder.Core.Dtos.UserAuthDtos;
using CompanyFinder.Core.ViewModels.RoleVM;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAuthAdminService
    {
        Task<bool> LoginAsync(AdminLoginDto login);
        Task<bool> LoginWithConfirmCodeAsync(AdminLoginDto login);
        Task<bool> LoginConfirmMailAsync(AdminLoginConfirmCodeDto model, string value);
        Task<bool> RegisterAsync(AdminRegisterDto model);
        Task<bool> RegisterCompanyAsync(UserRegisterDto model);
        Task<List<RoleAssignVM>> GetRoleAssingAsync(string id);
        Task<bool> RoleAssignAsync(List<RoleAssignVM> modelList, string id);
        Task<AdminChangePasswordDto> GetChangePasswordAsync();
        Task<bool> ChangePasswordAsync(AdminChangePasswordDto model);
        Task<AdminUpdateProfileDto> GetUpdateProfileAsync(AdminUpdateProfileDto model);
        Task<bool> UpdateProfileAsync(AdminUpdateProfileDto model);
        Task<bool> LogoutAsync();
    }
}
