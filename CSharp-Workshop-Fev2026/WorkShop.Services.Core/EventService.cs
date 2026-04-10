using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShop.Services.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Identity.Client;
    using System.Globalization;
    using WorkShop.Data;
    using WorkShop.Data.Models;
    using WorkShop.Services.Core.Interfaces;
    using WorkShop.ViewModels.Event;

    using static GCommon.ParameterConstants;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext dbContext;
        public EventService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EventAllViewModel>> GetAllEventsOrderedByNameThenByStartAscAsync()
        {
            IQueryable<Event> fetchEventsQuery = dbContext.Events
                .Include(e => e.Type)
                .AsNoTracking();

            // Materializing the query opens connection to the database and waits for result => we need asynchronous.
            IEnumerable<EventAllViewModel> events = await fetchEventsQuery
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Start)
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(FormatForDates, CultureInfo.InvariantCulture),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToArrayAsync();

            return events;
        }

        public async Task<EventAddInputModel> EventAddInputModelAsync()
        {
            EventAddInputModel model = new EventAddInputModel
            {
                Types = await GetTypesAsync()
            };
            return model;
        }

        public async Task AddEventAsync(EventAddInputModel eventAddInputModel, string userId)
        {
            Event eventToAdd = new Event()
            {
                Name = eventAddInputModel.Name,
                Description = eventAddInputModel.Description,
                Start = eventAddInputModel.Start,
                End = eventAddInputModel.End,
                TypeId = eventAddInputModel.TypeId,
                CreatedOn = DateTime.UtcNow,
                OrganiserId = userId
            };

            await dbContext.Events.AddAsync(eventToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EventAddInputModel> EventEditWithGivenParams(int id)
        {
            Event eventToEdit = await GetEventAsync(id);

            EventAddInputModel model = new EventAddInputModel
            {
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                Start = eventToEdit.Start,
                End = eventToEdit.End,
                TypeId = eventToEdit.TypeId,
                Types = await GetTypesAsync()
            };

            return model;
        }

        public async Task EventEditInDb(int id, EventAddInputModel eventAddInputModel)
        {
            Event? eventToEdit = await GetEventAsync(id);

            eventToEdit.Name = eventAddInputModel.Name;
            eventToEdit.Description = eventAddInputModel.Description;
            eventToEdit.Start = eventAddInputModel.Start;
            eventToEdit.End = eventAddInputModel.End;
            eventToEdit.TypeId = eventAddInputModel.TypeId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventType>> GetTypesAsync()
        {
            IEnumerable<EventType> types = await dbContext.Types
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToArrayAsync();

            return types;
        }

        public async Task<Event> GetEventAsync(int id)
        {
            Event? eventToReturn = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            return eventToReturn;
        }

        public async Task<bool> IsUserAlreadyJoinedInEventAsync(Event eventToCheck, string userId)
        {
            bool isJoined = await dbContext.EventsParticipants
                .AnyAsync(ep => ep.EventId == eventToCheck.Id && ep.HelperId == userId);

            return isJoined;
        }

        public async Task<IEnumerable<EventAllViewModel>> GetAllEventsJoinedByUser(string userId)
        {
            IEnumerable<EventAllViewModel> events = await dbContext.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventAllViewModel
                {
                    Id = ep.Event.Id,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start.ToString(FormatForDates, CultureInfo.InvariantCulture),
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToArrayAsync();

            return events;
        }

        public async Task EventParticipentAddToDBAsync(Event eventToJoin, string userId)
        {
            EventParticipant eventParticipant = new EventParticipant
            {
                EventId = eventToJoin.Id,
                HelperId = userId
            };

            await dbContext.EventsParticipants.AddAsync(eventParticipant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EventParticipant> GetAllEventParticipentWithProvidedParams(int eventId, string userId)
        {
            EventParticipant? eventParticipant = await dbContext.EventsParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.HelperId == userId);

            return eventParticipant;
        }

        public async Task RemoveEventParticipent(EventParticipant eventParticipant)
        {
            dbContext.EventsParticipants.Remove(eventParticipant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EventDetailsViewModel> GetEventWithItsDetailsAsync(int id)
        {
            Event? eventDetails = await dbContext.Events
                .AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Organiser)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (eventDetails == null)
            {
                return null;
            }

            EventDetailsViewModel eventDetailsViewModel = new EventDetailsViewModel
            {
                Id = eventDetails.Id,
                Name = eventDetails.Name,
                Description = eventDetails.Description,
                Start = eventDetails.Start.ToString(FormatForDates, CultureInfo.InvariantCulture),
                End = eventDetails.End.ToString(FormatForDates, CultureInfo.InvariantCulture),
                Type = eventDetails.Type.Name,
                Organiser = eventDetails.Organiser.UserName
            };

            return eventDetailsViewModel;
        }
    }
}
