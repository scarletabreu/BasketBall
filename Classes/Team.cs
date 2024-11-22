namespace Basket.Classes;

public class Team
{
    private string IdTeam { get; set; }
    private string Name { get; set; }
    private string City { get; set; }
    
    public Team(string idTeam, string name, string city)
    {
        IdTeam = idTeam;
        Name = name;
        City = city;
    }
    
    public string GetIdTeam()
    {
        return IdTeam;
    }
    
    public string GetName()
    {
        return Name;
    }
    
    public string GetCity()
    {
        return City;
    }
    
    public void SetIdTeam(string idTeam)
    {
        IdTeam = idTeam;
    }
    
    public void SetName(string name)
    {
        Name = name;
    }
    
    public void SetCity(string city)
    {
        City = city;
    }
}