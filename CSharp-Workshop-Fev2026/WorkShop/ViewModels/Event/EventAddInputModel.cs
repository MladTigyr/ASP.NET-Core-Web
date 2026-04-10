namespace WorkShop.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;

    using WorkShop.Models;

    using static Common.ModelsValidations;

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

        [Required]
        public IEnumerable<Type> Types { get; set; } 
            = new HashSet<Type>();

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
