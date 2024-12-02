using System.Collections.ObjectModel;
using System.Windows;
using Basket.Classes;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Visual
{
    public partial class MainDashboard
    {
        public ObservableCollection<Juego> Juegos { get; set; }
        public MainDashboard()
        {
            Juegos = new ObservableCollection<Juego>();
            InitializeComponent();
            DataContext = this; 
            LoadData();
        }
        
        public async void LoadData()
        {
            try
            {
                var games = await App.NbaInstance?.GetAllEntitiesAsync<Juego>()!;
                foreach (var game in games)
                {
                    Juegos.Add(game);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar juegos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        
        public void LoadGames()
        {
            
        }

        // Open the PlayerWindow
        private void PlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var playerWindow = App.ServiceProvider?.GetRequiredService<PlayerWindow>();
            playerWindow?.Show();

        }

        // Open the TeamWindow
        private void TeamButton_OnClick(object sender, RoutedEventArgs e)
        {
            var teamWindow = App.ServiceProvider?.GetRequiredService<TeamWindow>();
            teamWindow?.Show();
        }

        // Open the GameWindow
        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            var gameWindow = App.ServiceProvider?.GetRequiredService<GameWindow>();
            gameWindow?.Show();
        }

        // Open the CityWindow
        private void CityButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cityWindow = App.ServiceProvider?.GetRequiredService<CityWindow>();
            cityWindow?.Show();
        }

        // Close the MainDashboard window
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}