using System.Windows;
using System.Windows.Controls;

namespace Basket.Visual;

public partial class GameDetails : UserControl
{
    public GameDetails()
    {
        InitializeComponent();
    }
    
    public string Game
    {
        get { return GameInfo.Text; }
        set { GameInfo.Text = value; }
    }
    
    public string Description
    {
        get { return descriptionInfo.Text; }
        set { descriptionInfo.Text = value; }
    }
    
    public string Header
    {
        get { return GameHeader.Text; }
        set { GameHeader.Text = value; }
    }

    public string Initials
    {
        get { return GameInitials.Text; }
        set { GameInitials.Text = value; }
    }

    public string LocalTeam
    {
        get { return LocalTeamBlock.Text;}
        set { LocalTeamBlock.Text = value; }
    }
    
    public string VisitorTeam
    {
        get { return VisitorTextBlock.Text; }
        set { VisitorTextBlock.Text = value; }
    }

    public string LocalTeamInfo
    {
        get { return localTeamInfo.Text; }
        set {localTeamInfo.Text = value; }
    }
    
    public string VisitorTeamInfo
    {
        get { return VisitedTeamInfo.Text; }
        set { VisitedTeamInfo.Text = value; }
    }
    
    public string Date
    {
        get { return dateInfo.Text; }
        set { dateInfo.Text = value; }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement this method
        // Close the user control
    }

    private void ShowProcedure_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}