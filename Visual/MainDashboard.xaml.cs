using System.Windows;

namespace Basket.Visual;

public partial class MainDashboard : Window
{
    public MainDashboard()
    {
        InitializeComponent();
        this.Icon = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Users\\Scarlet\\Downloads\\Basket.png"));
    }
    
    private void Cards_ActionClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Botón de acción presionado");
    }


    private void PlayerButton_OnClick(object sender, RoutedEventArgs e)
    {
        PlayerWindow playerWindow = new();
        playerWindow.Show();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TeamButton_OnClick(object sender, RoutedEventArgs e)
    {
        TeamWindow teamWindow = new();
        teamWindow.Show();
    }

    private void GameButton_OnClick(object sender, RoutedEventArgs e)
    {
        GameWindow gameWindow = new();
        gameWindow.Show();
    }
}