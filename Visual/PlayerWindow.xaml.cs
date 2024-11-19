using System.Windows;
using System.Windows.Controls;
using Basket.Controller; // Asegúrate de tener el using correcto para tu controlador
using Basket.Classes;

namespace Basket.Visual
{
    public partial class PlayerWindow : Window
    {
        private readonly NBA _nbaController; 

        public PlayerWindow()
        {
            InitializeComponent();
            this.Icon = new System.Windows.Media.Imaging.BitmapImage(new Uri("C:\\Users\\Scarlet\\Downloads\\Basket.png"));
            _nbaController = new NBA(); 
            LoadPlayerCards();
            LoadFilters();
        }

        private void LoadPlayerCards()
        {
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear(); // Limpia las cards existentes

            // Obtiene la lista de jugadores del controlador
            var players = _nbaController.GetPlayers();
            var teams = _nbaController.GetTeams();

            string teamName = "";

            foreach (var player in players)
            {
                foreach (var team in teams)
                {
                    if (team.GetIdTeam() == player.GetIdTeam())
                    {
                        teamName = team.Name;
                    }
                }

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.Name} {player.LastName}",
                    Description = $"{player.GetNumber()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);

                wrapPanel.Children.Add(card);
            }
        }


        private void ShowPlayerDetails(Player player)
        {
            // Obtener el nombre del equipo y la ciudad con una verificación de nulo
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == player.GetCity());
            var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == player.GetIdTeam());

            var playerDetails = new PlayerDetails
            {
                Name = $"{player.GetName()} {player.GetLastName()}",     
                FullName = $"{player.GetName()} {player.GetSecondName()} {player.GetLastName()} {player.GetSecondLastName()}",
                FullNameGeneral = $"{player.GetName()} {player.GetSecondName()} {player.GetLastName()} {player.GetSecondLastName()}",
                BirthDay = player.GetBirthDay().ToString("dd/MM/yyyy"),
                Number = player.GetNumber().ToString(),
                City = city?.GetName() ?? "Sin ciudad",  // Verifica que la ciudad exista
                Team = team?.Name ?? "Sin equipo",  // Verifica que el equipo exista
                Age = (DateTime.Now.Year - player.GetBirthDay().Year).ToString()
            };

            // Crear la ventana de detalles
            var playerDetailsWindow = new Window
            {
                Title = "Detalles del jugador",
                Content = playerDetails,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            playerDetailsWindow.ShowDialog();
        }


        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void LoadFilters()
        {
            // Obtener los equipos del controlador
            var teams = _nbaController.GetTeams();

            // Agregar un item vacío para indicar que no hay filtro seleccionado
            TeamFilter.Items.Add(new ComboBoxItem {Content = "Todos", Tag = ""});

            // Agregar los equipos al ComboBox
            foreach (var team in teams)
            {
                TeamFilter.Items.Add(new ComboBoxItem {Content = team.Name, Tag = team.GetIdTeam()});
            }

            // Seleccionar el primer item por defecto
            TeamFilter.SelectedIndex = 0;
            
            var numbers = _nbaController.GetPlayers().Select(player => player.GetNumber().ToString()).Distinct().ToList();
            
            // Agregar un item vacío para indicar que no hay filtro seleccionado
            NumberFilter.Items.Add(new ComboBoxItem {Content = "Todos", Tag = ""});
            
            // Agregar los números al ComboBox
            
            foreach (var number in numbers)
            {
                NumberFilter.Items.Add(new ComboBoxItem {Content = number, Tag = number});
            }
            
            // Seleccionar el primer item por defecto
            NumberFilter.SelectedIndex = 0;
            
            var names = _nbaController.GetPlayers().Select(player => player.Name).Distinct().ToList();
            
            // Agregar un item vacío para indicar que no hay filtro seleccionado
            NameFilter.Items.Add(new ComboBoxItem {Content = "Todos", Tag = ""});
            
            // Agregar los nombres al ComboBox
            
            foreach (var name in names)
            {
                NameFilter.Items.Add(new ComboBoxItem {Content = name, Tag = name});
            }
            
            // Seleccionar el primer item por defecto
            NameFilter.SelectedIndex = 0;
            
            var cities = _nbaController.GetCities().Select(city => city.GetName()).Distinct().ToList();
            
            // Agregar un item vacío para indicar que no hay filtro seleccionado
            
            CityFilter.Items.Add(new ComboBoxItem {Content = "Todos", Tag = ""});
            
            // Agregar las ciudades al ComboBox
            
            foreach (var city in cities)
            {
                CityFilter.Items.Add(new ComboBoxItem {Content = city, Tag = city});
            }
            
            // Seleccionar el primer item por defecto
            CityFilter.SelectedIndex = 0;
        }
        
        private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Obtener los filtros ingresados por el usuario
            var nameFilter = NameFilter.Text.ToLower();
            var selectedTeamName = (TeamFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();  // Obtener el nombre del equipo
            var numberFilter = NumberFilter.Text;
            var selectedCityName = (CityFilter.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower();  // Obtener el nombre de la ciudad

            // Filtrar jugadores
            var filteredPlayers = _nbaController.GetPlayers()
                .Where(player =>
                        (string.IsNullOrEmpty(nameFilter) || player.GetName().ToLower().Contains(nameFilter)) &&
                        (string.IsNullOrEmpty(selectedTeamName) || GetTeamNameById(player.GetIdTeam()).ToLower().Contains(selectedTeamName)) &&  // Filtrar por el nombre del equipo
                        (string.IsNullOrEmpty(numberFilter) || player.GetNumber().ToString() == numberFilter) &&  // Corregir el error de paréntesis
                        (string.IsNullOrEmpty(selectedCityName) || GetCityNameById(player.GetCity()).ToLower().Contains(selectedCityName))  // Filtrar por el nombre de la ciudad
                )
                .ToList();

            // Actualizar las cards con los jugadores filtrados
            UpdatePlayerCards(filteredPlayers);
        }

        // Función auxiliar para obtener el nombre del equipo por su ID
        private string GetTeamNameById(string teamId)
        {
            var team = _nbaController.GetTeams().FirstOrDefault(t => t.GetIdTeam() == teamId);
            return team?.Name ?? "Sin equipo";
        }

        private string GetCityNameById(string cityId)
        {
            var city = _nbaController.GetCities().FirstOrDefault(c => c.GetIdCity() == cityId);
            return city?.GetName() ?? "Sin ciudad";
        }


        private void UpdatePlayerCards(List<Player> players)
        {
            // Obtén el WrapPanel
            var wrapPanel = CardsContainer;
            wrapPanel.Children.Clear(); // Limpia las cards existentes

            var teams = _nbaController.GetTeams();

            foreach (var player in players)
            {
                var teamName = teams.FirstOrDefault(team => team.GetIdTeam() == player.GetIdTeam())?.GetName() ?? "Sin equipo";

                var card = new Cards
                {
                    Width = 280,
                    Margin = new Thickness(5),
                    Heading = $"{player.GetName()} {player.GetLastName()}",
                    Description = $"{player.GetNumber()} - {teamName}",
                    ActionButtonText = "Ver detalles"
                };

                card.ActionClick += (s, e) => ShowPlayerDetails(player);
                wrapPanel.Children.Add(card);
            }
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            NameFilter.Text = "";
            TeamFilter.SelectedIndex = -1;
            NumberFilter.Text = "";
            CityFilter.SelectedIndex = -1;
            LoadPlayerCards();
        }
    }
}