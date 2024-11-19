namespace Basket.Classes;

public class Stat
{
    private string IdStat { get; set; }
    private string Description { get; set; }
    private int Value { get; set; }
    
    public Stat(string idStat, string description, int value)
    {
        IdStat = idStat;
        Description = description;
        Value = value;
    }
    
    public string GetIdStat()
    {
        return IdStat;
    }
    
    public string GetDescription()
    {
        return Description;
    }
    
    public int GetValue()
    {
        return Value;
    }
    
    public void SetValue(int value)
    {
        Value = value;
    }
    
    public void IncreaseValue(int value)
    {
        Value += value;
    }
    
    public void DecreaseValue(int value)
    {
        Value -= value;
    }
    
    public void ResetValue()
    {
        Value = 0;
    }
    
    public override string ToString()
    {
        return $"{Description}: {Value}";
    }
    
}