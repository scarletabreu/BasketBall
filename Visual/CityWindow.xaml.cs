using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class CityWindow : Window
{
    private  readonly Nba? _nbaController;
    public CityWindow()
    {
        InitializeComponent();
        _nbaController = App.NbaInstance;
        InitializeDataAsync();
    }
    
    private async Task InitializeDataAsync()
    {
        try
        {
            await LoadCityCards();
            await LoadFilters();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing data: {ex.Message}");
        }
    }
    
    private async Task LoadFilters()
    {
        var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;

        // Name filter
        var names = cities.Select(city => city.GetNombre()).Distinct().ToList();
        NameFilter.Items.Add(new ComboBoxItem { Content = "Todos", Tag = "" });  
        foreach (var name in names)
        {
            NameFilter.Items.Add(new ComboBoxItem { Content = name, Tag = name });
        }
        NameFilter.SelectedIndex = 0;
    }

    
    private async Task LoadCityCards()
    {
        try
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear();

            var cities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;
            foreach (var city in cities)
            {
                var card = await CreateCityCards(city);
                wrapPanel.Children.Add(card);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading game cards: {ex.Message}");
        }
    }
    
    private async void ShowCityDetails(Ciudad city)
    {
        var cityDetails = new CityDetails
        {
            Header = $"{city.GetNombre()}",
            Initials = $"{city.GetCodCiudad()}",
            InfoCity = city.GetNombre(),
            CityId = city.GetCodCiudad(),
            Name = city.GetNombre()
        };

        var cityDetailsWindow = new Window
        {
            Title = "Detalles de la Ciudad",
            Content = cityDetails,
            SizeToContent = SizeToContent.WidthAndHeight
        };

        cityDetailsWindow.ShowDialog();
    }
    
    private async Task<Cards> CreateCityCards(Ciudad city)
    {
        var ciudad = await GetCityNombreByCodAsync(city.GetCodCiudad());
        
        var card = new Cards
        {
            Width = 280,
            Margin = new Thickness(5),
            Heading = ciudad,
            ActionButtonText = "Ver detalles"
        };

        card.ActionClick += (s, e) => ShowCityDetails(city);
        return card;
    }
    
    private async Task<string> GetCityNombreByCodAsync(string cityCod)
    {
        var city = (await _nbaController?.GetAllEntitiesAsync<Ciudad>()!)?.FirstOrDefault(t => t.GetCodCiudad() == cityCod);
        return city?.GetNombre() ?? "Sin equipo";
    }

    private async void  ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
    {
        var nameFilter = (NameFilter.SelectedItem as ComboBoxItem)?.Tag?.ToString()?.ToLower().Trim();
        var allCities = await _nbaController?.GetAllEntitiesAsync<Ciudad>()!;
        
        var filteredCities = allCities.Where(city =>
            (string.IsNullOrEmpty(nameFilter) || city.GetNombre().ToLower().Contains(nameFilter)) 

        ).ToList();
        
        await UpdateCityCards(filteredCities);
    }
    
    private async Task UpdateCityCards(List<Ciudad> Cities)
    {
        var wrapPanel = CardsContainer;
        wrapPanel.Children.Clear();

        foreach (var city in Cities)
        {

            var card = new Cards
            {
                Width = 280,
                Margin = new Thickness(5),
                Heading = city.GetNombre(),
                ActionButtonText = "Ver detalles"
            };


            card.ActionClick += (s, e) => ShowCityDetails(city);
            wrapPanel.Children.Add(card);
        }
    }

    private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        NameFilter.SelectedIndex = 0;
        LoadCityCards();
    }
    
    private async void OnCityCreated(Ciudad city)
    {
        var wrapPanel = CardsContainer;
        var ciudad = await GetCityNombreByCodAsync(city.GetCodCiudad());
        
        var card = new Cards
        {
            Width = 280,
            Margin = new Thickness(5),
            Heading = ciudad,
            ActionButtonText = "Ver detalles"
        };

        card.ActionClick += (s, e) => ShowCityDetails(city);
        wrapPanel.Children.Add(card);    }
    
    private void CreateCityButton_OnClick(object sender, RoutedEventArgs e)
    {
        var createCity = new CreateCity();
        createCity.CityAdded += OnCityCreated;
        createCity.ShowDialog();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}