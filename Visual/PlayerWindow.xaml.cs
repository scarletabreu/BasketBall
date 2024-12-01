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
            _nbaController = App.NbaInstance;
            LoadPlayerCards();
            LoadFilters();
        }

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

    private async void LoadFilters()
    {
        var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
        var players = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;
        var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

        // Team filter
        TeamFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });  // Empty Tag for "Todos"
        foreach (var team in teams)
        {
            TeamFilter.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
        }
        TeamFilter.SelectedIndex = 0;

        // Number filter
        var numbers = players.Select(player => player.GetNumero().ToString()).Distinct().ToList();
        NumberFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });  // Empty Tag for "Todos"
        foreach (var number in numbers)
        {
            NumberFilter.Items.Add(new ComboBoxItem { Content = number, Tag = number });
        }
        NumberFilter.SelectedIndex = 0;

        // Name filter
        var names = players.Select(player => player.GetNombre1()).Distinct().ToList();
        NameFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });  // Empty Tag for "Todos"
        foreach (var name in names)
        {
            NameFilter.Items.Add(new ComboBoxItem { Content = name, Tag = name });
        }
        NameFilter.SelectedIndex = 0;

        // City filter
        CityFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });  // Empty Tag for "Todos"
        foreach (var city in cities)
        {
            CityFilter.Items.Add(new ComboBoxItem { Content = city.GetNombre(), Tag = city.GetCodCiudad() });
        }
        CityFilter.SelectedIndex = 0;
    }

    private async void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Retrieve selected filter values
        var nameFilter = (NameFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();
        var selectedTeamTag = (TeamFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();
        var numberFilter = NumberFilter.Text.Trim();
        var selectedCityTag = (CityFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();

        // Fetch necessary data asynchronously
        var allPlayers = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;

        // Filter players based on selected filters
        var filteredPlayers = allPlayers.Where(player =>
            // Name filter: allow partial matching, but if "Todos" (empty Tag), match everything
            (string.IsNullOrEmpty(nameFilter) || player.GetNombre1().ToLower().Contains(nameFilter)) &&

            // Team filter: match the team code (Tag) correctly
            (string.IsNullOrEmpty(selectedTeamTag) || player.GetCodEquipo().ToLower() == selectedTeamTag) &&

            // Number filter: match exactly or allow "Todos"
            (numberFilter == "Todos" || player.GetNumero().ToString() == numberFilter) &&

            // City filter: allow partial matching, but if "Todos" (empty Tag), match everything
            (string.IsNullOrEmpty(selectedCityTag) || player.GetCiudadNacim().ToLower() == selectedCityTag)
        ).ToList();

        // Update the player cards with the filtered players
        await UpdatePlayerCards(filteredPlayers);
    }




        private async Task UpdatePlayerCards(List<Jugador> players)
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

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilter.SelectedIndex = 0;
            TeamFilter.SelectedIndex = 0;
            NumberFilter.SelectedIndex = 0;
            CityFilter.SelectedIndex = 0;
            LoadPlayerCards();
        }

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

        private void CreatePlayerButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createPlayer = new CreatePlayer();
            createPlayer.PlayerAdded += OnPlayerCreated;
            createPlayer.ShowDialog();
        }
    }
}
