using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class GameWindow
    {
        private readonly Nba? _nbaController;
        
        public GameWindow()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadGameCards();
            LoadFilters();
        }
        private async void LoadGameCards()
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var games = await _nbaController?.GetAllEntitiesAsync<Juego>()!;

            foreach (var game in games)
            {
                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{game.GetDescripcion()}",
                    Description = $"{GetEquipoNombreByCod(game.GetEquipo1())} VS {GetEquipoNombreByCod(game.GetEquipo2())}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowGameDetails(game);
                wrapPanel.Children.Add(card);
            }
        }

        private void ShowGameDetails(Juego game)
        {
            var gameDetails = new GameDetails
            {
                Header = $"{game.GetDescripcion()}",
                Initials = $"{GetEquipoNombreByCod(game.GetEquipo1())[0]} vs {GetEquipoNombreByCod(game.GetEquipo2())[0]}",
                LocalTeam = $"{GetEquipoNombreByCod(game.GetEquipo1())}",
                LocalTeamInfo = $"{GetEquipoNombreByCod(game.GetEquipo1())}",
                VisitorTeam = $"{GetEquipoNombreByCod(game.GetEquipo2())}",
                VisitorTeamInfo = $"{GetEquipoNombreByCod(game.GetEquipo2())}",
                Description = $"{game.GetDescripcion()}",
                Game = $"{game.GetDescripcion()}",
                Date = $"{game.GetFecha():dd/MM/yyyy}"
            };

            var playerDetailsWindow = new Window
            {
                Title = "Detalles del Juego",
                Content = gameDetails,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            playerDetailsWindow.ShowDialog();
        }

        private string GetEquipoNombreByCod(string teamCod)
        {
            var team = _nbaController?.GetAllEntitiesAsync<Equipo>().Result.FirstOrDefault(t => t.GetCodEquipo() == teamCod);
            return team?.GetNombre() ?? "Sin equipo";
        }

        public async void LoadFilters()
        {
            var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;

            LocalTeamFilter.Items.Add(new ComboBoxItem { Content = " ", Tag = " " });
            foreach (var team in teams)
            {
                LocalTeamFilter.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
            }
            LocalTeamFilter.SelectedIndex = 0;

            VisitorTeamFilter.Items.Add(new ComboBoxItem { Content = " ", Tag = " " });
            foreach (var team in teams)
            {
                VisitorTeamFilter.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
            }
            VisitorTeamFilter.SelectedIndex = 0;

            var dates = (await _nbaController?.GetAllEntitiesAsync<Juego>()!).Select(game => game.GetFecha().ToString("dd/MM/yyyy")).Distinct().ToList();

            DateFilter.Items.Add(new ComboBoxItem { Content = " ", Tag = " " });
            foreach (var date in dates)
            {
                DateFilter.Items.Add(new ComboBoxItem { Content = date, Tag = date });
            }
            DateFilter.SelectedIndex = 0;
        }

        private async void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            var localEquipo = (LocalTeamFilter.SelectedItem as ComboBoxItem)?.Content?.ToString()?.ToLower();
            var visitorEquipo = (VisitorTeamFilter.SelectedItem as ComboBoxItem)?.Content?.ToString()?.ToLower();
            var date = (DateFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();

            var allJuegos = await _nbaController?.GetAllEntitiesAsync<Juego>()!;

            var filteredJuegos = allJuegos.Where(game =>
                (string.IsNullOrEmpty(localEquipo) || GetEquipoNombreByCod(game.GetEquipo1()).ToLower().Contains(localEquipo)) &&
                (string.IsNullOrEmpty(visitorEquipo) || GetEquipoNombreByCod(game.GetEquipo2()).ToLower().Contains(visitorEquipo)) ||
                (string.IsNullOrEmpty(date) || game.GetFecha().ToString("dd/MM/yyyy").Equals(date))
            ).ToList();

            UpdateGameCards(filteredJuegos);
        }

        private void UpdateGameCards(List<Juego> games)
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            foreach (var game in games)
            {
                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{game.GetDescripcion()}",
                    Description = $"{GetEquipoNombreByCod(game.GetEquipo1())} VS {GetEquipoNombreByCod(game.GetEquipo2())}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowGameDetails(game);
                wrapPanel.Children.Add(card);
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            LocalTeamFilter.SelectedIndex = 0;
            VisitorTeamFilter.SelectedIndex = 0;
            DateFilter.SelectedIndex = 0;
            LoadGameCards();
        }

        private void OnGameCreated(Juego newGame)
        {
            var wrapPanel = CardsContainer;

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{newGame.GetDescripcion()}",
                Description = $"{GetEquipoNombreByCod(newGame.GetEquipo1())} VS {GetEquipoNombreByCod(newGame.GetEquipo2())}",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowGameDetails(newGame);
            wrapPanel.Children.Add(card);
        }

        private void CreateGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createGame = new CreateGame();
            createGame.GameAdded += OnGameCreated;
            createGame.ShowDialog();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
