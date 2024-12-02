using System.Windows;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class CreateCity 
{
    private readonly Nba? _nbaController;
    public Action<Ciudad>? CityAdded { get; set; }

    public CreateCity()
    {
        InitializeComponent();
        InitializeComponent();
        _nbaController = App.NbaInstance;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        var cityName = CityName.Text.Trim();

        if (string.IsNullOrWhiteSpace(cityName))
        {
            MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var cantEquipos = (await _nbaController!.GetAllEntitiesAsync<Equipo>()).Count + 1;

        try
        {
            var ciudad = new Ciudad(
                "C-" + cantEquipos.ToString("D3"),
                cityName
                );

            await _nbaController.AddEntityAsync(ciudad);

            MessageBox.Show("Equipo guardado con éxito.", "Éxito", MessageBoxButton.OK,
                MessageBoxImage.Information);

            CityAdded?.Invoke(ciudad);
            Close();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error al guardar el equipo: {ex.Message}";

            if (ex.InnerException != null)
            {
                errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
            }

            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}