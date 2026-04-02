namespace GarageApp.Data.Configurations
{
    using GarageApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Reflection.Emit;
    using static GarageApp.Common.EntityValidation;

    public class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> entity)
        {
            entity.ToTable("Garages");

            entity.HasKey(g => g.Id);

            entity.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(MaxGarageNameLength);

            entity.Property(g => g.Location)
                .IsRequired()
                .HasMaxLength(MaxGarageLocationLength);

            entity.HasData(
                new Garage
                {
                    Id = 1,
                    Name = "Downtown Garage",
                    Location = "Sofia"
                },
                new Garage
                {
                    Id = 2,
                    Name = "Uptown Garage",
                    Location = "Plovdiv"
                });
        }
    }
}
