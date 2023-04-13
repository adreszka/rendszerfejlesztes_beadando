using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using rendszerfejlesztes_beadando.Models.Entities;
using System.ComponentModel;

namespace rendszerfejlesztes_beadando.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSetekkel létrehozzuk a bizonyos adatbázis táblákat és ezeket a Models/Entities
        // mappából lévő classok biztosítják, hogy milyen oszlopokat kell létrehoznia
        public DbSet<Models.Entities.Component> Components { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.Entities.Storage> Storage { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.Entities.Component>().HasIndex(x => x.Name).IsUnique();

            builder.Entity<Models.Entities.Storage>().HasIndex(x => new 
            { 
                x.Row, x.Columnn, x.Level 
            }).IsUnique();

            builder.Entity<Customer>().HasIndex(x => x.PhoneNumber).IsUnique();
            builder.Entity<Customer>().HasIndex(x => x.Email).IsUnique();

            builder.Entity<Status>().HasIndex(x => x.Name).IsUnique();


            base.OnModelCreating(builder);
        }
    }
}
