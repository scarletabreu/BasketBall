using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;
namespace Basket.Visual;

public partial class TeamWindow : Window
{
    private readonly NBA _nbaController;

    public TeamWindow()
    {
        InitializeComponent();
        this.Icon = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Users\\Scarlet\\Downloads\\Basket.png"));
        _nbaController = new NBA();
        LoadData();
        LoadFilters();
    }
    
    private void LoadData()
    {
        var wrapPanel = CardsContainer;
        wrapPanel.Children.Clear(); 
        
        var teams = _nbaController.GetTeams();

        foreach (var team in teams)
        {
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == team.GetCity());

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{GetTeamName(team.GetIdTeam())}",
                Description = city?.GetName() ?? "Sin ciudad",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowTeamDetails(team);

            wrapPanel.Children.Add(card);
            
        }
    }
    
    private void UpdateTeamsCards(List<Team> teams)
    {
        var wrapPanel = CardsContainer;
        wrapPanel.Children.Clear();
        
        foreach (var team in teams)
        {
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == team.GetCity());
            
            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = $"{GetTeamName(team.GetIdTeam())}",
                Description =  city?.GetName() ?? "Sin ciudad",
                ActionButtonText = "Ver detalles"
            };

            card.ActionClick += (s, e) => ShowTeamDetails(team);
            wrapPanel.Children.Add(card);
        }
    }

    private void LoadFilters()
    {
        var teams = _nbaController.GetTeams();
        
        TeamFilter.Items.Add(new ComboBoxItem {Content = " ", Tag = " "});
        foreach (var team in teams)
        {
            TeamFilter.Items.Add(new ComboBoxItem {Content = team.GetName(), Tag = team.GetIdTeam()});
        }
        TeamFilter.SelectedIndex = 0;
        
        var cities = _nbaController.GetCities().Select(city => city.GetName()).Distinct().ToList();
        
        CityFilter.Items.Add(new ComboBoxItem {Content = " ", Tag = " "});
        foreach (var city in cities)
        {
            CityFilter.Items.Add(new ComboBoxItem {Content = city, Tag = city});
        }
        CityFilter.SelectedIndex = 0;
    }

    private void ShowTeamDetails(Team team)
    {
        var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == team.GetCity());
        var initials = string.Concat(team.GetName()
            .Split(' ') 
            .Where(word => !string.IsNullOrWhiteSpace(word)) 
            .Select(word => word[0]));
        
        var teamDetails = new TeamDetails
        {
            Initials =initials,
            Name = team.GetName(),
            FullName = team.GetName(),
            TeamTextBlock = {Text = team.GetName()},
            City = city?.GetName() ?? "Sin ciudad",
            ListBox = {ItemsSource = _nbaController.GetPlayers().Where(player => player.GetIdTeam() == team.GetIdTeam()).Select(player => $"{player.GetName()} {player.GetLastName()}").ToList()}
        };
        
        var teamDetailsWindow = new Window
        {
            Title = "Detalles del equipo",
            Content = teamDetails,
            SizeToContent = SizeToContent.WidthAndHeight
        };

        teamDetailsWindow.ShowDialog();

    }

    private string GetTeamName(string idTeam)
    {
        var teams = _nbaController.GetTeams();
        foreach (var team in teams)
        {
            if (team.GetIdTeam() == idTeam)
            {
                return team.GetName();
            }
        }

        return "";
    }

    private string GetCityName(string idCity)
    {
        var cities = _nbaController.GetCities();
        foreach (var city in cities)
        {
            if (city.GetIdCity() == idCity)
            {
                return city.GetName();
            }
        }

        return "";
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedTeamName = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();
        var selectedCityName = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();
        
        var filteredTeams = _nbaController.GetTeams()
            .Where(player =>
                    (string.IsNullOrEmpty(selectedTeamName) || GetTeamName(player.GetIdTeam()).ToLower().Contains(selectedTeamName)) &&  
                    (string.IsNullOrEmpty(selectedCityName) || GetCityName(player.GetCity()).ToLower().Contains(selectedCityName))  
            )
            .ToList();
        
        UpdateTeamsCards(filteredTeams);
    }

    private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        TeamFilter.SelectedIndex = -1;
        CityFilter.SelectedIndex = -1;
        LoadData();
        
    }
    
    private void OnTeamCreated(Team team)
    {
        var wrapPanel = CardsContainer;
        var cities = _nbaController.GetCities();

        var cityName = cities.FirstOrDefault(city => team.GetCity() == city.GetIdCity())?.GetName() ?? "Sin Ciudad";

        var card = new Cards
        {
            Width = 280,
            Margin = new Thickness(5),
            Heading = $"{team.GetName()}",
            Description = $"{cityName}",
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