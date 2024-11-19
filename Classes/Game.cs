namespace Basket.Classes;

public class Game
{
    private string IdGame { get; set; }
    private string Description { get; set; }
    private string IdLocalTeam { get; set; }
    private string IdVisitorTeam { get; set; }
    private DateTime Date { get; set; }
    
    public Game(string idGame, string description, string localTeam, string visitorTeam, DateTime date)
    {
        IdGame = idGame;
        Description = description;
        IdLocalTeam = localTeam;
        IdVisitorTeam = visitorTeam;
        Date = date;
    }
    
    public string GetIdGame()
    {
        return IdGame;
    }
    
    public string GetDescription()
    {
        return Description;
    }
    
    public string GetLocalTeam()
    {
        return IdLocalTeam;
    }
    
    public string GetVisitorTeam()
    {
        return IdVisitorTeam;
    }
    
    public DateTime GetDate()
    {
        return Date;
    }
    
    public void SetLocalTeam(string localTeam)
    {
        IdLocalTeam = localTeam;
    }
    
    public void SetVisitorTeam(string visitorTeam)
    {
        IdVisitorTeam = visitorTeam;
    }
    
    public void SetDate(DateTime date)
    {
        Date = date;
    }
    
    public void SetDescription(string description)
    {
        Description = description;
    }
    
    public void SetIdGame(string idGame)
    {
        IdGame = idGame;
    }
}