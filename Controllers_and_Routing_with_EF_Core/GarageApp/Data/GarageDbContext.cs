namespace GarageApp.Data
{
    using GarageApp.Data.Configurations;
    using GarageApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using static GarageApp.Common.EntityValidation;

    public class GarageDbContext : DbContext
    {
        public GarageDbContext()
        {

        }

        public GarageDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public virtual DbSet<Garage> Garages { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //            .UseSqlServer(Configuration.ConnectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GarageConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
