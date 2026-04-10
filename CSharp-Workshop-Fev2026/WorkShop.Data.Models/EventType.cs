namespace WorkShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.ModelsValidations;
    public class EventType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTypeNameLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } 
            = new HashSet<Event>();
    }
}
