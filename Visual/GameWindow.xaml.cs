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
            _ = InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            try
            {
                await LoadGameCards();
                await LoadFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing data: {ex.Message}");
            }
        }

        private async Task LoadGameCards()
        {
            try
            {
                var wrapPanel = CardsContainer;
                wrapPanel.Children.Clear();

                var games = await _nbaController?.GetAllEntitiesAsync<Juego>()!;
                foreach (var game in games)
                {
                    var card = await CreateGameCard(game);
                    wrapPanel.Children.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading game cards: {ex.Message}");
            }
        }

        private async Task LoadFilters()
        {
            try
            {
                var teams = await _nbaController?.GetAllEntitiesAsync<Equipo>()!;
                var games = await _nbaController?.GetAllEntitiesAsync<Juego>()!;

                await Dispatcher.InvokeAsync(() =>
                {
                    LoadTeamFilter(LocalTeamFilter, teams);
                    LoadTeamFilter(VisitorTeamFilter, teams);

                    var dates = games.Select(game => game.GetFecha().ToString("dd/MM/yyyy")).Distinct().ToList();
                    DateFilter.Items.Clear();
                    DateFilter.Items.Add(new ComboBoxItem { Content = "Todas", Tag = "" });
                    foreach (var date in dates)
                    {
                        DateFilter.Items.Add(new ComboBoxItem { Content = date, Tag = date });
                    }
                    DateFilter.SelectedIndex = 0;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filters: {ex.Message}");
            }
        }

        private void LoadTeamFilter(ComboBox filter, List<Equipo> teams)
        {
            filter.Items.Clear();
            filter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });
            foreach (var team in teams)
            {
                filter.Items.Add(new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() });
            }
            filter.SelectedIndex = 0;
        }

        private async void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var localEquipo = (LocalTeamFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();
                var visitorEquipo = (VisitorTeamFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();
                var date = (DateFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
                
                var allJuegos = await _nbaController?.GetAllEntitiesAsync<Juego>()!;
                Console.WriteLine(localEquipo + '-' + visitorEquipo + '-' + date);
                var filteredJuegos = allJuegos.Where(game =>
                    (string.IsNullOrEmpty(localEquipo) || game.GetEquipo1().ToLower() == localEquipo) &&
                    (string.IsNullOrEmpty(visitorEquipo) || game.GetEquipo2().ToLower() == visitorEquipo) &&
                    (date == "Todas" || game.GetFecha().ToString("dd/MM/yyyy") == date)
                ).ToList();

                await UpdateGameCards(filteredJuegos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}");
            }
        }

        private async Task UpdateGameCards(List<Juego> games)
        {
            try
            {
                await Dispatcher.InvokeAsync(async () =>
                {
                    var wrapPanel = CardsContainer;
                    wrapPanel.Children.Clear();
                    foreach (var game in games)
                    {
                        // Await the task to get the card before adding it
                        var card = await CreateGameCard(game);
                        wrapPanel.Children.Add(card);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating game cards: {ex.Message}");
            }
        }


        private async Task<Cards> CreateGameCard(Juego game)
        {
            var localTeamName = await GetEquipoNombreByCodAsync(game.GetEquipo1());
            var visitorTeamName = await GetEquipoNombreByCodAsync(game.GetEquipo2());

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{game.GetDescripcion()}",
                Description = $"{localTeamName} VS {visitorTeamName}",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowGameDetails(game);
            return card;
        }

        private async void ShowGameDetails(Juego game)
        {
            var gameDetails = new GameDetails
            {
                Header = $"{game.GetCodJuego()}",
                Initials = $"{(await GetEquipoNombreByCodAsync(game.GetEquipo1()))[0]} vs {(await GetEquipoNombreByCodAsync(game.GetEquipo2()))[0]}",
                LocalTeam = await GetEquipoNombreByCodAsync(game.GetEquipo1()),
                LocalTeamInfo = await GetEquipoNombreByCodAsync(game.GetEquipo1()),
                VisitorTeam = await GetEquipoNombreByCodAsync(game.GetEquipo2()),
                VisitorTeamInfo = await GetEquipoNombreByCodAsync(game.GetEquipo2()),
                Description = game.GetDescripcion(),
                Game = game.GetDescripcion(),
                Date = game.GetFecha().ToString("dd/MM/yyyy")
            };

            var gameDetailsWindow = new Window
            {
                Title = "Detalles del Juego",
                Content = gameDetails,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            gameDetailsWindow.ShowDialog();
        }

        private async Task<string> GetEquipoNombreByCodAsync(string teamCod)
        {
            var team = (await _nbaController?.GetAllEntitiesAsync<Equipo>()!)?.FirstOrDefault(t => t.GetCodEquipo() == teamCod);
            return team?.GetNombre() ?? "Sin equipo";
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            LocalTeamFilter.SelectedIndex = 0;
            VisitorTeamFilter.SelectedIndex = 0;
            DateFilter.SelectedIndex = 0;
            _ = LoadGameCards();
        }

        private async void CreateGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Recreate the dialog each time
                var createGame = new CreateGame();
                createGame.GameAdded += OnGameCreated;
        
                // Show the dialog
                createGame.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening create game dialog: {ex.Message}");
            }
        }

        private async void OnGameCreated(Juego newGame)
        {
            try
            {
                var wrapPanel = CardsContainer;
        
                // Ensure the card does not have an existing parent before adding it
                var card = await CreateGameCard(newGame);

                if (card.Parent != null)
                {
                    // If the card already has a parent, remove it
                    var parent = (Panel)card.Parent;
                    parent.Children.Remove(card);
                }

                wrapPanel.Children.Add(card);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding game card: {ex.Message}");
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
