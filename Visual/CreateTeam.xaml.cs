using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class CreateTeam
    {
        private readonly Nba? _nbaController;

        public Action<Equipo>? TeamAdded { get; set; }

        // Constructor with dependency injection for NBA controller
        public CreateTeam()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadData();
        }

        private async void LoadData()
        {
            // Load cities into combo box
            var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

            foreach (var city in cities)
            {
                CityComboBox.Items.Add(new ComboBoxItem { Content = city.GetNombre(), Tag = city.GetCodCiudad() });
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get team name and selected city
            var teamName = TeamName.Text.Trim();
            var city = (CityComboBox.SelectedItem as ComboBoxItem)?.Tag as string;

            // Validate input fields
            if (string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var cantEquipos = (await _nbaController!.GetAllEntitiesAsync<Equipo>()).Count + 1;

            // Create game object
            try
            {
                // Create player object
                var equipo = new Equipo(
                    "E-" + cantEquipos.ToString("D3"),
                    teamName,
                    city
                );

                // Save player
                await _nbaController.AddEntityAsync(equipo);

                MessageBox.Show("Equipo guardado con éxito.", "Éxito", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                TeamAdded?.Invoke(equipo);
                Close();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error al guardar el equipo: {ex.Message}";

                // Check if there is an InnerException and add it to the error message
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving
            Close();
        }
    }
}