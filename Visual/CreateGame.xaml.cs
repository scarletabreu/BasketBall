using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class CreateGame
    {
        private  readonly Nba? _nbaController;

        // Event to notify when a new game is added
        public Action<Juego>? GameAdded { get; set; }

        // Default constructor for WPF/XAML
        public CreateGame()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadData();
        }

        // Load team data asynchronously
        private async void LoadData()
        {
            if (_nbaController == null)
            {
                MessageBox.Show("Controller not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var teams = await _nbaController.GetAllEntitiesAsync<Equipo>();
                foreach (var team in teams)
                {
                    var comboBoxItem = new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() };
                    LocalTeam.Items.Add(comboBoxItem);
                    VisitorTeam.Items.Add(comboBoxItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Save button click handler
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_nbaController == null)
            {
                MessageBox.Show("Controller not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var description = Description.Text.Trim();
            var localTeamId = (LocalTeam.SelectedItem as ComboBoxItem)?.Tag as string;
            var visitorTeamId = (VisitorTeam.SelectedItem as ComboBoxItem)?.Tag as string;
            var date = GameDate.SelectedDate ?? DateTime.Now;

            // Input validation
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Por favor, introduce una descripción válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(localTeamId))
            {
                MessageBox.Show("Por favor, selecciona un equipo local.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(visitorTeamId))
            {
                MessageBox.Show("Por favor, selecciona un equipo visitante.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (localTeamId == visitorTeamId)
            {
                MessageBox.Show("El equipo local y el equipo visitante no pueden ser el mismo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (date < DateTime.Now)
            {
                MessageBox.Show("Por favor, selecciona una fecha válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create game object
            var game = new Juego(Guid.NewGuid().ToString(), description, localTeamId, visitorTeamId, date);

            try
            {
                await _nbaController.AddEntityAsync(game);
                GameAdded?.Invoke(game);

                MessageBox.Show($"Juego creado correctamente:\n\n" +
                                $"Descripción: {description}\n" +
                                $"Local: {await GetTeamNameById(localTeamId)}\n" +
                                $"Visitante: {await GetTeamNameById(visitorTeamId)}\n" +
                                $"Fecha: {date:dd/MM/yyyy}",
                                "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el juego: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cancel button click handler
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Get the team name asynchronously
        private async Task<string> GetTeamNameById(string teamId)
        {
            if (_nbaController == null) return "Sin equipo";

            var teams = await _nbaController.GetAllEntitiesAsync<Equipo>();
            var team = teams.FirstOrDefault(t => t.GetCodEquipo() == teamId);
            return team?.GetNombre() ?? "Sin equipo";
        }
    }
}
