namespace EventManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using EventManager.Data;
    using EventManager.Models;
    using EventManager.ViewModels.InputModels;

    public class RegistrationsController : Controller
    {
        private readonly EventManagerDbContext dbContext;

        public RegistrationsController(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create([FromQuery] int eventId)
        {
            InputRegistration registration = new InputRegistration
            {
                EventId = eventId
            };

            return View(registration);
        }

        [HttpPost]
        public IActionResult Create(InputRegistration inputRegistration)
        {
            if (!ModelState.IsValid)
            {
                return View(inputRegistration);
            }

            Registration registration = new Registration
            {
                EventId = inputRegistration.EventId,
                ParticipantName = inputRegistration.ParticipantName,
                ParticipantEmail = inputRegistration.ParticipantEmail
            };

            dbContext.Registrations.Add(registration);
            dbContext.SaveChanges();

            return RedirectToAction("Details", "Events", new { id = inputRegistration.EventId });
        }
    }
}
