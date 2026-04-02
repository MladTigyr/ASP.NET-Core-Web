namespace GarageApp.Models
{
    using GarageApp.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using static GarageApp.Common.EntityValidation;

    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCarMakeLength)]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(MaxCarModelLength)]
        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public CarType Type { get; set; }

        public bool IsAvailable { get; set; }

        public int GarageId { get; set; }

        public virtual Garage Garage { get; set; } = null!;
    }
}
