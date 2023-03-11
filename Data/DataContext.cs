using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.Models;

namespace rendszerfejlesztes_beadando.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //DbSet kell majd ide...
        public DbSet<Projekt> Projekt { get; set; }
        //public DbSet<Megrendelo> Megrendelo { get; set; }
        //public DbSet<Alkatresz> Alkatresz { get; set; }

        //public DbSet<Raktar> Raktar { get; set; }
        //public DbSet<Statusz> Statusz { get; set; }
        //public DbSet<Naplo> Naplo { get; set; }
    }
}
