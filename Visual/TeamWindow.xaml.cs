using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class TeamWindow
    {
        private readonly Nba? _nbaController;

        public TeamWindow()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            _ = InitializeDataAsync(); // Fire-and-forget initialization
        }

        private async Task InitializeDataAsync()
        {
            try
            {
                await LoadData();
                await LoadFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing data: {ex.Message}");
            }
        }

        private async Task LoadData()
        {
            try
            {
                var wrapPanel = CardsContainer;
                wrapPanel.Children.Clear();
                var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

                foreach (var team in teams)
                {
                    var city = cities.FirstOrDefault(c => c.CodCiudad == team.CodCiudad);
                    var card = CreateTeamCard(team, city?.Nombre);
                    wrapPanel.Children.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}");
            }
        }

        private async Task LoadFilters()
        {
            try
            {
                var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

                // Populate Team Filter
                TeamFilter.Items.Clear();
                TeamFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
                foreach (var team in teams)
                {
                    TeamFilter.Items.Add(new ComboBoxItem { Content = team.Nombre, Tag = team.CodEquipo });
                }
                TeamFilter.SelectedIndex = 0;

                // Populate City Filter
                CityFilter.Items.Clear();
                CityFilter.Items.Add(new ComboBoxItem { Content = "Todas", Tag = "" });
                foreach (var cityName in cities.Select(c => c.Nombre).Distinct())
                {
                    CityFilter.Items.Add(new ComboBoxItem { Content = cityName, Tag = cityName });
                }
                CityFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filters: {ex.Message}");
            }
        }

        private async void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedTeamNombre = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();
                var selectedCityNombre = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();

                var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

                var filteredTeams = teams.Where(team =>
                    (string.IsNullOrEmpty(selectedTeamNombre) || team.Nombre.ToLower().Contains(selectedTeamNombre)) &&
                    (string.IsNullOrEmpty(selectedCityNombre) || cities.Any(c => c.CodCiudad == team.CodCiudad && c.Nombre.ToLower().Contains(selectedCityNombre)))
                ).ToList();

                await UpdateTeamsCards(filteredTeams);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}");
            }
        }

        private async Task UpdateTeamsCards(List<Equipo> teams)
        {
            try
            {
                var wrapPanel = CardsContainer;
                wrapPanel.Children.Clear();
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

                foreach (var team in teams)
                {
                    var city = cities.FirstOrDefault(c => c.CodCiudad == team.CodCiudad);
                    var card = CreateTeamCard(team, city?.Nombre);
                    wrapPanel.Children.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating team cards: {ex.Message}");
            }
        }

        private async void ShowTeamDetails(Equipo team)
        {
            try
            {
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;
                var players = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;

                var city = cities.FirstOrDefault(c => c.CodCiudad == team.CodCiudad);
                var initials = string.Concat(team.Nombre.Split(' ').Where(word => !string.IsNullOrWhiteSpace(word)).Select(word => word[0]));

                var teamDetails = new TeamDetails
                {
                    Initials = initials,
                    Name = team.Nombre,
                    FullName = team.Nombre,
                    TeamTextBlock = { Text = team.Nombre },
                    City = city?.Nombre ?? "Sin ciudad",
                    ListBox = { ItemsSource = players.Where(player => player.CodEquipo == team.CodEquipo).Select(player => $"{player.Nombre1} {player.Apellido1}").ToList() }
                };

                var teamDetailsWindow = new Window
                {
                    Title = "Detalles del equipo",
                    Content = teamDetails,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                teamDetailsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing team details: {ex.Message}");
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            TeamFilter.SelectedIndex = -1;
            CityFilter.SelectedIndex = -1;
            _ = LoadData();
        }

        private void CreateTeamButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createTeam = new CreateTeam();
            createTeam.TeamAdded += async team => await OnTeamCreated(team);
            createTeam.ShowDialog();
        }

        private async Task OnTeamCreated(Equipo team)
        {
            try
            {
                var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;
                var city = cities.FirstOrDefault(c => c.CodCiudad == team.CodCiudad);

                var card = CreateTeamCard(team, city?.Nombre);
                CardsContainer.Children.Add(card);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding new team: {ex.Message}");
            }
        }

        private Cards CreateTeamCard(Equipo team, string? cityName)
        {
            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = team.Nombre,
                Description = cityName ?? "Sin ciudad",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowTeamDetails(team);
            return card;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e) => Close();
    }
}
