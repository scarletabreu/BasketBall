using System.ComponentModel.DataAnnotations;

namespace Basket.Classes
{
    public class Jugador
    {
        [Key]
        public string CodJugador { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string CiudadNacim { get; set; }
        public DateTime FechaNacim { get; set; }
        public int Numero { get; set; }
        public string CodEquipo { get; set; }
        
        public ICollection<EstadisticaJuego> EstadisticasJuegos { get; set; }
        public Equipo Equipo { get; set; }
        public Ciudad Ciudad { get; set; }
        
        public Jugador(){}
        
        // Constructor to initialize the properties
        
        public Jugador(string codJugador, string nombre1, string nombre2, string apellido1, string apellido2, string ciudadNacim, DateTime fechaNacim, int numero, string codEquipo)
        {
            CodJugador = codJugador;
            Nombre1 = nombre1;
            Nombre2 = nombre2;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            CiudadNacim = ciudadNacim;
            FechaNacim = fechaNacim;
            Numero = numero;
            CodEquipo = codEquipo;
        }

        // Getter and Setter for CodJugador
        public string GetCodJugador()
        {
            return CodJugador;
        }
        
        public void SetCodJugador(string idPlayer)
        {
            CodJugador = idPlayer;
        }

        // Getter and Setter for Nombre1
        public string GetNombre1()
        {
            return Nombre1;
        }
        
        public void SetNombre1(string nombre1)
        {
            Nombre1 = nombre1;
        }

        // Getter and Setter for Nombre2
        public string GetNombre2()
        {
            return Nombre2;
        }
        
        public void SetNombre2(string nombre2)
        {
            Nombre2 = nombre2;
        }

        // Getter and Setter for Apellido1
        public string GetApellido1()
        {
            return Apellido1;
        }

        public void SetApellido1(string apellido1)
        {
            Apellido1 = apellido1;
        }

        // Getter and Setter for Apellido2
        public string GetApellido2()
        {
            return Apellido2;
        }

        public void SetApellido2(string apellido2)
        {
            Apellido2 = apellido2;
        }

        // Getter and Setter for CiudadNacim
        public string GetCiudadNacim()
        {
            return CiudadNacim;
        }

        public void SetCiudadNacim(string ciudadNacim)
        {
            CiudadNacim = ciudadNacim;
        }

        // Getter and Setter for FechaNacim (Birthdate)
        public DateTime GetFechaNacim()
        {
            return FechaNacim;
        }

        public void SetFechaNacim(DateTime fechaNacim)
        {
            FechaNacim = fechaNacim;
        }

        // Getter and Setter for Numero
        public int GetNumero()
        {
            return Numero;
        }

        public void SetNumero(int numero)
        {
            Numero = numero;
        }

        // Getter and Setter for CodEquipo
        public string GetCodEquipo()
        {
            return CodEquipo;
        }

        public void SetCodEquipo(string codEquipo)
        {
            CodEquipo = codEquipo;
        }

        // Additional methods to get full name and full data
        public string GetFullNombre()
        {
            return $"{Nombre1} {Nombre2} {Apellido1} {Apellido2}";
        }

        public string GetFullData()
        {
            return $"{CodJugador} {Nombre1} {Nombre2} {Apellido1} {Apellido2} {CiudadNacim} {FechaNacim.ToShortDateString()} {Numero} {CodEquipo}";
        }

        // Calculate age based on birthdate
        public int GetAge()
        {
            return DateTime.Now.Year - FechaNacim.Year;
        }
    }
}
