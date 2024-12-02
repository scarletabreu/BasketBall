using System.Windows;
using System.Windows.Controls;

namespace Basket.Visual;

public partial class CityDetails : UserControl
{
    public CityDetails()
    {
        InitializeComponent();
    }

    public string Header
    {
        get { return CityHeader.Text; }
        set { CityHeader.Text = value; }
    }
    
    public string InfoCity
    {
        get { return CityInfo.Text; }
        set { CityInfo.Text = value; }
    }
    
    public string Initials
    {
        get { return CityInitials.Text; }
        set { CityInitials.Text = value; }
    }
    
        
    public string CityId
    {
        get { return IdCity.Text; }
        set { IdCity.Text = value; }
    }

    public string Name
    {
        get { return CityName.Text; }
        set { CityName.Text = value; }
    }
    
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        var parent = (Panel)Parent;
        parent.Children.Remove(this);
    }
}