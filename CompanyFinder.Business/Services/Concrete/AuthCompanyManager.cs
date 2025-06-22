using System.Security.Claims;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Core.Dtos.UserAuthDtos;
using CompanyFinder.Core.Helpers;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using CompanyFinder.Entities.Entities.AppUser;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AuthCompanyManager : IAuthCompanyService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public AuthCompanyManager(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<bool> LoginAsync(UserLoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                if (user.IsActive == true && user.IsDeleted == false)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (result.Succeeded)
                    {
                        var userSession = new UserSession
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            LoginDate = DateTime.Now.ToLocalTime(),
                            IsOnline = true
                        };
                        if (userSession != null)
                        {
                            _context.UserSessions.Add(userSession);
                            await _context.SaveChangesAsync();

                            if (string.IsNullOrEmpty(login.ReturnUrl))
                                return true;
                            return false;
                        }
                        return false;
                    }
                    throw new Exception("Login was not successfull.");
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LoginWithConfirmCodeAsync(UserLoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                if (user.IsActive == true && user.IsDeleted == false)
                {
                    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: true);
                    if (!signInResult.Succeeded)
                    {
                        return false;
                    }
                    else
                    {
                        if (user.IsLoginConfirmCode == true)
                        {
                            MimeMessage mimeMessage = new MimeMessage();
                            MailboxAddress mailboxAddressFrom = new MailboxAddress("bi2bi.com", "email address");
                            MailboxAddress mailboxAddressTo = new MailboxAddress(user.UserName, user.Email);

                            mimeMessage.From.Add(mailboxAddressFrom);
                            mimeMessage.To.Add(mailboxAddressTo);

                            var bodyBuilder = new BodyBuilder();
                            bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz. Bu kod 5 dakika geçerlidir. Bu kodu kimse ile paylaşmayınız. Bu kod sisteme giriş yapabilmeniz için gönderildi. :  " + code;
                            mimeMessage.Body = bodyBuilder.ToMessageBody();
                            mimeMessage.Subject = "bi2bi.com Giriş Onay Kodu";

                            SmtpClient client = new SmtpClient();
                            client.Connect("smtp.gmail.com", 587, false);
                            client.Authenticate("", "password key");
                            await client.SendAsync(mimeMessage);
                            client.Disconnect(true);

                            var authToken = Guid.NewGuid().ToString();
                            var creationTime = DateTime.Now;
                            _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                            _httpContextAccessor.HttpContext.Session.SetString("companyLoginEmail", user.Email);
                            _httpContextAccessor.HttpContext.Session.SetString("confirmCode", code.ToString());
                            _httpContextAccessor.HttpContext.Session.SetString("authToken", authToken);
                            _httpContextAccessor.HttpContext.Session.SetString("tokenCreationTime", creationTime.ToString());

                            return true;
                        }
                    }                    
                    throw new Exception("Login was not successfull.");
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterAsync(UserRegisterDto model)
        {
            try
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                var audit = httpContext?.Items["CurrentAudit"] as Audit;

                var newBlacklist = new BlackList
                {
                    MacAddress = audit.MacAddress,
                    IpAddress = audit.IpAddress,
                    RemoteIpAddress = audit.RemoteIpAddress,
                    IpAddressVPN = audit.IpAddressVPN,
                    LocalIpAddress = audit.LocalIpAddress,
                    ExpirationDate = DateTime.Now.AddMonths(4),
                    AuditId = audit.Id
                };
                await _context.BlackLists.AddAsync(newBlacklist);
                await _context.SaveChangesAsync();

                var blackListEntry = await _context.BlackLists.Where(b => (b.MacAddress == audit.MacAddress || b.IpAddress == audit.IpAddress || b.IpAddressVPN == audit.IpAddressVPN || b.LocalIpAddress == audit.LocalIpAddress || b.RemoteIpAddress == audit.RemoteIpAddress) && b.Id != newBlacklist.Id).Where(b => b.ExpirationDate > DateTime.Now).FirstOrDefaultAsync();

                if (blackListEntry != null)
                {
                    return false;
                }
                else
                {
                    if (model == null)
                        throw new ArgumentNullException(nameof(model), "Model was null");

                    var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (nameSurnameParts.Length < 2)
                    {
                        throw new Exception("Ad ve soyad tam olarak girilmeli (ör: Ad Soyad).");
                    }

                    var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                    var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                    var baseUsername = $"{firstName}{lastName}";
                    var username = $"{baseUsername}_bi2bi.com";

                    int suffix = 1;
                    while (await _userManager.FindByNameAsync(username) != null)
                    {
                        username = $"{baseUsername}{suffix}_bi2bi.com";
                        suffix++;
                    }

                    Random random = new Random();
                    int code;
                    code = random.Next(100000, 1000000);

                    var user = new User
                    {
                        NameSurname = model.NameSurname,
                        UserName = username,
                        Email = model.Email,
                        Country = model.Country,
                        City = model.City,
                        PhoneNumber = model.PhoneNumber,
                        Title = "Company",
                        Birthdate = model.Birthdate,
                        IsAcceptedPolicies = model.IsAcceptedPolicies,
                        ConfirmCode = code,
                        CreatedDate = DateTime.Now.ToLocalTime()
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, "Companies");
                    _httpContextAccessor.HttpContext.Session.SetString("companyEmail", user.Email);

                    if (result.Succeeded)
                    {
                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom = new MailboxAddress("bi2bi.com", "email address");
                        MailboxAddress mailboxAddressTo = new MailboxAddress(user.UserName, user.Email);

                        mimeMessage.From.Add(mailboxAddressFrom);
                        mimeMessage.To.Add(mailboxAddressTo);

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz. Bu kodu kimse ile paylaşmayınız. Bu kod sisteme kayıt olabilmeniz için gönderildi. : " + code;
                        mimeMessage.Body = bodyBuilder.ToMessageBody();
                        mimeMessage.Subject = "bi2bi.com Üyelik Onay Kodu";

                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("email address", "password key");
                        await client.SendAsync(mimeMessage);
                        client.Disconnect(true);

                    }
                    return true;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new company user.", ex);
            }
        }
        public async Task<UserChangePasswordDto> GetChangePasswordAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(_httpContextAccessor.HttpContext.User)}'.");
                }
                var hasPassword = await _userManager.HasPasswordAsync(user);
                if (!hasPassword)
                {
                    throw new Exception("There is not a current password");
                }
                return new UserChangePasswordDto
                {
                    StatusMessage = "Success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<bool> ChangePasswordAsync(UserChangePasswordDto model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(_httpContextAccessor.HttpContext.User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    return false;
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while chancing password.", ex);
            }
        }
        public async Task<UserUpdateProfileDto> GetUpdateProfileAsync(UserUpdateProfileDto model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
                model.City = user.City;
                model.Country = user.Country;
                model.IsLoginConfirmCode = user.IsLoginConfirmCode;
                if (model != null)
                {
                    return model;
                }
                throw new Exception("Model was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<bool> UpdateProfileAsync(UserUpdateProfileDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "Model was null");

                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.City = model.City;
                user.Country = model.Country;
                user.IsLoginConfirmCode = model.IsLoginConfirmCode;
                user.UpdatedDate = DateTime.Now.ToLocalTime();

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating profile.", ex);
            }
        }
        public async Task<bool> LogoutAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var activeSession = await _context.UserSessions
                    .Where(s => s.UserId == userId && s.IsActive)
                    .OrderByDescending(s => s.LoginDate).FirstOrDefaultAsync();

                if (activeSession != null)
                {
                    try
                    {
                        activeSession.LogoutDate = DateTime.Now.ToLocalTime();
                        activeSession.IsOnline = false;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An unexpected error occurred while doing logout.", ex);
                    }

                    _httpContextAccessor.HttpContext.Session.Clear();
                    await _signInManager.SignOutAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while doing logout.", ex);
            }
        }
        public async Task<bool> ConfirmMailAsync(UserConfirmCodeDto model, string value)
        {
            try
            {
                value = _httpContextAccessor.HttpContext.Session.GetString("companyEmail");
                if (value != null)
                {
                    var user = await _userManager.FindByEmailAsync(value);
                    if (user != null)
                    {
                        if (user.ConfirmCode == model.ConfirmCode)
                        {
                            return true;
                        }
                        throw new Exception("Confirm codes are not same");
                    }
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                throw new ArgumentNullException(nameof(value), "value was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while confirming email.", ex);
            }
        }
        public async Task<bool> LoginConfirmMailAsync(UserLoginConfirmCodeDto model, string value)
        {
            try
            {
                value = _httpContextAccessor.HttpContext.Session.GetString("companyLoginEmail");
                var storedCode = _httpContextAccessor.HttpContext.Session.GetString("confirmCode");
                var tokenCreationTime = _httpContextAccessor.HttpContext.Session.GetString("tokenCreationTime");

                if (value != null)
                {
                    var user = await _userManager.FindByEmailAsync(value);
                    if (user != null)
                    {
                        if (storedCode != null && tokenCreationTime != null)
                        {
                            var creationTime = DateTime.Parse(tokenCreationTime);
                            if ((DateTime.Now - creationTime).TotalSeconds > 300)
                            {
                                throw new Exception("Token has expired. Please request a new code.");
                            }

                            if (storedCode == Convert.ToString(model.LoginConfirmCode))
                            {
                                var storedToken = _httpContextAccessor.HttpContext.Session.GetString("authToken");
                                if (string.IsNullOrEmpty(storedToken))
                                {
                                    throw new Exception("Authentication token not found in session.");
                                }
                                var userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                                if (user.Id != userId)
                                {
                                    throw new Exception("Invalid user session.");
                                }
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                                _httpContextAccessor.HttpContext.Session.SetString("companyLoginEmail", user.Email);
                                var userSession = new UserSession
                                {
                                    UserId = user.Id,
                                    Username = user.UserName,
                                    LoginDate = DateTime.Now.ToLocalTime(),
                                    IsOnline = true
                                };
                                if (userSession != null)
                                {
                                    _context.UserSessions.Add(userSession);
                                    await _context.SaveChangesAsync();

                                    if (string.IsNullOrEmpty(model.ReturnUrl))
                                        return true;
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        }
                        throw new Exception("Stored code was null");
                    }
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                throw new ArgumentNullException(nameof(value), "value was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while confirming email.", ex);
            }
        }

        public async Task<bool> ResetPassword(UserResetPasswordDto model, string code)
        {
            try
            {
                if (code == null)
                    throw new ArgumentNullException(nameof(code), "code was null");

                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");

                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    return false;
                }

                User user = new User();
                user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                else
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while reset password.", ex);
            }
        }
    }
}