namespace WorkShop.Controllers
{
    using WorkShop.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using WorkShop.Data;
    using WorkShop.ViewModels.Event;
    using Microsoft.EntityFrameworkCore;

    using static GCommon.ParameterConstants;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}