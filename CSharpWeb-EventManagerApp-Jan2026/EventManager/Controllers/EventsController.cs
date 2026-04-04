namespace EventManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using EventManager.Data;
    using EventManager.Models;
    using EventManager.ViewModels.InputModels;
    public class EventsController : Controller
    {
        private readonly EventManagerDbContext dbContext;

        public EventsController(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Event> events = GetEvents();

            return View(events);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Category> categories = GetCategories();

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult Create(InputEvent newEvent)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Category> categories = GetCategories();
                ViewBag.Categories = categories;

                return View(newEvent);
            }

            Event eventToAdd = new Event
            {
                Title = newEvent.Title,
                Description = newEvent.Description,
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate,
                MaxParticipants = newEvent.MaxParticipants,
                CategoryId = newEvent.CategoryId
            };

            dbContext.Events.Add(eventToAdd);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Event detailsEvent = GetEvents()
                .FirstOrDefault(e => e.Id == id);

            if (detailsEvent == null)
            {
                return NotFound();
            }

            return View(detailsEvent);
        }

        private IEnumerable<Category> GetCategories()
        {
            return dbContext.Categories
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(c => c.Name)
                .ToList();
        }

        private IEnumerable<Event> GetEvents()
        {
            return dbContext.Events
                .Include(e => e.Category)
                .Include(e => e.Registrations)
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(e => e.Title)
                .ThenBy(e => e.StartDate)
                .ToList();
        }
    }
}
