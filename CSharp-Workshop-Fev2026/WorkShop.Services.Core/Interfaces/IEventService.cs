using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkShop.Data.Models;
using WorkShop.ViewModels.Event;

namespace WorkShop.Services.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventAllViewModel>> GetAllEventsOrderedByNameThenByStartAscAsync();

        Task<EventAddInputModel> EventAddInputModelAsync();

        Task AddEventAsync(EventAddInputModel eventAddInputModel, string userId);

        Task<EventAddInputModel> EventEditWithGivenParams(int id);

        Task EventEditInDb(int id, EventAddInputModel eventAddInputModel);

        Task<IEnumerable<EventType>> GetTypesAsync();

        Task<Event> GetEventAsync(int id);

        Task<bool> IsUserAlreadyJoinedInEventAsync(Event eventToCheck, string userId);

        Task<IEnumerable<EventAllViewModel>> GetAllEventsJoinedByUser(string userId);

        Task EventParticipentAddToDBAsync(Event eventToJoin, string userId);

        Task<EventParticipant> GetAllEventParticipentWithProvidedParams(int eventId, string userId);

        Task RemoveEventParticipent(EventParticipant eventParticipant);

        Task<EventDetailsViewModel> GetEventWithItsDetailsAsync(int id);

    }
}
