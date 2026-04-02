
namespace GarageApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GarageApp.Data;
    using GarageApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class GarageController : Controller
    {
        private readonly GarageDbContext dbContext;

        public GarageController(GarageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ICollection<Garage> garages = dbContext.Garages
                .Include(g => g.Cars)
                .OrderBy(g => g.Name)
                .ToList();

            return View(garages);
        }

        public IActionResult Details(int id)
        {
            Garage? garage = dbContext.Garages
                .Include(g => g.Cars)
                .FirstOrDefault(g => g.Id == id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }
    }
}
