using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ToDoHitRead:ViewComponent
    {
        readonly IToDoService _toDoService;
        public ToDoHitRead(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_toDoService.HitRead(id));
        }
    }
}
