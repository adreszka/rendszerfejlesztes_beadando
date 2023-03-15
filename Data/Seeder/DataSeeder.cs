namespace rendszerfejlesztes_beadando.Data.Seeder
{
    public class DataSeeder
    {
        private readonly DataContext context;

        public DataSeeder(DataContext context)
        {
            this.context = context;
        }

        public async Task Initialize()
        {
            await context.Database.MigrateAsync();
        }
    }
}
