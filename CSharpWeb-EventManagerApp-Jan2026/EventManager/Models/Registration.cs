namespace EventManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation.Registration;
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; } = null!;

        [Required]
        [MaxLength(ParticipantNameMaxLength)]
        public string ParticipantName { get; set; } = null!;

        [Required]
        public string ParticipantEmail { get; set; } = null!;
    }
}
