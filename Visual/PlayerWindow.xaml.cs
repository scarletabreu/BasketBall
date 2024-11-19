using System.Windows;

namespace Basket.Visual;

public partial class PlayerWindow : Window
{
    public PlayerWindow()
    {
        InitializeComponent();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}