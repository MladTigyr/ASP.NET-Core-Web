namespace WorkShop.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ModelsValidations;
    public class Type
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
