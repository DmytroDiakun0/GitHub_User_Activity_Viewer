//using GitHub_User_Activity_Viewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GitHub_User_Activity_Viewer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
