using System.Text;
using CompanyFinder.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CompanyFinder.WebUI.Extensions
{
    public static class UrlHelperExtension
    {
        //public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        //{
        //    return urlHelper.Action(
        //        action: nameof(AccountCompanyController.ConfirmEmail),
        //        controller: "AccountCompany",
        //        values: new { userId, code },
        //        protocol: scheme);
        //}
        public static string CompanyResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return urlHelper.Action(
                 action: nameof(AccountCompanyController.ResetPassword),
                 controller: "AccountCompany",
                 values: new { userId, code },
                 protocol: scheme);
        }
    }
}
