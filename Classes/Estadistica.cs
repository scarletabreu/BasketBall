using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Basket.Classes
{
    public class Estadistica
    {
        [Key]
        public string CodEstadistica { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }

        public ICollection<EstadisticaJuego> EstadisticaJuegos { get; set; }

        public Estadistica() { }

        public Estadistica(string codEstadistica, string descripcion, int valor)
        {
            CodEstadistica = codEstadistica;
            Descripcion = descripcion;
            Valor = valor;
        }

        public string GetCodEstadistica() => CodEstadistica;
        public string GetDescripcion() => Descripcion;
        public int GetValor() => Valor;
        public void SetValor(int value) => Valor = value;

        public void IncreaseValor(int value) => Valor += value;
        public void DecreaseValor(int value) => Valor -= value;
        public void ResetValor() => Valor = 0;

        public override string ToString()
        {
            return $"{Descripcion}: {Valor}";
        }
    }
}