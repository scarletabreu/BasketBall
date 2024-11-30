using System.ComponentModel.DataAnnotations;

namespace Basket.Classes;

public class Ciudad
{

    public string CodCiudad { get; set; }
    public string Nombre { get; set; }
    
    public ICollection<Equipo> Equipos { get; set; }
    public ICollection<Jugador> Jugadores { get; set; }
    
    public Ciudad(string codCiudad, string nombre)
    {
        CodCiudad = codCiudad;
        Nombre = nombre;
    }
    public Ciudad(){}
    
    public string GetCodCiudad()
    {
        return CodCiudad;
    }
    
    public string GetNombre()
    {
        return Nombre;
    }
    
    public void SetCodCiudad(string idCity)
    {
        CodCiudad = idCity;
    }
    
    public void SetNombre(string name)
    {
        Nombre = name;
    }
}