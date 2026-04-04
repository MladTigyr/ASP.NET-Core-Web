namespace EventManager.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        private readonly IEnumerable<Event> events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Title = "ASP.NET Core Fundamentals Conference",
                Description = "A conference covering the fundamentals of ASP.NET Core MVC.",
                StartDate = new DateTime(2026, 3, 15),
                EndDate = new DateTime(2026, 3, 16),
                MaxParticipants = 300,
                CategoryId = 1
            },
            new Event
            {
                Id = 2,
                Title = "Modern Web Development Conference",
                Description = "Topics: MVC, REST, validation, and security basics for web apps.",
                StartDate = new DateTime(2026, 5, 20),
                EndDate = new DateTime(2026, 5, 21),
                MaxParticipants = 400,
                CategoryId = 1
            },
            new Event
            {
                Id = 3,
                Title = "Model Binding and Validation Workshop",
                Description = "Hands-on workshop focused on model binding and validation.",
                StartDate = new DateTime(2026, 4, 10),
                EndDate = new DateTime(2026, 4, 10),
                MaxParticipants = 40,
                CategoryId = 2
            },
            new Event
            {
                Id = 4,
                Title = "Razor Forms Workshop",
                Description = "Build forms with tag helpers and display validation messages properly.",
                StartDate = new DateTime(2026, 4, 24),
                EndDate = new DateTime(2026, 4, 24),
                MaxParticipants = 35,
                CategoryId = 2
            },
            new Event
            {
                Id = 5,
                Title = "Clean Controllers Seminar",
                Description = "How to keep controller actions small and predictable with ModelState.",
                StartDate = new DateTime(2026, 6, 5),
                EndDate = new DateTime(2026, 6, 5),
                MaxParticipants = 120,
                CategoryId = 3
            },
            new Event
            {
                Id = 6,
                Title = "Validation Best Practices Seminar",
                Description = "Server-side validation patterns and common mistakes in MVC apps.",
                StartDate = new DateTime(2026, 6, 12),
                EndDate = new DateTime(2026, 6, 12),
                MaxParticipants = 120,
                CategoryId = 3
            },
            new Event
            {
                Id = 7,
                Title = "EF Core Essentials Training",
                Description = "DbContext, migrations, relationships, and seeding essentials.",
                StartDate = new DateTime(2026, 2, 17),
                EndDate = new DateTime(2026, 2, 18),
                MaxParticipants = 80,
                CategoryId = 4
            },
            new Event
            {
                Id = 8,
                Title = "Testing MVC Forms Training",
                Description = "Practice form submissions, invalid model states, and error rendering.",
                StartDate = new DateTime(2026, 7, 7),
                EndDate = new DateTime(2026, 7, 8),
                MaxParticipants = 70,
                CategoryId = 4
            },
            new Event
            {
                Id = 9,
                Title = "Sofia .NET Meetup",
                Description = "Community meetup: mini talks and networking for .NET developers.",
                StartDate = new DateTime(2026, 3, 28),
                EndDate = new DateTime(2026, 3, 28),
                MaxParticipants = 150,
                CategoryId = 5
            },
            new Event
            {
                Id = 10,
                Title = "Student Projects Meetup",
                Description = "Students present their projects and discuss common issues and fixes.",
                StartDate = new DateTime(2026, 5, 9),
                EndDate = new DateTime(2026, 5, 9),
                MaxParticipants = 150,
                CategoryId = 5
            }
        };
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasData(events);
        }
    }
}
