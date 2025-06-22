using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminProfileTask:ViewComponent
    {
        readonly IToDoService _toDoService;
        public AdminProfileTask(IToDoService toDoService)
        {
           _toDoService = toDoService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_toDoService.GetAllIncludingTaksForAdminProfileByUserId(id));
        }
    }
}
