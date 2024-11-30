using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Basket.Classes
{
    public class Juego
    {
        [Key]
        public string CodJuego { get; set; }
        public string Descripcion { get; set; }
        public string Equipo1 { get; set; }
        public string Equipo2 { get; set; }
        public DateTime Fecha { get; set; }

        // Navigation properties (if related to EstadisticaJuego and Jugador)
        public ICollection<EstadisticaJuego> EstadisticasJuegos { get; set; }
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        
        public Juego() { }

        public Juego(string codJuego, string descripcion, string localTeam, string visitorTeam, DateTime fecha)
        {
            CodJuego = codJuego;
            Descripcion = descripcion;
            Equipo1 = localTeam;
            Equipo2 = visitorTeam;
            Fecha = fecha;
        }

        // Getter and Setter methods
        public string GetCodJuego() => CodJuego;
        public string GetDescripcion() => Descripcion;
        public string GetEquipo1() => Equipo1;
        public string GetEquipo2() => Equipo2;
        public DateTime GetFecha() => Fecha;

        public void SetEquipo1(string localTeam) => Equipo1 = localTeam;
        public void SetEquipo2(string visitorTeam) => Equipo2 = visitorTeam;
        public void SetFecha(DateTime date) => Fecha = date;
        public void SetDescripcion(string description) => Descripcion = description;
        public void SetCodJuego(string idGame) => CodJuego = idGame;
    }
}