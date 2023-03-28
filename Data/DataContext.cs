using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using rendszerfejlesztes_beadando.Models.Entities;
using System.ComponentModel;

namespace rendszerfejlesztes_beadando.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Models.Entities.Component> Components { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.Entities.Storage> Storage { get; set; }
    }
}
