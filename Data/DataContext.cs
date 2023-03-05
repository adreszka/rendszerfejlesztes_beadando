using Microsoft.EntityFrameworkCore;

namespace rendszerfejlesztes_beadando.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //DbSet kell majd ide...
    }
}
