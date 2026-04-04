namespace EventManager.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidation.Event;
    using static Common.EntityColomns;
    using System.Collections.Generic;

    public class InputEvent : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(ParticipantsMin, ParticipantsMax)]
        public int MaxParticipants { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End date cannot be earlier than start date.",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
