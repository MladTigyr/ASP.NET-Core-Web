namespace WorkShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using WorkShop.Data;
    using WorkShop.Models;
    using WorkShop.ViewModels.Event;

    using static Common.ParameterConstants;

    public class EventController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EventController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<EventAllViewModel> eventAllViewModel = dbContext.Events
                .Include(e => e.Type)
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Start)
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(FormatForDates),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToArray();

            return View(eventAllViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            EventAddInputModel eventAddInputModel = new EventAddInputModel
            {
                Types = GetTypes()
            };

            return View(eventAddInputModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(EventAddInputModel eventAddInputModel)
        {
            IEnumerable<Type> types = GetTypes();

            if (!ModelState.IsValid)
            {
                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            if (!types.Any(t => t.Id == eventAddInputModel.TypeId))
            {
                ModelState.AddModelError(nameof(eventAddInputModel.TypeId), "Invalid type selected.");

                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            try
            {
                Event eventToAdd = new Event
                {
                    Name = eventAddInputModel.Name,
                    Description = eventAddInputModel.Description,
                    CreatedOn = DateTime.UtcNow,
                    Start = eventAddInputModel.Start,
                    End = eventAddInputModel.End,
                    TypeId = eventAddInputModel.TypeId,
                    OrganiserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                dbContext.Events.Add(eventToAdd);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "An error occured while creating the event.");

                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Event? eventToEdit = dbContext.Events
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);

            if (eventToEdit == null)
            {
                return NotFound();
            }

            EventAddInputModel model = new EventAddInputModel()
            {
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                Start = eventToEdit.Start,
                End = eventToEdit.End,
                TypeId = eventToEdit.TypeId,
                Types = GetTypes()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EventAddInputModel eventAddInputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<Type> types = GetTypes();

            if (!ModelState.IsValid)
            {
                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            if (!types.Any(t => t.Id == eventAddInputModel.TypeId))
            {
                ModelState.AddModelError(nameof(eventAddInputModel.TypeId), "Invalid type selected.");

                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            Event? eventToEdit = dbContext.Events
                .SingleOrDefault(e => e.Id == id);

            if (eventToEdit == null)
            {
                return NotFound();
            }

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            if (eventToEdit.OrganiserId != currentUserId)
            {
                return Unauthorized();
            }

            eventToEdit.Name = eventAddInputModel.Name;
            eventToEdit.Description = eventAddInputModel.Description;
            eventToEdit.Start = eventAddInputModel.Start;
            eventToEdit.End = eventAddInputModel.End;
            eventToEdit.TypeId = eventAddInputModel.TypeId;

            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Join(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            Event? eventToJoin = dbContext.Events
                .Include(e => e.EventsParticipants)
                .FirstOrDefault(e => e.Id == id);

            if (eventToJoin == null)
            {
                return NotFound();
            }
            
            bool isOrganiser = eventToJoin.OrganiserId == userId;

            if (isOrganiser)
            {
                return BadRequest();
            }

            bool alreadyJoined = eventToJoin.EventsParticipants
                .Any(ep => ep.HelperId == userId);

            if (!alreadyJoined)
            {
                try
                {
                    EventParticipant eventParticipant = new EventParticipant
                    {
                        EventId = id,
                        HelperId = userId
                    };
                    eventToJoin.EventsParticipants.Add(eventParticipant);

                    dbContext.SaveChanges();
                }
                catch 
                {
                    return BadRequest();
                }
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Joined()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            IEnumerable<EventAllViewModel> eventAllViewModel = dbContext.Events
                .Include(e => e.Type)
                .Include(e => e.EventsParticipants)
                .AsNoTracking()
                .Where(e => e.EventsParticipants.Any(ep => ep.HelperId == userId))
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Start)
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(FormatForDates),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName!
                })
                .ToArray();

            return View(eventAllViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Leave(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            EventParticipant? eventParticipant = dbContext.EventsParticipants
                    .SingleOrDefault(ep => ep.EventId == id && ep.HelperId == userId);

            if (eventParticipant == null)
            {
                return BadRequest();
            }

            try
            {
                dbContext.EventsParticipants.Remove(eventParticipant);
                dbContext.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while trying to leave the event.");

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Event? eventDetails = dbContext.Events
                .AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Organiser)
                .SingleOrDefault(e => e.Id == id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            EventDetailsViewModel eventDetailsViewModel = new EventDetailsViewModel
            {
                Name = eventDetails.Name,
                Description = eventDetails.Description,
                Start = eventDetails.Start.ToString(FormatForDates),
                End = eventDetails.End.ToString(FormatForDates),
                Type = eventDetails.Type.Name,
                Organiser = eventDetails.Organiser.UserName!,
                CreatedOn = eventDetails.CreatedOn.ToString(FormatForDates)
            };

            return View(eventDetailsViewModel);
        }

        private IEnumerable<Type> GetTypes()
        {
            IEnumerable<Type> types = dbContext.Types
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToArray();

            return types;
        }

        private IEnumerable<Event> GetEvents()
        {
            IEnumerable<Event> events = dbContext.Events
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Start)
                .ToArray();

            return events;
        }
    }
}
