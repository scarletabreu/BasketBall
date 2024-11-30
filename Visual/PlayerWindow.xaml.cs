using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class PlayerWindow
    {
        private readonly Nba? _nbaController;
        
        public PlayerWindow()
        {
            InitializeComponent();
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadPlayerCards();
            LoadFilters();
        }

        // Load the player cards based on the current data from the NBA controller
        private async void LoadPlayerCards()
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var players = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;
            var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
            
            foreach (var player in players)
            {
                var teamName = teams.FirstOrDefault(team => team.GetCodEquipo() == player.GetCodEquipo())?.GetNombre() ?? "Sin equipo";

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.GetNombre1()} {player.GetApellido1()}",
                    Description = $"{player.GetNumero()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);
                wrapPanel.Children.Add(card);
            }
        }

        // Show the details of a player in a new window
        private async void ShowPlayerDetails(Jugador jugador)
        {
            var city = (await _nbaController?.GetAllEntitiesAsync<Ciudad>()!).FirstOrDefault(c => c.GetCodCiudad() == jugador.GetCiudadNacim());
            var team = (await _nbaController?.GetAllEntitiesAsync<Equipo>()!).FirstOrDefault(t => t.GetCodEquipo() == jugador.GetCodEquipo());

            var playerDetails = new PlayerDetails
            {
                Name = $"{jugador.GetNombre1()} {jugador.GetApellido1()}",
                FullName = $"{jugador.GetNombre1()} {jugador.GetNombre2()} {jugador.GetApellido1()} {jugador.GetApellido2()}",
                BirthDay = jugador.GetFechaNacim().ToString("dd/MM/yyyy"),
                Number = jugador.GetNumero().ToString(),
                City = city?.GetNombre() ?? "Sin ciudad",
                Team = team?.GetNombre() ?? "Sin equipo",
                Age = (DateTime.Now.Year - jugador.GetFechaNacim().Year).ToString(),
                Initials = $"{jugador.GetNombre1()[0]}{jugador.GetApellido1()[0]}"
            };

            var playerDetailsWindow = new Window
            {
                Title = "Detalles del jugador",
                Content = playerDetails,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            playerDetailsWindow.ShowDialog();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Load filters into the combo boxes for filtering players
        private async void LoadFilters()
        {
            var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
            var players = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;
            var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

            // Populate Team Filter
            TeamFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var team in teams)
            {
                TeamFilter.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
            }
            TeamFilter.SelectedIndex = 0;

            // Populate Number Filter
            var numbers = players.Select(player => player.GetNumero().ToString()).Distinct().ToList();
            NumberFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var number in numbers)
            {
                NumberFilter.Items.Add(new ComboBoxItem { Content = number, Tag = number });
            }
            NumberFilter.SelectedIndex = 0;

            // Populate Name Filter
            var names = players.Select(player => player.GetNombre1()).Distinct().ToList();
            NameFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var name in names)
            {
                NameFilter.Items.Add(new ComboBoxItem { Content = name, Tag = name });
            }
            NameFilter.SelectedIndex = 0;

            // Populate City Filter
            var cityNames = cities.Select(city => city.GetNombre()).Distinct().ToList();
            CityFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var city in cityNames)
            {
                CityFilter.Items.Add(new ComboBoxItem { Content = city, Tag = city });
            }
            CityFilter.SelectedIndex = 0;
        }

        // Apply filters based on user selections
        private async void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nameFilter = NameFilter.Text.ToLower();
            var selectedTeamName = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();
            var numberFilter = NumberFilter.Text;
            var selectedCityName = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString()?.ToLower();

            var filteredPlayers = (await _nbaController?.GetAllEntitiesAsync<Jugador>()!)
                .Where(player =>
                    (string.IsNullOrEmpty(nameFilter) || player.GetNombre1().ToLower().Contains(nameFilter)) &&
                    (string.IsNullOrEmpty(selectedTeamName) || GetEquipoNameByCod(player.GetCodEquipo()).ToLower().Contains(selectedTeamName)) &&
                    (string.IsNullOrEmpty(numberFilter) || player.GetNumero().ToString() == numberFilter) &&
                    (string.IsNullOrEmpty(selectedCityName) || GetCiudadNameByCod(player.GetCiudadNacim()).ToLower().Contains(selectedCityName))
                )
                .ToList();

            UpdatePlayerCards(filteredPlayers);
        }

        // Helper methods to get team and city names by their IDs
        private string GetEquipoNameByCod(string teamCod)
        {
            return _nbaController?.GetAllEntitiesAsync<Equipo>().Result.FirstOrDefault(t => t.GetCodEquipo() == teamCod)?.GetNombre() ?? "Sin equipo";
        }

        private string GetCiudadNameByCod(string cityCod)
        {
            return _nbaController?.GetAllEntitiesAsync<Ciudad>().Result.FirstOrDefault(c => c.GetCodCiudad() == cityCod)?.GetNombre() ?? "Sin ciudad";
        }

        // Update the player cards after filtering
        private async void UpdatePlayerCards(List<Jugador> players)
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
            foreach (var player in players)
            {
                var teamName = teams.FirstOrDefault(team => team.GetCodEquipo() == player.GetCodEquipo())?.GetNombre() ?? "Sin equipo";

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.GetNombre1()} {player.GetApellido1()}",
                    Description = $"{player.GetNumero()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);
                wrapPanel.Children.Add(card);
            }
        }

        // Clear all filters and reload the player cards
        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilter.Text = "";
            TeamFilter.SelectedIndex = -1;
            NumberFilter.Text = "";
            CityFilter.SelectedIndex = -1;
            LoadPlayerCards();
        }

        // Handle the player creation event
        private async void OnPlayerCreated(Jugador newJugador)
        {
            var wrapPanel = CardsContainer;
            var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
            var teamName = teams.FirstOrDefault(team => team.GetCodEquipo() == newJugador.GetCodEquipo())?.GetNombre() ?? "Sin equipo";

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{newJugador.GetNombre1()} {newJugador.GetApellido1()}",
                Description = $"{newJugador.GetNumero()} - {teamName}",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowPlayerDetails(newJugador);
            wrapPanel.Children.Add(card);
        }

        // Open the player creation dialog
        private void CreatePlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createPlayer = new CreatePlayer();
            createPlayer.PlayerAdded += OnPlayerCreated;
            createPlayer.ShowDialog();
        }
    }
}
