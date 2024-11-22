using System.Windows;
using System.Windows.Controls;
using Basket.Controller; // Asegúrate de tener el using correcto para tu controlador
using Basket.Classes;
using SharpVectors.Converters;
using System.Windows.Media.Imaging;

namespace Basket.Visual
{
    public partial class PlayerWindow : Window
    {
        private readonly NBA _nbaController;

        public PlayerWindow()
        {
            InitializeComponent();
            this.Icon = new System.Windows.Media.Imaging.BitmapImage(
                new Uri("C:\\Users\\Scarlet\\Downloads\\Basket.png"));
            _nbaController = new NBA();
            LoadPlayerCards();
            LoadFilters();
        }

        private void LoadPlayerCards()
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var players = _nbaController.GetPlayers();
            var teams = _nbaController.GetTeams();

            string teamName = "";

            foreach (var player in players)
            {
                foreach (var team in teams)
                {
                    if (team.GetIdTeam() == player.GetIdTeam())
                    {
                        teamName = team.Name;
                    }
                }

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.Name} {player.LastName}",
                    Description = $"{player.GetNumber()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);
                wrapPanel.Children.Add(card);
            }
        }


        private void ShowPlayerDetails(Player player)
        {
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == player.GetCity());
            var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == player.GetIdTeam());

            var playerDetails = new PlayerDetails
            {
                Name = $"{player.GetName()} {player.GetLastName()}",
                FullName =
                    $"{player.GetName()} {player.GetSecondName()} {player.GetLastName()} {player.GetSecondLastName()}",
                FullNameGeneral =
                    $"{player.GetName()} {player.GetSecondName()} {player.GetLastName()} {player.GetSecondLastName()}",
                BirthDay = player.GetBirthDay().ToString("dd/MM/yyyy"),
                Number = player.GetNumber().ToString(),
                City = city?.GetName() ?? "Sin ciudad",
                Team = team?.Name ?? "Sin equipo",
                Age = (DateTime.Now.Year - player.GetBirthDay().Year).ToString(),
                Initials = $"{player.GetName()[0]}{player.GetLastName()[0]}",
                num = player.GetNumber().ToString()

            };

            // Crear la ventana de detalles
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

        private void LoadFilters()
        {
            var teams = _nbaController.GetTeams();

            TeamFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var team in teams)
            {
                TeamFilter.Items.Add(new ComboBoxItem { Content = team.Name, Tag = team.GetIdTeam() });
            }

            TeamFilter.SelectedIndex = 0;

            var numbers = _nbaController.GetPlayers().Select(player => player.GetNumber().ToString()).Distinct()
                .ToList();

            NumberFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var number in numbers)
            {
                NumberFilter.Items.Add(new ComboBoxItem { Content = number, Tag = number });
            }

            NumberFilter.SelectedIndex = 0;

            var names = _nbaController.GetPlayers().Select(player => player.Name).Distinct().ToList();

            NameFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var name in names)
            {
                NameFilter.Items.Add(new ComboBoxItem { Content = name, Tag = name });
            }

            NameFilter.SelectedIndex = 0;

            var cities = _nbaController.GetCities().Select(city => city.GetName()).Distinct().ToList();

            CityFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var city in cities)
            {
                CityFilter.Items.Add(new ComboBoxItem { Content = city, Tag = city });
            }

            CityFilter.SelectedIndex = 0;
        }

        private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nameFilter = NameFilter.Text.ToLower();
            var selectedTeamName = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();
            var numberFilter = NumberFilter.Text;
            var selectedCityName = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();

            // Filtrar jugadores
            var filteredPlayers = _nbaController.GetPlayers()
                .Where(player =>
                    (string.IsNullOrEmpty(nameFilter) || player.GetName().ToLower().Contains(nameFilter)) &&
                    (string.IsNullOrEmpty(selectedTeamName) ||
                     GetTeamNameById(player.GetIdTeam()).ToLower().Contains(selectedTeamName)) &&
                    (string.IsNullOrEmpty(numberFilter) || player.GetNumber().ToString() == numberFilter) &&
                    (string.IsNullOrEmpty(selectedCityName) ||
                     GetCityNameById(player.GetCity()).ToLower().Contains(selectedCityName))
                )
                .ToList();

            UpdatePlayerCards(filteredPlayers);
        }

        private string GetTeamNameById(string teamId)
        {
            var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == teamId);
            return team?.Name ?? "Sin equipo";
        }

        private string GetCityNameById(string cityId)
        {
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == cityId);
            return city?.GetName() ?? "Sin ciudad";
        }


        private void UpdatePlayerCards(List<Player> players)
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var teams = _nbaController.GetTeams();

            foreach (var player in players)
            {
                var teamName = teams.FirstOrDefault(team => team.GetIdTeam() == player.GetIdTeam())?.GetName() ??
                               "Sin equipo";

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.GetName()} {player.GetLastName()}",
                    Description = $"{player.GetNumber()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);
                wrapPanel.Children.Add(card);
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilter.Text = "";
            TeamFilter.SelectedIndex = -1;
            NumberFilter.Text = "";
            CityFilter.SelectedIndex = -1;
            LoadPlayerCards();
        }
        
        private void OnPlayerCreated(Player newPlayer)
        {
            var wrapPanel = CardsContainer;
            var teams = _nbaController.GetTeams();

            var teamName = teams.FirstOrDefault(team => team.GetIdTeam() == newPlayer.GetIdTeam())?.GetName() ?? "Sin equipo";

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{newPlayer.GetName()} {newPlayer.GetLastName()}",
                Description = $"{newPlayer.GetNumber()} - {teamName}",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowPlayerDetails(newPlayer);
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