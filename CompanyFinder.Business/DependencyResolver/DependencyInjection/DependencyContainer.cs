using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Business.Services.Concrete;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyFinder.Business.DependencyResolver.DependencyInjection
{
    public static class DependencyContainer
    {
        public static void DependencyService(this IServiceCollection services)
        {
            services.AddScoped<IAuthAdminService, AuthAdminManager>();
            services.AddScoped<IAuthCompanyService, AuthCompanyManager>();

            services.AddScoped<IUserSessionDal, UserSessionDal>();
            services.AddScoped<IUserSessionService, UserSessionManager>();

            services.AddScoped<IAboutDal, AboutDal>();
            services.AddScoped<IAboutService, AboutManager>();

            services.AddScoped<IAdDal, AdDal>();
            services.AddScoped<IAdService, AdManager>();

            services.AddScoped<IAdCompanyDal, AdCompanyDal>();
            services.AddScoped<IAdCompanyService, AdCompanyManager>();

            services.AddScoped<IAdTargetDal, AdTargetDal>();
            services.AddScoped<IAdTargetService, AdTargetManager>();

            services.AddScoped<IAnnouncementDal, AnnouncementDal>();
            services.AddScoped<IAnnouncementService, AnnouncementManager>();

            services.AddScoped<IAppointmentDal, AppointmentDal>();
            services.AddScoped<IAppointmentService, AppointmentManager>();

            services.AddScoped<IAppointmentAnswerDal, AppointmentAnswerDal>();
            services.AddScoped<IAppointmentAnswerService, AppointmentAnswerManager>();

            services.AddScoped<IAuditDal, AuditDal>();
            services.AddScoped<IAuditService, AuditManager>();

            services.AddScoped<IBlackListDal, BlackListDal>();
            services.AddScoped<IBlackListService, BlackListManager>();

            services.AddScoped<IBlockedDal, BlockedDal>();
            services.AddScoped<IBlockedService, BlockedManager>();

            services.AddScoped<IBlogDal, BlogDal>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<IBlogCategoryDal, BlogCategoryDal>();
            services.AddScoped<IBlogCategoryService, BlogCategoryManager>();

            services.AddScoped<ICancelMembershipDal, CancelMembershipDal>();
            services.AddScoped<ICancelMembershipService, CancelMembershipManager>();

            services.AddScoped<ICancelMembershipReasonDal, CancelMembershipReasonDal>();
            services.AddScoped<ICancelMembershipReasonService, CancelMembershipReasonManager>();

            services.AddScoped<ICityDal, CityDal>();
            services.AddScoped<ICityService, CityManager>();

            services.AddScoped<ICommentDal, CommentDal>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<ICommentAnswerDal, CommentAnswerDal>();
            services.AddScoped<ICommentAnswerService, CommentAnswerManager>();

            services.AddScoped<ICompanyDal, CompanyDal>();
            services.AddScoped<ICompanyService, CompanyManager>();

            services.AddScoped<ICompanyCategoryDal, CompanyCategoryDal>();
            services.AddScoped<ICompanyCategoryService, CompanyCategoryManager>();

            services.AddScoped<ICompanyContactDal, CompanyContactDal>();
            services.AddScoped<ICompanyContactService, CompanyContactManager>();

            services.AddScoped<ICompanyFormDal, CompanyFormDal>();
            services.AddScoped<ICompanyFormService, CompanyFormManager>();

            services.AddScoped<ICompanyFormAnswerDal, CompanyFormAnswerDal>();
            services.AddScoped<ICompanyFormAnswerService, CompanyFormAnswerManager>();

            services.AddScoped<ICompanySubcategoryDal, CompanySubcategoryDal>();
            services.AddScoped<ICompanySubcategoryService, CompanySubcategoryManager>();

            services.AddScoped<ICompanyMessageDal, CompanyMessageDal>();
            services.AddScoped<ICompanyMessageService, CompanyMessageManager>();

            services.AddScoped<ICompanyPartnershipDal, CompanyPartnershipDal>();
            services.AddScoped<ICompanyPartnershipService, CompanyPartnershipManager>();

            services.AddScoped<IContactDal, ContactDal>();
            services.AddScoped<IContactService, ContactManager>();

            services.AddScoped<ICountryDal, CountryDal>();
            services.AddScoped<ICountryService, CountryManager>();

            services.AddScoped<ICustomerDal, CustomerDal>();
            services.AddScoped<ICustomerService, CustomerManager>();

            services.AddScoped<ICustomerStatusDal, CustomerStatusDal>();
            services.AddScoped<ICustomerStatusService, CustomerStatusManager>();

            services.AddScoped<IDataPolicyDal, DataPolicyDal>();
            services.AddScoped<IDataPolicyService, DataPolicyManager>();

            services.AddScoped<IDropInformationDal, DropInformationDal>();
            services.AddScoped<IDropInformationService, DropInformationManager>();

            services.AddScoped<IExceptionLoggerDal, ExceptionLoggerDal>();
            services.AddScoped<IExceptionLoggerService, ExceptionLoggerManager>();

            services.AddScoped<IFeedbackDal, FeedbackDal>();
            services.AddScoped<IFeedbackService, FeedbackManager>();

            services.AddScoped<IFormCategoryDal, FormCategoryDal>();
            services.AddScoped<IFormCategoryService, FormCategoryManager>();

            services.AddScoped<IFrequentlyDal, FrequentlyDal>();
            services.AddScoped<IFrequentlyService, FrequentlyManager>();

            services.AddScoped<IHitDal, HitDal>();
            services.AddScoped<IHitService, HitManager>();

            services.AddScoped<ILayoutInfoDal, LayoutInfoDal>();
            services.AddScoped<ILayoutInfoService, LayoutInfoManager>();

            services.AddScoped<ILikeDal, LikeDal>();
            services.AddScoped<ILikeService, LikeManager>();

            services.AddScoped<ILogoDal, LogoDal>();
            services.AddScoped<ILogoService, LogoManager>();

            services.AddScoped<INewsDal, NewsDal>();
            services.AddScoped<INewsService, NewsManager>();

            services.AddScoped<IOurServicesDal, OurServicesDal>();
            services.AddScoped<IOurServicesService, OurServicesManager>();

            services.AddScoped<IPictureDal, PictureDal>();
            services.AddScoped<IPictureService, PictureManager>();

            services.AddScoped<IPricingDal, PricingDal>();
            services.AddScoped<IPricingService, PricingManager>();

            services.AddScoped<IPricingCategoryDal, PricingCategoryDal>();
            services.AddScoped<IPricingCategoryService, PricingCategoryManager>();

            services.AddScoped<IPricingContactDal, PricingContactDal>();
            services.AddScoped<IPricingContactService, PricingContactManager>();

            services.AddScoped<IProductDal, ProductDal>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IProductCategoryDal, ProductCategoryDal>();
            services.AddScoped<IProductCategoryService, ProductCategoryManager>();

            services.AddScoped<IProductSubcategoryDal, ProductSubcategoryDal>();
            services.AddScoped<IProductSubcategoryService, ProductSubcategoryManager>();

            services.AddScoped<IProfileImageDal, ProfileImageDal>();
            services.AddScoped<IProfileImageService, ProfileImageManager>();

            services.AddScoped<IQuestionDal, QuestionDal>();
            services.AddScoped<IQuestionService, QuestionManager>();

            services.AddScoped<IQuestionAnswerDal, QuestionAnswerDal>();
            services.AddScoped<IQuestionAnswerService, QuestionAnswerManager>();

            services.AddScoped<IReportDal, ReportDal>();
            services.AddScoped<IReportService, ReportManager>();

            services.AddScoped<IReportAnswerDal, ReportAnswerDal>();
            services.AddScoped<IReportAnswerService, ReportAnswerManager>();

            services.AddScoped<IReportCategoryDal, ReportCategoryDal>();
            services.AddScoped<IReportCategoryService, ReportCategoryManager>();

            services.AddScoped<ISavedContentDal, SavedContentDal>();
            services.AddScoped<ISavedContentService, SavedContentManager>();

            services.AddScoped<ISectorNewsDal, SectorNewsDal>();
            services.AddScoped<ISectorNewsService, SectorNewsManager>();

            services.AddScoped<ISliderDal, SliderDal>();
            services.AddScoped<ISliderService, SliderManager>();

            services.AddScoped<ISocialMediaDal, SocialMediaDal>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();

            services.AddScoped<IStockDal, StockDal>();
            services.AddScoped<IStockService, StockManager>();

            services.AddScoped<IToDoDal, ToDoDal>();
            services.AddScoped<IToDoService, ToDoManager>();

            services.AddScoped<IUserAgreementDal, UserAgreementDal>();
            services.AddScoped<IUserAgreementService, UserAgreementManager>();

            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IUserRoleDal, UserRoleDal>();
            services.AddScoped<IUserRoleService, UserRoleManager>();

            services.AddFluentValidationServices();
        }
    }
}