using System.ComponentModel.DataAnnotations;

namespace Basket.Classes;

public class EstadisticaJuego
{
    [Key]
    public string CodJugador { get; set; }
    [Key]
    public string CodEstadistica { get; set; }
    [Key]
    public string CodJuego { get; set; }
    public int Cantidad { get; set; }
    
    public Estadistica Estadistica { get; set; }
    public Juego Juego { get; set; }
    public Jugador Jugador { get; set; }
    
    public EstadisticaJuego(){}
    public EstadisticaJuego(string codJugador, string codEstadistica, string codJuego, int cantidad)
    {
        CodJugador = codJugador;
        CodEstadistica = codEstadistica;
        CodJuego = codJuego;
        Cantidad = cantidad;
    }
    
    public string GetCodJugador()
    {
        return CodJugador;
    }
    
    public string GetCodEstadistica()
    {
        return CodEstadistica;
    }
    
    public string GetCodJuego()
    {
        return CodJuego;
    }
    
    public int GetCantidad()
    {
        return Cantidad;
    }
    
    public void SetCodJugador(string idPlayer)
    {
        CodJugador = idPlayer;
    }
    
    public void SetCodEstadistica(string idStat)
    {
        CodEstadistica = idStat;
    }
    
    public void SetCodJuego(string idGame)
    {
        CodJuego = idGame;
    }
    
    public void SetCantidad(int value)
    {
        Cantidad = value;
    }
    
    public override string ToString()
    {
        return $"CodJugador: {CodJugador}, CodEstadistica: {CodEstadistica}, IdGame: {CodJuego}, Cantidad: {Cantidad}";
    }
}