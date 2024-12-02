using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Basket.Visual;

public partial class GameDetails
{
    public GameDetails()
    {
        InitializeComponent();
    }
    
    public string Game
    {
        get => GameInfo.Text;
        set => GameInfo.Text = value;
    }
    
    public string Description
    {
        get => descriptionInfo.Text;
        set => descriptionInfo.Text = value;
    }
    
    public string Header
    {
        get => GameHeader.Text;
        init => GameHeader.Text = value;
    }

    public string Initials
    {
        get => GameInitials.Text;
        set => GameInitials.Text = value;
    }

    public string LocalTeam
    {
        get => LocalTeamBlock.Text;
        set => LocalTeamBlock.Text = value;
    }
    
    public string VisitorTeam
    {
        get => VisitorTextBlock.Text;
        set => VisitorTextBlock.Text = value;
    }

    public string LocalTeamInfo
    {
        get => localTeamInfo.Text;
        set => localTeamInfo.Text = value;
    }
    
    public string VisitorTeamInfo
    {
        get => VisitedTeamInfo.Text;
        set => VisitedTeamInfo.Text = value;
    }
    
    public string Date
    {
        get => dateInfo.Text;
        set => dateInfo.Text = value;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement this method
        // Close the user control
    }

    private async void ShowProcedure_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Retrieve the full result of the stored procedure
            var result = await ExecuteStoredProcedureAsync();

            // Display the full result in a scrollable popup window
            var resultPopup = new Window
            {
                Title = "Stored Procedure Result",
                Width = 837,
                Height = 525,
                Content = new ScrollViewer
                {
                    Content = new TextBlock
                    {
                        Text = result,
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                        Margin = new Thickness(10)
                    }
                },
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            resultPopup.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error executing stored procedure: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    private async Task<string> ExecuteStoredProcedureAsync()
    {
        var formattedOutput = new StringBuilder();

        // Ensure your connection string is correctly retrieved
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
        }

        await using var connection = new SqlConnection(connectionString);

        // Capture PRINT statements via InfoMessage event
        connection.InfoMessage += (sender, args) =>
        {
            formattedOutput.AppendLine(args.Message); // Append PRINT outputs
        };

        await connection.OpenAsync();

        await using var command = new SqlCommand("DetallesJuego", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        // Add the necessary parameter
        command.Parameters.Add(new SqlParameter("@CodJuego", Header));

        await using var reader = await command.ExecuteReaderAsync();

        do
        {
            // Process each result set
            bool isHeaderWritten = false;
            List<int> columnWidths = new(); // To store the width of each column

            while (await reader.ReadAsync())
            {
                if (!isHeaderWritten)
                {
                    // Calculate column widths based on header and data
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var header = reader.GetName(i);
                        columnWidths.Add(Math.Max(header.Length, header == "Jugador" ? 23 : 15)); // Ensure minimum width of 15
                    }

                    // Write headers
                    var headers = new List<string>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        headers.Add(reader.GetName(i).PadRight(columnWidths[i]));
                    }
                    formattedOutput.AppendLine(string.Join(" ", headers));

                    // Write separator line
                    var separator = string.Join(" ", columnWidths.Select(w => new string('-', w)));
                    formattedOutput.AppendLine(separator);

                    isHeaderWritten = true;
                }

                // Write row data
                var row = new List<string?>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader[i].ToString()?.PadRight(columnWidths[i]));
                }
                formattedOutput.AppendLine(string.Join(" ", row));
            }

            if (isHeaderWritten)
            {
                formattedOutput.AppendLine(); // Add a blank line after rows for clarity
            }
        } while (await reader.NextResultAsync()); // Move to the next result set, if any

        return formattedOutput.ToString();
    }




    
}