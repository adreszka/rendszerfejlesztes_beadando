namespace rendszerfejlesztes_beadando.Data.Seeder
{
    public class DatabaseSeederService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var authSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

                await authSeeder.Initialize();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
