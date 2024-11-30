using System.Windows;
using System.Windows.Controls;

namespace Basket.Visual;

public partial class Cards : UserControl
{
    public Cards()
    {
        InitializeComponent();
    }

    // Propiedades para el encabezado
    public string Heading
    {
        get { return HeadingText.Text; }
        set { HeadingText.Text = value; }
    }

    // Propiedades para la descripción
    public string Description
    {
        get { return DescriptionText.Text; }
        set { DescriptionText.Text = value; }
    }

    // Propiedades para el texto del botón
    public string? ActionButtonText
    {
        get { return ActionButton.Content.ToString(); }
        set { ActionButton.Content = value; }
    }

    public string ListHeading
    {
        get { return ListHeadingText.Text; }
        set { ListHeadingText.Text = value; }
    }

    public string Subheading
    {
        get { return SubheadingText.Text; }
        set { SubheadingText.Text = value; }
    }

    // Evento para el botón de acción
    public event RoutedEventHandler ActionClick
    {
        add { ActionButton.Click += value; }
        remove { ActionButton.Click -= value; }
    }
}