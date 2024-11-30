using System.Windows;
using System.Windows.Controls;

namespace Basket.Visual;

public partial class TeamDetails : UserControl
{
    public TeamDetails()
    {
        InitializeComponent();
    }
    
    public string Initials
    {
        get => TeamInitials.Text;
        set => TeamInitials.Text = value;
    }
    
    public new string Name
    {
        get => FullNameTextBlockHeader.Text;
        set => FullNameTextBlockHeader.Text = value;
    }
    
    public string FullName
    {
        get => FullNameTextBlockInfo.Text;
        set => FullNameTextBlockInfo.Text = value;
    }
    
    public string City
    {
        get => CityTextBlock.Text;
        set => CityTextBlock.Text = value;
    }


    public void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this);
        parentWindow?.Close();
    }
}