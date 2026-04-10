namespace WorkShop.ViewModels.Event
{
    public class EventDetailsViewModel : EventAllViewModel
    {
        public string Description { get; set; } = null!;

        public string End { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
