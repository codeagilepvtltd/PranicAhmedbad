using Microsoft.AspNetCore.Mvc;

namespace PranicAhmedbad.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
