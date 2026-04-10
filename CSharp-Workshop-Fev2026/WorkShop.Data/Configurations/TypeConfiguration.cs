namespace WorkShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WorkShop.Data.Models;
    public class TypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        private readonly EventType[] types = new EventType[]
        {
            new EventType()
            {
                Id = 1,
                Name = "Animals"
            },
            new EventType()
            {
                Id = 2,
                Name = "Fun"
            },
            new EventType()
            {
                Id = 3,
                Name = "Discussion"
            },
            new EventType()
            {
                Id = 4,
                Name = "Work"
            }
        };

        public void Configure(EntityTypeBuilder<EventType> entity)
        {
            entity.HasData(types);
        }
    }
}
