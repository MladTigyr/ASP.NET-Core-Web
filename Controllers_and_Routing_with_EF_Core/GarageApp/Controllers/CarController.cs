namespace GarageApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GarageApp.Data;
    using GarageApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class CarController : Controller
    {
        private readonly GarageDbContext dbContext;

        public CarController(GarageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ICollection<Car> cars = dbContext.Cars
                .Include(c => c.Garage)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ThenByDescending(c => c.Year)
                .ToList();

            return View(cars);
        }

        public IActionResult Details(int id)
        {
            Car? car = dbContext.Cars
                .Include(c => c.Garage)
                .FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult Search(string make) 
        {
            make = (make ?? string.Empty).Trim();

            var query = dbContext.Cars
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(make))
            {
                query = query.Where(c => c.Make.Contains(make));
            }

            var cars = query
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ToList();

            return View("Index", cars);
        }
    }
}
