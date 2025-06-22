using System.Net;
using System.Net.Mail;
using System.Text;
using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Core.Dtos.UserAuthDtos;
using CompanyFinder.Entities.Entities.AppUser;
using CompanyFinder.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CompanyFinder.WebUI.Controllers
{
    [ExceptionHandler]
    [AuditLog]
    public class AccountCompanyController : Controller
    {
        readonly IAuthCompanyService _authCompanyService;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public AccountCompanyController(IAuthCompanyService authCompanyService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _authCompanyService = authCompanyService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginDto()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "user was null");

            if (user.IsLoginConfirmCode == true)
            {
                var result = await _authCompanyService.LoginWithConfirmCodeAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(LoginConfirmMail));
                }
                TempData["companyLoginError"] = "Giriş işleminiz başarısız oldu. Lütfen tekrar deneyiniz. Hata alamaya devam ederseniz lütfen email adreslerimizden bizimle iletişime geçmekten çekinmeyin. Yaşadığınız rahatsızlık için özür dileriz.";
                return RedirectToAction(nameof(LoginConfirmMail));
            }
            else
            {
                var result = await _authCompanyService.LoginAsync(model);
                if (result)
                {
                    return RedirectToAction("Index", "HomeCompany", new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
                }
                TempData["companyLoginError"] = "Giriş işleminiz başarısız oldu. Lütfen tekrar deneyiniz. Hata alamaya devam ederseniz lütfen email adreslerimizden bizimle iletişime geçmekten çekinmeyin. Yaşadığınız rahatsızlık için özür dileriz.";
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Register()
        {
            ViewData["email"] = _httpContextAccessor.HttpContext.Session.GetString("companyEmail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var result = await _authCompanyService.RegisterAsync(model);
            if (result)
            {
                return RedirectToAction(nameof(ConfirmMail));
            }
            TempData["companyRegisterError"] = "Kayıt olurken bir hata meydana geldi. Lütfen zorunlu alanlar (tüm alanlar) doldurup tekrar deneyiniz. Kayıt sırasında yine hata alırsanız lütfen email adreslerimizden bizimle iletişime geçmekten çekinmeyin. Yaşadığınız rahatsızlık için özür dileriz.";
            return RedirectToAction(nameof(Register));
        }

        public IActionResult LoginConfirmMail()
        {
            ViewData["loginEmail"] = _httpContextAccessor.HttpContext.Session.GetString("companyLoginEmail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginConfirmMail(UserLoginConfirmCodeDto model, string value)
        {
            var result = await _authCompanyService.LoginConfirmMailAsync(model, value);
            if (result)
            {
                return RedirectToAction("Index", "HomeCompany", new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
            }
            ModelState.AddModelError("", "Mail onay için girdiğiniz 6 haneli onay kodu bir hata var. Lütfen, onay kodu kontrol ederek tekrar deneyiniz.");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ConfirmMail()
        {
            ViewData["email"] = _httpContextAccessor.HttpContext.Session.GetString("companyEmail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmMail(UserConfirmCodeDto model, string value)
        {
            var result = await _authCompanyService.ConfirmMailAsync(model, value);
            if (result)
            {
                return RedirectToAction(nameof(Login));
            }
            TempData["confirmMailError"] = "Mail onayı için girdiğiniz 6 haneli onay kodunda bir hata var. Lütfen, onay kodunu kontrol ederek tekrar deneyiniz.";
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToAction(nameof(ForgotPassword));
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (string.IsNullOrEmpty(code))
                {
                    throw new Exception("Failed to generate reset password token.");
                }
                var callbackUrl = Url.CompanyResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                if (string.IsNullOrEmpty(callbackUrl))
                {
                    throw new Exception("Failed to generate callback URL.");
                }

                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("email address", "bi2bi.com");
                mail.Priority = MailPriority.High;
                mail.Subject = "Şifremi Unuttum...";
                mail.To.Add(new MailAddress(model.Email, ""));
                mail.Body = "Şifre Sıfırlama İsteği" + " " + "Buradaki linke tıklayarak şifrenizi sıfırlayabilirsiniz. Lütfen linke tıklayın: <a href=\"" + callbackUrl + "\">link</a>";
                mail.IsBodyHtml = true;

                NetworkCredential enter = new NetworkCredential("email address", "password key");
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = enter;
                client.Send(mail);
                TempData["successResetPasswordCodeSent"] = "Lütfen mail adresinizi kontrol edin. Şifre sıfırlama kodu mail adresinize gönderildi. Eğer kodu bulamazsanız spam/gereksiz klasörünü kontrol edin.";
                return RedirectToAction(nameof(ForgotPassword));
            }
            TempData["errorResetPasswordCodeSent"] = "Lütfen geçerli mail adresinizi yazınız.";
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code), "code was null");
            }
            string decodedCode;
            try
            {
                var codeBytes = WebEncoders.Base64UrlDecode(code);
                decodedCode = Encoding.UTF8.GetString(codeBytes);
            }
            catch
            {
                throw new ArgumentException("Invalid code format");
            }
            var model = new UserResetPasswordDto { Code = decodedCode };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDto model, string code)
        {
            var result = await _authCompanyService.ResetPassword(model, code);
            if (result)
            {
                return RedirectToAction(nameof(Login));
            }
            TempData["companyResetPasswordError"] = "Şifreniz sıfırlanırken bir hata meydana geldi. Gerekli bigileri yazdığınızdan emin olun ve tekrar deneyin. Hatanın devam etmesi durumunda geri bildirim sayfasından ya da iletişim sayfamızdaki mail adresimizden durumu bize bildiriniz. Yaşadığınız sorun için üzgünüz.";
            return RedirectToAction(nameof(ResetPassword));
        }

        [Authorize(Roles = "Companies")]
        public async Task<IActionResult> ChangePassword()
        {
            return View(await _authCompanyService.GetChangePasswordAsync());
        }

        [Authorize(Roles = "Companies")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto model)
        {
            var result = await _authCompanyService.ChangePasswordAsync(model);
            if (result)
            {
                TempData["companyChangePasswordSuccessfull"] = "Şifreniz başarı ile değiştirildi";
                return RedirectToAction(nameof(ChangePassword), new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
            }
            TempData["companyChangePasswordError"] = "Şifreniz değiştirilirken bir hata meydana geldi. Lütfen tekrar deneyin. Sorununuzun devam etmesi durumunda bu durumu bize geri bildirim ile ya da iletişim sayfamızdaki mail adresini kullanarak bildirmekten çekinmeyin. Yaşadığınız sorun için üzgünüz.";
            return RedirectToAction(nameof(ChangePassword), new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
        }

        [Authorize(Roles = "Companies")]
        public async Task<IActionResult> UpdateProfile()
        {
            UserUpdateProfileDto model = new UserUpdateProfileDto();
            var result = await _authCompanyService.GetUpdateProfileAsync(model);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [Authorize(Roles = "Companies")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserUpdateProfileDto model)
        {
            var result = await _authCompanyService.UpdateProfileAsync(model);
            if (result)
            {
                TempData["companyProfileUpdatedSuccessfull"] = "Profil Bilgileriniz Başarı ile Güncellendi.";
                return RedirectToAction(nameof(UpdateProfile), new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
            }
            TempData["companyProfileUpdateError"] = "Profile bilgileriniz güncellenirken bir hata meydana geldi. Lütfen tekrar tüm alanları doldurup tekrar deneyin. Sorununuzun devam etmesi durumunda bu durumu bize geri bildirim ile ya da iletişim sayfamızdaki mail adresini kullanarak bildirmekten çekinmeyin. Yaşadığınız sorun için üzgünüz.";
            return RedirectToAction(nameof(UpdateProfile), new { id = _httpContextAccessor.HttpContext.Session.GetString("userId") });
        }

        [Authorize(Roles = "Companies")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var result = await _authCompanyService.LogoutAsync();
            if (result)
                return RedirectToAction(nameof(Login));
            return View();
        }
    }
}
