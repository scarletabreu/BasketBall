using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class CreatePlayer
    {
        private readonly Nba? _nbaController;

        // Event to notify when a new player is added
        public Action<Jugador>? PlayerAdded { get; set; }

        // Default constructor for WPF/XAML
        public CreatePlayer()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadData();
        }

        // Load team and city data asynchronously
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
                var cities = await _nbaController.GetAllEntitiesAsync<Ciudad>();

                foreach (var team in teams)
                {
                    TeamComboBox.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
                }

                foreach (var city in cities)
                {
                    CityComboBox.Items.Add(new ComboBoxItem { Content = city.GetNombre(), Tag = city.GetCodCiudad() });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // Capture form data
            var playerName = PlayerName.Text.Trim();
            var secondName = SecondNameLabel.Text.Trim();
            var lastName = LastName.Text.Trim();
            var secondLastName = SecondLastNameLabel.Text.Trim();

            if (!int.TryParse(NumberLabel.Text, out var number))
            {
                MessageBox.Show("Por favor, introduce un número válido en el campo de Número.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var cityId = (CityComboBox.SelectedItem as ComboBoxItem)?.Tag as string;
            var teamId = (TeamComboBox.SelectedItem as ComboBoxItem)?.Tag as string;
            var birthDate = DatePickerQuery.SelectedDate;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(playerName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(cityId) ||
                string.IsNullOrWhiteSpace(teamId) ||
                birthDate == null)
            {
                MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Create player object
                var jugador = new Jugador(
                    Guid.NewGuid().ToString(), // Generate unique ID
                    playerName,
                    secondName,
                    lastName,
                    secondLastName,
                    cityId,
                    birthDate.Value,
                    number,
                    teamId
                );

                // Save player
                await _nbaController.AddEntityAsync(jugador);

                MessageBox.Show("Jugador guardado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                PlayerAdded?.Invoke(jugador);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el jugador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cancel button click handler
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
