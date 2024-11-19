namespace Basket.Classes;

public class GameStat
{
    private string IdPlayer { get; set; }
    private string IdStat { get; set; }
    private string IdGame { get; set; }
    private int Value { get; set; }
    
    public GameStat(string idPlayer, string idStat, string idGame, int value)
    {
        IdPlayer = idPlayer;
        IdStat = idStat;
        IdGame = idGame;
        Value = value;
    }
    
    public string GetIdPlayer()
    {
        return IdPlayer;
    }
    
    public string GetIdStat()
    {
        return IdStat;
    }
    
    public string GetIdGame()
    {
        return IdGame;
    }
    
    public int GetValue()
    {
        return Value;
    }
    
    public void SetIdPlayer(string idPlayer)
    {
        IdPlayer = idPlayer;
    }
    
    public void SetIdStat(string idStat)
    {
        IdStat = idStat;
    }
    
    public void SetIdGame(string idGame)
    {
        IdGame = idGame;
    }
    
    public void SetValue(int value)
    {
        Value = value;
    }
    
    public override string ToString()
    {
        return $"IdPlayer: {IdPlayer}, IdStat: {IdStat}, IdGame: {IdGame}, Value: {Value}";
    }
}