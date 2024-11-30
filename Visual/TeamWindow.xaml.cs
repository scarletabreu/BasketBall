using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class TeamWindow
    {
        private Nba? _nbaController;

        // Constructor with Dependency Injection for NBA controller and DbConnection
        public TeamWindow(Nba? nbaController) : this()
        {
            InitializeComponent();
            _nbaController = nbaController ?? throw new ArgumentNullException(nameof(nbaController));
            LoadData();
            LoadFilters();
        }

        public TeamWindow()
        {
            InitializeComponent();
        }

        private async void LoadData()
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();
            var teams =  _nbaController?.GetAllEntitiesAsync<Equipo>().Result; // Fetch teams from the database

            if (teams != null)
                foreach (var team in teams)
                {
                    var city = (await _nbaController?.GetAllEntitiesAsync<Ciudad>()!).Find(c => c.CodCiudad == team.CodCiudad);

                    var card = new Cards
                    {
                        Width = 280,
                        Margin = new Thickness(5),
                        Heading = $"{team.Nombre}",
                        Description = city?.Nombre ?? "Sin ciudad",
                        ActionButtonText = "Ver detalles"
                    };

                    card.ActionClick += (s, e) => ShowTeamDetails(team);
                    wrapPanel.Children.Add(card);
                }
        }

        private void UpdateTeamsCards(List<Equipo>? teams)
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            if (teams != null)
                foreach (var team in teams)
                {
                    var city = _nbaController?.GetAllEntitiesAsync<Ciudad>().Result
                        .FirstOrDefault(c => c.CodCiudad == team.CodCiudad);

                    var card = new Cards
                    {
                        Width = 280,
                        Margin = new Thickness(5),
                        Heading = $"{team.Nombre}",
                        Description = city?.Nombre ?? "Sin ciudad",
                        ActionButtonText = "Ver detalles"
                    };

                    card.ActionClick += (s, e) => ShowTeamDetails(team);
                    wrapPanel.Children.Add(card);
                }
        }

        private void LoadFilters()
        {

            var teams = _nbaController?.GetAllEntitiesAsync<Equipo>().Result.ToList();

            TeamFilter.Items.Add(new ComboBoxItem { Content = " ", Tag = " " });
            if (teams != null)
                foreach (var team in teams)
                {
                    TeamFilter.Items.Add(new ComboBoxItem { Content = team.Nombre, Tag = team.CodEquipo });
                }

            TeamFilter.SelectedIndex = 0;

            var cities = _nbaController?.GetAllEntitiesAsync<Ciudad>().Result.Select(city => city.Nombre).Distinct().ToList();

            CityFilter.Items.Add(new ComboBoxItem { Content = " ", Tag = " " });
            if (cities != null)
                foreach (var city in cities)
                {
                    CityFilter.Items.Add(new ComboBoxItem { Content = city, Tag = city });
                }
            CityFilter.SelectedIndex = 0;
        }

        private void ShowTeamDetails(Equipo team)
        {
            var city = _nbaController?.GetAllEntitiesAsync<Ciudad>().Result.FirstOrDefault(c => c.CodCiudad == team.CodCiudad);
            var initials = string.Concat(team.Nombre
                .Split(' ') 
                .Where(word => !string.IsNullOrWhiteSpace(word)) 
                .Select(word => word[0]));
            
            var teamDetails = new TeamDetails
            {
                Initials = initials,
                Name = team.Nombre,
                FullName = team.Nombre,
                TeamTextBlock = { Text = team.Nombre },
                City = city?.Nombre ?? "Sin ciudad",
                ListBox = { ItemsSource = _nbaController?.GetAllEntitiesAsync<Jugador>().Result.Where(player => player.CodEquipo == team.CodEquipo).Select(player => $"{player.Nombre1} {player.Apellido1}").ToList() }
            };

            var teamDetailsWindow = new Window
            {
                Title = "Detalles del equipo",
                Content = teamDetails,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            teamDetailsWindow.ShowDialog();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedTeamNombre = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();
            var selectedCityNombre = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();

            var filteredTeams = _nbaController?.GetAllEntitiesAsync<Equipo>().Result
                .Where(team =>
                    (string.IsNullOrEmpty(selectedTeamNombre) || team.Nombre.ToLower().Contains(selectedTeamNombre)) &&
                    (string.IsNullOrEmpty(selectedCityNombre) || _nbaController.GetAllEntitiesAsync<Ciudad>().Result.Any(c => c.CodCiudad == team.CodCiudad && c.Nombre.ToLower().Contains(selectedCityNombre))))
                .ToList();

            UpdateTeamsCards(filteredTeams);
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            TeamFilter.SelectedIndex = -1;
            CityFilter.SelectedIndex = -1;
            LoadData();
        }

        private void OnTeamCreated(Equipo team)
        {
            var wrapPanel = CardsContainer;
            var city = _nbaController?.GetAllEntitiesAsync<Ciudad>().Result.FirstOrDefault(c => team.CodCiudad == c.CodCiudad);

            var cityNombre = city?.Nombre ?? "Sin Ciudad";

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{team.Nombre}",
                Description = cityNombre,
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowTeamDetails(team);
            wrapPanel.Children.Add(card);
        }

        private void CreateTeamButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createTeam = new CreateTeam();
            createTeam.TeamAdded += OnTeamCreated;
            createTeam.ShowDialog();
        }
        
    }
}
