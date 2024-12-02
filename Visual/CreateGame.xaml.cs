using System.Windows;
using System.Windows.Controls;
using Basket.Classes;
using Basket.Controller;

namespace Basket.Visual
{
    public partial class CreateGame
    {
        private  readonly Nba? _nbaController;
        private List<EstadisticaJuego> _estadisticaJuegos;

        // Event to notify when a new game is added
        public Action<Juego>? GameAdded { get; set; }

        // Default constructor for WPF/XAML
        public CreateGame()
        {
            InitializeComponent();
            _nbaController = App.NbaInstance;
            LoadData();
        }

        // Load team data asynchronously
        private async void LoadData()
        {
            if (_nbaController == null)
            {
                MessageBox.Show("Controller not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var teams = await _nbaController.GetAllEntitiesAsync<Equipo>();

                // Add the teams to both the LocalTeam and VisitorTeam ComboBoxes
                foreach (var team in teams)
                {
                    var localComboBoxItem = new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() };
                    var visitorComboBoxItem = new ComboBoxItem { Content = team.GetNombre(), Tag = team.GetCodEquipo() };

                    // Add to LocalTeam ComboBox
                    LocalTeam.Items.Add(localComboBoxItem);

                    // Add to VisitorTeam ComboBox
                    VisitorTeam.Items.Add(visitorComboBoxItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Save button click handler
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_nbaController == null)
            {
                MessageBox.Show("Controller not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var description = Description.Text.Trim();
            var localTeamId = (LocalTeam.SelectedItem as ComboBoxItem)?.Tag as string;
            var visitorTeamId = (VisitorTeam.SelectedItem as ComboBoxItem)?.Tag as string;
            var date = GameDate.SelectedDate ?? DateTime.Now;

            // Input validation
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Por favor, introduce una descripción válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(localTeamId))
            {
                MessageBox.Show("Por favor, selecciona un equipo local.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(visitorTeamId))
            {
                MessageBox.Show("Por favor, selecciona un equipo visitante.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (localTeamId == visitorTeamId)
            {
                MessageBox.Show("El equipo local y el equipo visitante no pueden ser el mismo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (date < DateTime.Now)
            {
                MessageBox.Show("Por favor, selecciona una fecha válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var cantJuegos = (await _nbaController.GetAllEntitiesAsync<Juego>()).Count + 1;

            // Create game object
            try
            {
                // Create player object
                var juego = new Juego(
                    "JU" + cantJuegos.ToString("D3"),
                    description,
                    localTeamId,
                    visitorTeamId,
                    date
                );

                // Save player
                await _nbaController.AddEntityAsync(juego);

                MessageBox.Show("Juego guardado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                GameAdded?.Invoke(juego);
                Close();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error al guardar el juego: {ex.Message}";

                // Check if there is an InnerException and add it to the error message
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var count = _estadisticaJuegos.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    // Create EstadisticaJuego object
                    var estJue = _estadisticaJuegos[i];

                    // Save EstadisticaJuego
                    await _nbaController.AddEntityAsync(estJue);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error al guardar estadística de juego: {ex.Message}";

                    if (ex.InnerException != null)
                    {
                        errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                    }

                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            MessageBox.Show("Estadísticas de juego guardadas con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Cancel button click handler
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private async Task<List<Jugador>> GetTeamPlayersByCodTeam(string? codTeam)
        {
            var jugadores = new List<Jugador>();
            var allJugadores = await _nbaController?.GetAllEntitiesAsync<Jugador>()!;
            foreach (var j in allJugadores)
            {
                if(j.GetCodEquipo() == codTeam) jugadores.Add(j);
            }
            return jugadores;
        }

        private async void LocalPointsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (LocalTeam.SelectedItem is ComboBoxItem selectedItem)
            {
                var cantJuegos = (await _nbaController?.GetAllEntitiesAsync<Juego>()!).Count + 1;
                var gameStat = new CreateGameStat();
                gameStat.SetCodJuego("JU" + cantJuegos.ToString("D3"));
                var jugadores = await GetTeamPlayersByCodTeam(selectedItem.Tag.ToString()!);
                gameStat.SetJugadores(jugadores);
                Console.WriteLine(LocalTeam.SelectedItem.ToString());
                if (gameStat.ShowDialog() == true)
                {
                    _estadisticaJuegos.AddRange(gameStat.GetEstadisticaJuegos());
                }
            }
        }
        private async void VisitorPointsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (VisitorTeam.SelectedItem is ComboBoxItem selectedItem)
            {
                var cantJuegos = (await _nbaController?.GetAllEntitiesAsync<Juego>()!).Count + 1;
                var gameStat = new CreateGameStat();
                gameStat.SetCodJuego("JU" + cantJuegos.ToString("D3"));
                var jugadores = await GetTeamPlayersByCodTeam(selectedItem.Tag.ToString());
                gameStat.SetJugadores(jugadores);
                if (gameStat.ShowDialog() == true)
                {
                    _estadisticaJuegos.AddRange(gameStat.GetEstadisticaJuegos());
                }
            }
        }
    }
}
