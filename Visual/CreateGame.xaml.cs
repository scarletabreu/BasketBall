using System;
using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual;

public partial class CreateGame : Window
{
    public readonly NBA _nbaController;

    public CreateGame()
    {
        InitializeComponent();
        _nbaController = NBA.GetInstance();
        LoadData();
    }

    private void LoadData()
    {
        var teams = _nbaController.GetTeams();

        foreach (var team in teams)
        {
            LocalTeam.Items.Add(new ComboBoxItem { Content = team.GetName(), Tag = team.GetIdTeam() });
            VisitorTeam.Items.Add(new ComboBoxItem { Content = team.GetName(), Tag = team.GetIdTeam() });
        }
    }

    public Action<Game> GameAdded { get; set; }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var description = Description.Text;
        var localTeam = (LocalTeam.SelectedItem as ComboBoxItem)?.Tag as string;
        var visitorTeam = (VisitorTeam.SelectedItem as ComboBoxItem)?.Tag as string;
        var date = GameDate.SelectedDate ?? DateTime.Now;

        if (string.IsNullOrWhiteSpace(description))
        {
            MessageBox.Show("Por favor, introduce una descripción válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(localTeam))
        {
            MessageBox.Show("Por favor, selecciona un equipo local.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(visitorTeam))
        {
            MessageBox.Show("Por favor, selecciona un equipo visitante.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (localTeam == visitorTeam)
        {
            MessageBox.Show("El equipo local y el equipo visitante no pueden ser el mismo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (date < DateTime.Now)
        {
            MessageBox.Show("Por favor, selecciona una fecha válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var game = new Game(Guid.NewGuid().ToString(), description, localTeam, visitorTeam, date);
        _nbaController.AddGame(game);
        GameAdded?.Invoke(game);

        MessageBox.Show($"Juego creado correctamente:\n\n" +
                        $"Descripción: {description}\n" +
                        $"Local: {GetTeamNameById(localTeam)}\n" +
                        $"Visitante: {GetTeamNameById(visitorTeam)}\n" +
                        $"Fecha: {date:dd/MM/yyyy}", 
                        "Información", MessageBoxButton.OK, MessageBoxImage.Information);

        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private string GetTeamNameById(string teamId)
    {
        var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == teamId);
        return team?.GetName() ?? "Sin equipo";
    }
}
