namespace EventManager.Data.Configurations
{
    using EventManager.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly Category[] categories =
        {
            new Category { Id = 1, Name = "Technology" },
            new Category { Id = 2, Name = "Business" },
            new Category { Id = 3, Name = "Education" },
            new Category { Id = 4, Name = "Health" },
            new Category { Id = 5, Name = "Science" },
            new Category { Id = 6, Name = "Art" },
            new Category { Id = 7, Name = "Marketing" },
            new Category { Id = 8, Name = "Finance" }
        };

        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(categories);
        }
    }
}
