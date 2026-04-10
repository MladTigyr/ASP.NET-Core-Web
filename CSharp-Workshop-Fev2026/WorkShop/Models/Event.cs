namespace WorkShop.Models
{
    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.ModelsValidations;
    using static Common.ParameterConstants;

    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxEventNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string OrganiserId { get; set; } = null!;

        [ForeignKey(nameof(OrganiserId))]
        public virtual IdentityUser Organiser { get; set; } = null!;

        [Column(TypeName = DateTypeName)]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = DateTypeName)]
        public DateTime Start { get; set; }

        [Column(TypeName = DateTypeName)]
        public DateTime End { get; set; }

        public int TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual Type Type { get; set; } = null!;

        public virtual ICollection<EventParticipant> EventsParticipants { get; set; } 
            = new HashSet<EventParticipant>();
    }
}
