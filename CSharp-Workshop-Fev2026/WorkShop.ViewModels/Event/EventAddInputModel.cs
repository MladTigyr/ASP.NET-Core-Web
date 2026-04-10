namespace WorkShop.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;
    using WorkShop.Data.Models;
    using static GCommon.ModelsValidations;

    public class EventAddInputModel : IValidatableObject
    {
        [Required]
        [MinLength(MinEventNameLength)]
        [MaxLength(MaxEventNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        public IEnumerable<EventType> Types { get; set; } 
            = new HashSet<EventType>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (End <= Start)
            {
                yield return new ValidationResult(
                    "End date must be later than start date.",
                    new[] { nameof(End) });
            }
        }
    }
}
