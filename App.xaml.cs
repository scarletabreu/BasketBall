using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using Basket.Controller;
using Basket.DataBase;
using Basket.Visual;
using Microsoft.EntityFrameworkCore;

namespace Basket
{
    public partial class App
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static Nba? NbaInstance => ServiceProvider?.GetService<Nba>();

        public App()
        {
            // Build the service provider for dependency injection
            ServiceProvider = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            // Load appsettings.json for configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve and validate the connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
            }

            // Set up dependency injection
            var services = new ServiceCollection();

            // Register DbContext and IDbContextFactory for DbConnection
            services.AddDbContext<DbConnection>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            // Register IDbContextFactory for DbConnection (so Nba can use it)
            services.AddPooledDbContextFactory<DbConnection>(options =>
                options.UseSqlServer(connectionString));

            // Register Nba as a scoped service
            services.AddScoped<Nba>();

            // Register all windows as transient for fresh instances
            services.AddTransient<MainDashboard>();
            services.AddTransient<CreateGame>();
            services.AddTransient<CreatePlayer>();
            services.AddTransient<CreateTeam>();
            services.AddTransient<PlayerWindow>();
            services.AddTransient<TeamWindow>();
            services.AddTransient<GameWindow>();
            services.AddTransient<CityWindow>();
            services.AddTransient<GameDetails>();
            services.AddTransient<TeamDetails>();
            services.AddTransient<PlayerDetails>();

            return services.BuildServiceProvider();
        }




        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Resolve MainDashboard from the ServiceProvider
                var mainWindow = ServiceProvider?.GetRequiredService<MainDashboard>();

                if (mainWindow == null)
                {
                    throw new InvalidOperationException("Failed to create the MainDashboard window.");
                }

                // Show the main window
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                MessageBox.Show($"An error occurred during startup: {ex.Message}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
