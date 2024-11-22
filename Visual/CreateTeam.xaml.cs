using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class CreateTeam : Window
{
    public readonly NBA _nbaController;
    public CreateTeam()
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
    
    public Action<Team> TeamAdded { get; set; }

    private void LoadData()
    {
        var cities = _nbaController.GetCities();
        
        foreach (var city in cities)
        {
            CityComboBox.Items.Add(new ComboBoxItem { Content = city.GetName(), Tag = city.GetIdCity() });
        }

    }
    
    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var teamName = TeamName.Text;
        var city = (CityComboBox.SelectedItem as ComboBoxItem)?.Tag as string;
        
        if (string.IsNullOrWhiteSpace(teamName) || city == null)
        {
            MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        var team = new Team(Guid.NewGuid().ToString(), teamName, city);
        _nbaController.AddTeam(team);
        TeamAdded?.Invoke(team);
        this.Close();
        
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    

}