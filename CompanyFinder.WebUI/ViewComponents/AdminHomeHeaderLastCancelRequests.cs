using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeHeaderLastCancelRequests:ViewComponent
    {
        readonly ICancelMembershipService _cancelMembershipService;
        public AdminHomeHeaderLastCancelRequests(ICancelMembershipService cancelMembershipService)
        {
            _cancelMembershipService = cancelMembershipService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cancelMembershipService.GetAllIncludingCancelMembershipsForAdminHeader());
        }
    }
}
