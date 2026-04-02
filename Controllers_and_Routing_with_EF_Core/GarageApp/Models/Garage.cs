namespace GarageApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using static GarageApp.Common.EntityValidation;
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxGarageNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxGarageLocationLength)]
        public string Location { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; } 
            = new HashSet<Car>();
    }
}