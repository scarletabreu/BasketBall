﻿using Basket.Classes;

namespace Basket.Controller;

public class NBA
{
    private List<Player> Players { get; set; }
    private List<Team> Teams { get; set; }
    private List<Game> Games { get; set; }
    private List<Stat> Stats { get; set; }
    private List<GameStat> GameStats { get; set; }
    private List<City> Cities { get; set; }
    private static NBA _instance;
    
    /*private NBA()
    {
        Players = new();
        Teams = new();
        Games = new();
        Stats = new();
        GameStats = new();
        Cities = new();
    }*/

    private NBA()
    {
        Players = new List<Player>
        {
            new Player("P001", "Michael", "Jeffrey", "Jordan", "", "CHI", new DateTime(1963, 2, 17), 23, "CHI"),
            new Player("P002", "LeBron", "Raymone", "James", "", "CLE", new DateTime(1984, 12, 30), 6, "LAL"),
            new Player("P003", "Kobe", "Bean", "Bryant", "", "PHI", new DateTime(1978, 8, 23), 24, "LAL"),
            new Player("P004", "Stephen", "", "Curry", "", "AKR", new DateTime(1988, 3, 14), 30, "GSW"),
            new Player("P005", "Kevin", "Wayne", "Durant", "", "WDC", new DateTime(1988, 9, 29), 7, "PHX")
        };
        
        Teams = new List<Team>
        {
            new Team("CHI", "Chicago Bulls", "Chicago"),
            new Team("LAL", "Los Angeles Lakers", "Los Angeles"),
            new Team("GSW", "Golden State Warriors", "San Francisco"),
            new Team("PHX", "Phoenix Suns", "Phoenix"),
            new Team("CLE", "Cleveland Cavaliers", "Cleveland")
        };
        
        Cities = new List<City>
        {
            new City("CHI", "Chicago"),
            new City("LAL", "Los Angeles"),
            new City("GSW", "San Francisco"),
            new City("PHX", "Phoenix"),
            new City("CLE", "Cleveland"),
            new City("NYC", "New York"),
            new City("MIA", "Miami"),
            new City("BOS", "Boston")
        };
        
        Games = new List<Game>
        {
            new Game("G001", "Bulls vs Lakers", "CHI", "LAL", new DateTime(2024, 1, 15)),
            new Game("G002", "Warriors vs Suns", "GSW", "PHX", new DateTime(2024, 1, 16)),
            new Game("G003", "Cavaliers vs Heat", "CLE", "MIA", new DateTime(2024, 1, 17)),
            new Game("G004", "Knicks vs Celtics", "NYC", "BOS", new DateTime(2024, 1, 18)),
            new Game("G005", "Lakers vs Warriors", "LAL", "GSW", new DateTime(2024, 1, 19))
        };
        
        Stats = new List<Stat>
        {
            new Stat("01", "Puntos de dos", 2),
            new Stat("02", "Puntos de tres", 3),
            new Stat("03", "Asistencias", 1),
            new Stat("04", "Rebotes", 1),
            new Stat("05", "Bolas robadas", 1),
            new Stat("06", "Faltas personales", 1),
            new Stat("07", "Faltas técnicas", 1),
            new Stat("08", "Bolas perdidas", 1),
            new Stat("09", "Tiros libres", 1)
        };
        GameStats = new List<GameStat>()
        {
            new GameStat("P001", "01", "G001", 20), // Michael Jordan anotó 20 puntos de dos en G001
            new GameStat("P001", "02", "G001", 5), // Michael Jordan anotó 5 triples en G001
            new GameStat("P002", "03", "G001", 8), // LeBron James realizó 8 asistencias en G001
            new GameStat("P003", "04", "G002", 10), // Kobe Bryant tomó 10 rebotes en G002
            new GameStat("P004", "05", "G002", 3), // Stephen Curry robó 3 bolas en G002
            new GameStat("P005", "06", "G003", 4) // Kevin Durant cometió 4 faltas personales en G003
        };
    }
    

    // Do singleton
    public static NBA Instance => _instance ??= new();
    
    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }
    
    public void AddTeam(Team team)
    {
        Teams.Add(team);
    }
    
    public void AddGame(Game game)
    {
        Games.Add(game);
    }
    
    public void AddStat(Stat stat)
    {
        Stats.Add(stat);
    }
    
    public void AddGameStat(GameStat gameStat)
    {
        GameStats.Add(gameStat);
    }
    
    public void AddCity(City city)
    {
        Cities.Add(city);
    }
    
    public List<Player> GetPlayers()
    {
        return Players;
    }
    
    public List<Team> GetTeams()
    {
        return Teams;
    }
    
    public List<Game> GetGames()
    {
        return Games;
    }
    
    public List<Stat> GetStats()
    {
        return Stats;
    }
    
    public List<GameStat> GetGameStats()
    {
        return GameStats;
    }
    
    public List<City> GetCities()
    {
        return Cities;
    }
    
    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
    }
    
    public void RemoveTeam(Team team)
    {
        Teams.Remove(team);
    }
    
    public void RemoveGame(Game game)
    {
        Games.Remove(game);
    }
    
    public void RemoveStat(Stat stat)
    {
        Stats.Remove(stat);
    }
    
    public void RemoveGameStat(GameStat gameStat)
    {
        GameStats.Remove(gameStat);
    }
    
    public void RemoveCity(City city)
    {
        Cities.Remove(city);
    }
    
    public void UpdatePlayer(Player player)
    {
        var index = Players.FindIndex(p => p.GetIdPlayer() == player.GetIdPlayer());
        Players[index] = player;
    }
    
    public void UpdateTeam(Team team)
    {
        var index = Teams.FindIndex(t => t.GetIdTeam() == team.GetIdTeam());
        Teams[index] = team;
    }
}