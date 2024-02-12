using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PranicAhmedbad.Controllers
{
    public class AccountController : Controller
    {
        // GET: Controller
        public ActionResult Login()
        {
            return View("Admin/Login");
        }


        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateLogin(IFormCollection collection)
        {
            try
            {
                string UserId = Convert.ToString(collection["emailid"]);
                string Password = Convert.ToString(collection["Password"]);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
