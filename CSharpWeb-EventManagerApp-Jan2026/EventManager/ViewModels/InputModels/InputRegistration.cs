namespace EventManager.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation.Registration;
    public class InputRegistration
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }


        [Required]
        [MinLength(ParticipantNameMinLength)]
        [MaxLength(ParticipantNameMaxLength)]
        public string ParticipantName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string ParticipantEmail { get; set; } = null!;
    }
}
