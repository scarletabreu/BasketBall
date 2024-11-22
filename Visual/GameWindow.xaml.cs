using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class GameWindow : Window
{
    private readonly NBA _nbaController;
    public GameWindow()
    {
        InitializeComponent();
        this.Icon = new System.Windows.Media.Imaging.BitmapImage(
            new Uri("C:\\Users\\Scarlet\\Downloads\\Basket.png"));
        _nbaController = NBA.GetInstance();
        LoadGameCards();
        LoadFilters();
    }
    
    private void LoadGameCards()
    {
        var wrapPanel = CardsContainer;
        wrapPanel.Children.Clear();

        var games = _nbaController.GetGames();

        foreach (var game in games)
        {
            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{game.GetDescription()}",
                Description = $"{GetTeamNameById(game.GetLocalTeam())} VS {GetTeamNameById(game.GetVisitorTeam())}",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowGameDetails(game);
            wrapPanel.Children.Add(card);
        }
    }
    
    private void ShowGameDetails(Game game)
    {

        var gameDetails = new GameDetails()
        {
            Header = $"{game.GetDescription()}",
            Initials = $"{GetTeamNameById(game.GetLocalTeam())[0]} vs {GetTeamNameById(game.GetVisitorTeam())[0]}",
            LocalTeam = $"{GetTeamNameById(game.GetLocalTeam())}",
            LocalTeamInfo = $"{GetTeamNameById(game.GetLocalTeam())}",
            VisitorTeam = $"{GetTeamNameById(game.GetVisitorTeam())}",
            VisitorTeamInfo = $"{GetTeamNameById(game.GetVisitorTeam())}",
            Description = $"{game.GetDescription()}",
            Game = $"{game.GetDescription()}",
            Date = $"{game.GetDate():dd/MM/yyyy}"

        };

        var playerDetailsWindow = new Window
        {
            Title = "Detalles del Juego",
            Content = gameDetails,
            SizeToContent = SizeToContent.WidthAndHeight
        };

        playerDetailsWindow.ShowDialog();
    }
    
    private string GetTeamNameById(string teamId)
    {
        var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == teamId);
        return team?.GetName() ?? "Sin equipo";
    }


    public void LoadFilters()
    {
        var teams = _nbaController.GetTeams();
        
        LocalTeamFilter.Items.Add(new ComboBoxItem {Content = " ", Tag = " "});
        foreach (var team in teams)
        {
            LocalTeamFilter.Items.Add(new ComboBoxItem {Content = team.GetName(), Tag = team.GetIdTeam()});
        }
        LocalTeamFilter.SelectedIndex = 0;
        
        VisitorTeamFilter.Items.Add(new ComboBoxItem {Content = " ", Tag = " "});
        foreach (var team in teams)
        {
            VisitorTeamFilter.Items.Add(new ComboBoxItem {Content = team.GetName(), Tag = team.GetIdTeam()});
        }
        VisitorTeamFilter.SelectedIndex = 0;
        
        var dates = _nbaController.GetGames().Select(game => game.GetDate().ToString("dd/MM/yyyy")).Distinct().ToList();
        
        DateFilter.Items.Add(new ComboBoxItem {Content = " ", Tag = " "});
        foreach (var date in dates)
        {
            DateFilter.Items.Add(new ComboBoxItem {Content = date, Tag = date});
        }
        DateFilter.SelectedIndex = 0;
        
    }
    private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
    {
        var localTeam = (LocalTeamFilter.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower();
        var visitorTeam = (VisitorTeamFilter.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower();
        var date = (DateFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();

        var allGames = _nbaController.GetGames();

        var filteredGames = allGames.Where(game =>
            (string.IsNullOrEmpty(localTeam) || GetTeamNameById(game.GetLocalTeam()).ToLower().Contains(localTeam)) &&
            (string.IsNullOrEmpty(visitorTeam) || GetTeamNameById(game.GetVisitorTeam()).ToLower().Contains(visitorTeam)) ||
            (string.IsNullOrEmpty(date) || game.GetDate().ToString("dd/MM/yyyy").Equals(date))
        ).ToList();

        UpdateGameCards(filteredGames);
    }

    private void UpdateGameCards(List<Game> games)
    {
        var wrapPanel = CardsContainer;
        wrapPanel.Children.Clear();
        
        foreach (var game in games)
        {

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{game.GetDescription()}",
                Description = $"{GetTeamNameById(game.GetLocalTeam())} VS {GetTeamNameById(game.GetVisitorTeam())}",
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
    
    private void OnGameCreated(Game newGame)
    {
        var wrapPanel = CardsContainer;

        var games = _nbaController.GetGames();
        
        var card = new Cards
        {
            Width = 280,
            Margin = new Thickness(5),
            Heading = $"{newGame.GetDescription()}",
            Description = $"{GetTeamNameById(newGame.GetLocalTeam())} VS {GetTeamNameById(newGame.GetVisitorTeam())}",
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