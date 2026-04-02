using Microsoft.AspNetCore.Mvc;

namespace MvcIntro.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Our Products";
            return View();
        }

        [Route("/Products/{id}")]
        [Route("/Products/Details/{id}")]
        public IActionResult Details(int id)
        {
            if (id < 1)
            {
                return Ok("Invalid product id");
            }

            return Ok($"Product Details for Product ID: {id}");
        }
    }
}
