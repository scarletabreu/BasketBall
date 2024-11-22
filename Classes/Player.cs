namespace Basket.Classes;

public class Player
{
    private string IdPlayer { get; set; }
    internal string Name { get; set; }
    private string SecondName { get; set; }
    internal string LastName { get; set; }
    private string SecondLastName { get; set; }
    private string IdCity { get; set; }
    private DateTime BirthDay { get; set; }
    private int Number { get; set; }
    private string IdTeam { get; set; }
    
    public Player(string idPlayer, string name, string secondName, string lastName, string secondLastName, string city, DateTime birthDay, int number, string idTeam)
    {
        IdPlayer = idPlayer;
        Name = name;
        SecondName = secondName;
        LastName = lastName;
        SecondLastName = secondLastName;
        IdCity = city;
        BirthDay = birthDay;
        Number = number;
        IdTeam = idTeam;
    }
    

    public string GetIdPlayer()
    {
        return IdPlayer;
    }
    
    public string GetName()
    {
        return Name;
    }
    
    public string GetSecondName()
    {
        return SecondName;
    }
    
    public string GetLastName()
    {
        return LastName;
    }
    
    public string GetSecondLastName()
    {
        return SecondLastName;
    }
    
    public string GetCity()
    {
        return IdCity;
    }
    
    public DateTime GetBirthDay()
    {
        return BirthDay;
    }
    
    public int GetNumber()
    {
        return Number;
    }
    
    public string GetIdTeam()
    {
        return IdTeam;
    }
    
    public void SetIdPlayer(string idPlayer)
    {
        IdPlayer = idPlayer;
    }
    
    public void SetName(string name)
    {
        Name = name;
    }
    
    public void SetSecondName(string secondName)
    {
        SecondName = secondName;
    }
    
    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }
    
    public void SetSecondLastName(string secondLastName)
    {
        SecondLastName = secondLastName;
    }
    
    public void SetCity(string city)
    {
        IdCity = city;
    }
    
    public void SetBirthDay(DateTime birthDay)
    {
        BirthDay = birthDay;
    }
    
    public void SetNumber(int number)
    {
        Number = number;
    }
    
    public void SetIdTeam(string idTeam)
    {
        IdTeam = idTeam;
    }
    
    public string GetFullName()
    {
        return $"{Name} {SecondName} {LastName} {SecondLastName}";
    }
    
    public string GetFullData()
    {
        return $"{IdPlayer} {Name} {SecondName} {LastName} {SecondLastName} {IdCity} {BirthDay} {Number} {IdTeam}";
    }
    
    public int GetAge()
    {
        return DateTime.Now.Year - BirthDay.Year;
    }
    
}