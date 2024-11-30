using Basket.Classes;
using Microsoft.EntityFrameworkCore;

namespace Basket.DataBase
{
    public class DbConnection : DbContext
    {
        // Constructor accepts DbContextOptions which is required for DI and DbContext pooling
        public DbConnection(DbContextOptions<DbConnection> options)
            : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<Jugador> Jugador { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Juego> Juego { get; set; }
        public DbSet<Estadistica> Estadistica { get; set; }
        public DbSet<EstadisticaJuego> EstadisticaJuego { get; set; }

        // Fluent API configuration for entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Jugador entity
            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.HasKey(j => j.CodJugador);

                // Map private fields explicitly
                entity.Property<string>("CodJugador")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Nombre1")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property<string>("Nombre2")
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property<string>("Apellido1")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property<string>("Apellido2")
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property<string>("CiudadNacim")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<DateTime>("FechaNacim")
                      .IsRequired();

                entity.Property<int>("Numero")
                      .IsRequired();

                entity.Property<string>("CodEquipo")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                // Navigation and relationships
                entity.HasOne(j => j.Equipo)
                      .WithMany(e => e.Jugadores)
                      .HasForeignKey(j => j.CodEquipo)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(j => j.Ciudad)
                      .WithMany(c => c.Jugadores)
                      .HasForeignKey(j => j.CiudadNacim)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the Equipo entity
            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.CodEquipo);

                entity.Property<string>("CodEquipo")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Nombre")
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property<string>("CodCiudad")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.HasOne(e => e.Ciudad)
                      .WithMany(c => c.Equipos)
                      .HasForeignKey(e => e.CodCiudad)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the Ciudad entity
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(c => c.CodCiudad);

                entity.Property<string>("CodCiudad")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Nombre")
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Configure the Juego entity
            modelBuilder.Entity<Juego>(entity =>
            {
                entity.HasKey(j => j.CodJuego);

                entity.Property<string>("CodJuego")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Descripcion")
                      .HasMaxLength(250)
                      .IsRequired(false);

                entity.Property<string>("Equipo1")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Equipo2")
                      .HasMaxLength(5)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<DateTime>("Fecha")
                      .IsRequired();

                entity.HasOne(j => j.EquipoLocal)
                      .WithMany(e => e.JuegosLocal)
                      .HasForeignKey(j => j.Equipo1)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(j => j.EquipoVisitante)
                      .WithMany(e => e.JuegosVisitante)
                      .HasForeignKey(j => j.Equipo2)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the Estadistica entity
            modelBuilder.Entity<Estadistica>(entity =>
            {
                entity.HasKey(e => e.CodEstadistica);

                entity.Property<string>("CodEstadistica")
                      .HasMaxLength(2)
                      .IsFixedLength()
                      .IsRequired();

                entity.Property<string>("Descripcion")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property<int>("Valor")
                      .IsRequired();
            });

            // Configure the EstadisticaJuego entity
            modelBuilder.Entity<EstadisticaJuego>(entity =>
            {
                entity.HasKey(ej => new { ej.CodJuego, ej.CodEstadistica, ej.CodJugador });

                entity.HasOne(ej => ej.Estadistica)
                      .WithMany(e => e.EstadisticaJuegos)
                      .HasForeignKey(ej => ej.CodEstadistica);

                entity.HasOne(ej => ej.Juego)
                      .WithMany(j => j.EstadisticasJuegos)
                      .HasForeignKey(ej => ej.CodJuego);

                entity.HasOne(ej => ej.Jugador)
                      .WithMany(j => j.EstadisticasJuegos)
                      .HasForeignKey(ej => ej.CodJugador);
            });
        }
    }
}
