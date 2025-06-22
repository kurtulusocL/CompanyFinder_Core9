using CompanyFinder.Business.Constants.Validator.FluentValidation;
using CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation;
using CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyFinder.Business.Attributes
{
    public static class FluentValidationExtensions
    {
        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AboutValidation>();
            services.AddValidatorsFromAssemblyContaining<AdValidation>();
            services.AddValidatorsFromAssemblyContaining<AdCompanyValidation>();
            services.AddValidatorsFromAssemblyContaining<AnnouncementValidation>();
            services.AddValidatorsFromAssemblyContaining<AppointmentValidation>();
            services.AddValidatorsFromAssemblyContaining<AppointmentAnswerValidation>();
            services.AddValidatorsFromAssemblyContaining<BlogValidation>();
            services.AddValidatorsFromAssemblyContaining<BlogCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<CancelMembershipValidation>();
            services.AddValidatorsFromAssemblyContaining<CancelMembershipReasonValidation>();
            services.AddValidatorsFromAssemblyContaining<CityValidation>();
            services.AddValidatorsFromAssemblyContaining<CommentValidation>();
            services.AddValidatorsFromAssemblyContaining<CommentAnswerValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyFormValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyFormAnswerValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyMessageValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanyPartnershipValidation>();
            services.AddValidatorsFromAssemblyContaining<CompanySubcategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<ContactValidation>();
            services.AddValidatorsFromAssemblyContaining<CountryValidation>();
            services.AddValidatorsFromAssemblyContaining<CustomerValidation>();
            services.AddValidatorsFromAssemblyContaining<CustomerStatusValidation>();
            services.AddValidatorsFromAssemblyContaining<DataPolicyValidation>();
            services.AddValidatorsFromAssemblyContaining<DropInformationValidation>();
            services.AddValidatorsFromAssemblyContaining<FeedbackValidation>();
            services.AddValidatorsFromAssemblyContaining<FormCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<FrequentlyValidation>();
            services.AddValidatorsFromAssemblyContaining<HitValidation>();
            services.AddValidatorsFromAssemblyContaining<LayoutInfoValidation>();
            services.AddValidatorsFromAssemblyContaining<LikeValidation>();
            services.AddValidatorsFromAssemblyContaining<LogoValidation>();
            services.AddValidatorsFromAssemblyContaining<NewsValidation>();
            services.AddValidatorsFromAssemblyContaining<OurServicesValidation>();
            services.AddValidatorsFromAssemblyContaining<PictureValidation>();
            services.AddValidatorsFromAssemblyContaining<PricingValidation>();
            services.AddValidatorsFromAssemblyContaining<PricingCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<PricingContactValidation>();
            services.AddValidatorsFromAssemblyContaining<ProductValidation>();
            services.AddValidatorsFromAssemblyContaining<ProductCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<ProductSubcategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<ProfileImageValidation>();
            services.AddValidatorsFromAssemblyContaining<QuestionValidation>();
            services.AddValidatorsFromAssemblyContaining<QuestionAnswerValidation>();
            services.AddValidatorsFromAssemblyContaining<ReportValidation>();
            services.AddValidatorsFromAssemblyContaining<ReportAnswerValidation>();
            services.AddValidatorsFromAssemblyContaining<ReportCategoryValidation>();
            services.AddValidatorsFromAssemblyContaining<SavedContentValidation>();
            services.AddValidatorsFromAssemblyContaining<SectorNewsValidation>();
            services.AddValidatorsFromAssemblyContaining<SliderValidation>();
            services.AddValidatorsFromAssemblyContaining<SocialMediaValidation>();
            services.AddValidatorsFromAssemblyContaining<StockValidation>();
            services.AddValidatorsFromAssemblyContaining<ToDoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserAgreementValidation>();
            services.AddValidatorsFromAssemblyContaining<UserValidation>();
            services.AddValidatorsFromAssemblyContaining<UserRoleValidation>();

            services.AddValidatorsFromAssemblyContaining<AdminLoginDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<AdminLoginConfirmCodeDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<AdminRegisterDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<AdminChangePasswordDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<AdminUpdateProfileDtoValidation>();

            services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserConfirmCodeDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserLoginConfirmCodeDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserChangePasswordDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserUpdateProfileDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserForgotPasswordDtoValidation>();
            services.AddValidatorsFromAssemblyContaining<UserResetPasswordDtoValidation>();

            services.AddFluentValidationAutoValidation();
        }
    }
}