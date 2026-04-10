namespace WorkShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WorkShop.Data.Models;
    using WorkShop.Services.Core.Interfaces;
    using WorkShop.ViewModels.Event;


    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<EventAllViewModel>? eventAllViewModel = await eventService
                .GetAllEventsOrderedByNameThenByStartAscAsync();

            if (eventAllViewModel == null)
            {
                return NotFound();
            }

            return View(eventAllViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            EventAddInputModel eventAddInputModel = await eventService
                .EventAddInputModelAsync();

            return View(eventAddInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(EventAddInputModel eventAddInputModel)
        {
            eventAddInputModel.Types = await eventService
                .GetTypesAsync();

            if (!ModelState.IsValid)
            {
                return View(eventAddInputModel);
            }

            if (!eventAddInputModel.Types.Any(t => t.Id == eventAddInputModel.TypeId))
            {
                ModelState.AddModelError(nameof(eventAddInputModel.TypeId), "Invalid type selected.");

                return View(eventAddInputModel);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                await eventService.AddEventAsync(eventAddInputModel, userId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "An error occured while creating the event.");

                return View(eventAddInputModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Event? eventToEdit = await eventService.GetEventAsync(id);

            if (eventToEdit == null)
            {
                return NotFound();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (eventToEdit.OrganiserId != userId)
            {
                return Unauthorized();
            }

            EventAddInputModel model = await eventService
                .EventEditWithGivenParams(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EventAddInputModel eventAddInputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<EventType> types = await eventService.GetTypesAsync();

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

            Event? eventToEdit = await eventService.GetEventAsync(id);

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

            try
            {
                await eventService.EventEditInDb(id, eventAddInputModel);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured while editing the event.");
                eventAddInputModel.Types = types;
                return View(eventAddInputModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Join(int id)
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

            Event? eventToJoin = await eventService.GetEventAsync(id);

            if (eventToJoin == null)
            {
                return NotFound();
            }
            
            bool isOrganiser = eventToJoin.OrganiserId == userId;

            if (isOrganiser)
            {
                return BadRequest();
            }

            bool alreadyJoined = await eventService.IsUserAlreadyJoinedInEventAsync(eventToJoin, userId);

            if (!alreadyJoined)
            {
                try
                {
                    await eventService.EventParticipentAddToDBAsync(eventToJoin, userId);
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
        public async Task<IActionResult> Joined()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            IEnumerable<EventAllViewModel> eventAllViewModel = await eventService
                .GetAllEventsJoinedByUser(userId);

            return View(eventAllViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Leave(int id)
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

            EventParticipant? eventParticipant = await eventService
                .GetAllEventParticipentWithProvidedParams(id, userId);

            if (eventParticipant == null)
            {
                return RedirectToAction(nameof(Joined));
            }

            try
            {
                await eventService.RemoveEventParticipent(eventParticipant);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while trying to leave the event.");

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            EventDetailsViewModel eventDetailsViewModel = await eventService
                .GetEventWithItsDetailsAsync(id);

            if (eventDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(eventDetailsViewModel);
        }
    }
}
