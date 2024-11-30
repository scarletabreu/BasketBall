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
                MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create new team object and add to controller
            var team = new Equipo(Guid.NewGuid().ToString(), teamName, city);
            await _nbaController?.AddEntityAsync(team)!;

            // Trigger the TeamAdded event and close the window
            TeamAdded?.Invoke(team);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving
            this.Close();
        }
    }
}