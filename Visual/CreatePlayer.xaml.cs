using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class CreatePlayer : Window
{
    public readonly NBA _nbaController;
    public CreatePlayer()
    {
        InitializeComponent();
        _nbaController = NBA.GetInstance();

        if (_nbaController == null)
        {
            MessageBox.Show("Error al inicializar el controlador NBA. Verifica la configuración.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            this.Close();
            return;
        }
        LoadData();
    }
    
    public Action<Player> PlayerAdded { get; set; }

    private void LoadData()
    {
        var teams = _nbaController.GetTeams();
        var cities = _nbaController.GetCities();

        foreach (var team in teams)
        {
            TeamComboBox.Items.Add(new ComboBoxItem { Content = team.GetName(), Tag = team.GetIdTeam() });
        }

        foreach (var city in cities)
        {
            CityComboBox.Items.Add(new ComboBoxItem { Content = city.GetName(), Tag = city.GetIdCity() });
        }

    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        // Capturar los datos del formulario
        var playerName = PlayerName.Text;
        var secondName = SecondNameLabel.Text;
        var lastName = LastName.Text;
        var secondLastName = SecondLastNameLabel.Text;

        if (!int.TryParse(NumberLabel.Text, out var number))
        {
            MessageBox.Show("Por favor, introduce un número válido en el campo de Número.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var city = (CityComboBox.SelectedItem as ComboBoxItem)?.Tag as string;
        var team = (TeamComboBox.SelectedItem as ComboBoxItem)?.Tag as string;
        var birthDate = DatePickerQuery.SelectedDate;

        if (string.IsNullOrWhiteSpace(playerName) || 
            string.IsNullOrWhiteSpace(lastName) || 
            city == null || 
            team == null || 
            birthDate == null)
        {
            MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Player player = new Player("P007", playerName, secondName, lastName, secondLastName, city, birthDate.Value, number, team);
        
        bool isSaved = _nbaController.AddPlayer(player);

        if (isSaved)
        {
            MessageBox.Show("Jugador guardado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            PlayerAdded?.Invoke(player);
            this.Close();
        }
        else
        {
            MessageBox.Show("Ocurrió un error al guardar el jugador.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }   
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}