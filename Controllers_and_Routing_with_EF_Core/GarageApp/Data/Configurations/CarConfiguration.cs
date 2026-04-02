namespace GarageApp.Data.Configurations
{
    using GarageApp.Models;
    using GarageApp.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static GarageApp.Common.EntityValidation;

    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> entity)
        {
            entity.ToTable("Cars");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Make)
                .IsRequired()
                .HasMaxLength(MaxCarMakeLength);

            entity.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(MaxCarModelLength);

            entity.HasOne(c => c.Garage)
                .WithMany(g => g.Cars)
                .HasForeignKey(c => c.GarageId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new Car
                {
                    Id = 1,
                    Make = "Audi",
                    Model = "Rs3",
                    Year = 2020,
                    Type = CarType.Sedan,
                    IsAvailable = true,
                    GarageId = 1
                },
                new Car
                {
                    Id = 2,
                    Make = "BMW",
                    Model = "M5",
                    Year = 2019,
                    Type = CarType.SUV,
                    IsAvailable = true,
                    GarageId = 1
                },
                new Car
                {
                    Id = 3,
                    Make = "Mercedes",
                    Model = "C63",
                    Year = 2018,
                    Type = CarType.Hatchback,
                    IsAvailable = false,
                    GarageId = 2
                });
        }
    }
}
