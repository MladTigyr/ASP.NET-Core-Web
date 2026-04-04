namespace EventManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.EntityValidation.Event;
    using static Common.EntityColomns;
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Column(TypeName = dates)]
        public DateTime StartDate { get; set; }

        [Column(TypeName = dates)]
        public DateTime EndDate { get; set; }

        public int MaxParticipants { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Registration> Registrations { get; set; } = 
            new List<Registration>();
    }
}
