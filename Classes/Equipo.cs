using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Basket.Classes
{
    public class Equipo
    {
        [Key]
        public string CodEquipo { get; set; }
        public string Nombre { get; set; }
        public string CodCiudad { get; set; }

        // Navigation property for related Jugadores
        public ICollection<Jugador> Jugadores { get; set; }

        // Navigation property for related Juegos
        public ICollection<Juego> JuegosLocal { get; set; } = new List<Juego>();
        public ICollection<Juego> JuegosVisitante { get; set; } = new List<Juego>();
        
        public Ciudad Ciudad { get; set; }

        public Equipo() { }

        public Equipo(string codEquipo, string nombre, string codCiudad)
        {
            CodEquipo = codEquipo;
            Nombre = nombre;
            CodCiudad = codCiudad;
        }

        public string GetCodEquipo() => CodEquipo;
        public string GetNombre() => Nombre;
        public string GetCodCiudad() => CodCiudad;

        public void SetCodEquipo(string idTeam) => CodEquipo = idTeam;
        public void SetNombre(string name) => Nombre = name;
        public void SetCodCiudad(string city) => CodCiudad = city;
    }
}