namespace Basket.Classes;

public class City
{
    private string IdCity { get; set; }
    private string Name { get; set; }
    
    public City(string idCity, string name)
    {
        IdCity = idCity;
        Name = name;
    }
    
    public string GetIdCity()
    {
        return IdCity;
    }
    
    public string GetName()
    {
        return Name;
    }
    
    public void SetIdCity(string idCity)
    {
        IdCity = idCity;
    }
    
    public void SetName(string name)
    {
        Name = name;
    }
}