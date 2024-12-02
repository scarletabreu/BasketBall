using System.Windows;
using Basket.Classes;

namespace Basket.Visual
{
    public partial class CreateGameStat
    {
        private List<PlayerViewModel> Players { get; set; }
        private string? _codJuego;
        public List<Jugador>? Jugadores { get; set; }
        private readonly List<EstadisticaJuego> _estadisticaJuegos = new List<EstadisticaJuego>();

        public void SetCodJuego(string codJuego)
        {
            _codJuego = codJuego;
        }
        
        public CreateGameStat()
        {
            InitializeComponent();
            Players = new List<PlayerViewModel>();

            // Set ListBox bindings to the Players list
            TwoPointPlayersListBox.ItemsSource = Players;
            ThreePointPlayersListBox.ItemsSource = Players;
            OnePointPlayersListBox.ItemsSource = Players;
            AssistsPlayersListBox.ItemsSource = Players;
            ReboundsPlayersListBox.ItemsSource = Players;
            StealsPlayersListBox.ItemsSource = Players;
            PersonalFoulsPlayersListBox.ItemsSource = Players;
            TechnicalFoulsPlayersListBox.ItemsSource = Players;
            TurnoversPlayersListBox.ItemsSource = Players;
        }
        
        private void CreateGameStat_Loaded(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        
        public void SetJugadores(List<Jugador> jugadores)
        {
            Jugadores = jugadores;
            Console.WriteLine($"Total players received: {jugadores?.Count ?? 0}");

            Players.Clear();
            foreach (var j in Jugadores)
            {
                var playerVM = new PlayerViewModel
                {
                    Name = $"{j.GetNombre1()} {j.Apellido1}",
                    CodJugador = j.CodJugador
                };
                Players.Add(playerVM);
                Console.WriteLine($"Added player: {playerVM.Name}");
            }

            RefreshAllListBoxes();
        }

        private void RefreshAllListBoxes()
        {
            TwoPointPlayersListBox.Items.Refresh();
            ThreePointPlayersListBox.Items.Refresh();
            OnePointPlayersListBox.Items.Refresh();
            AssistsPlayersListBox.Items.Refresh();
            ReboundsPlayersListBox.Items.Refresh();
            StealsPlayersListBox.Items.Refresh();
            PersonalFoulsPlayersListBox.Items.Refresh();
            TechnicalFoulsPlayersListBox.Items.Refresh();
            TurnoversPlayersListBox.Items.Refresh();
        }

        public List<EstadisticaJuego> GetEstadisticaJuegos()
        {
            return _estadisticaJuegos;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _estadisticaJuegos.Clear(); // Clear previous entries

            foreach (var player in Players)
            {
                // Add 2-point stats if score > 0
                if (player.TwoPointScore > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "01", _codJuego, player.TwoPointScore));
                }

                // Add 3-point stats if score > 0
                if (player.ThreePointScore > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "02", _codJuego, player.ThreePointScore));
                }

                // Add 1-point stats if score > 0
                if (player.OnePointScore > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "09", _codJuego, player.OnePointScore));
                }

                // Add Assists if score > 0
                if (player.Assists > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "03", _codJuego, player.Assists));
                }

                // Add Rebounds if score > 0
                if (player.Rebounds > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "04", _codJuego, player.Rebounds));
                }

                // Add Steals if score > 0
                if (player.Steals > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "05", _codJuego, player.Steals));
                }

                // Add Personal Fouls if score > 0
                if (player.PersonalFouls > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "06", _codJuego, player.PersonalFouls));
                }

                // Add Technical Fouls if score > 0
                if (player.TechnicalFouls > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "07", _codJuego, player.TechnicalFouls));
                }

                // Add Turnovers if score > 0
                if (player.Turnovers > 0)
                {
                    _estadisticaJuegos.Add(new EstadisticaJuego(player.CodJugador, "08", _codJuego, player.Turnovers));
                }
            }
            DialogResult = true;

            MessageBox.Show("Los datos se han guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            Close(); // Optionally close the window after saving
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close(); // Close the window without savin
        }
    }

    // ViewModel for a Player
    public class PlayerViewModel
    {
        public string Name { get; set; }
        public string CodJugador { get; set; } // Player's unique code
        public int TwoPointScore { get; set; }
        public int ThreePointScore { get; set; }
        public int OnePointScore { get; set; }

        // Additional properties for other stats
        public int Assists { get; set; }
        public int Rebounds { get; set; }
        public int Steals { get; set; }
        public int PersonalFouls { get; set; }
        public int TechnicalFouls { get; set; }
        public int Turnovers { get; set; }
    }
}