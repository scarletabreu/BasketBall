using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Basket.Visual
{
    public partial class PlayerDetails : UserControl
    {
        public PlayerDetails()
        {
            InitializeComponent();
        }
        
        public string Name
        {
            get { return FullNameTextBlockHeader.Text; }
            set { FullNameTextBlockHeader.Text = value; }
        }

        public string FullName
        {
            get { return FullNameTextBlockInfo.Text; }
            set { FullNameTextBlockInfo.Text = value; }
        }
        
        public string FullNameGeneral
        {
            get { return FullNameTextBlockGeneral.Text; }
            set { FullNameTextBlockGeneral.Text = value; }
        }
        
        public string BirthDay
        {
            get { return DateTextBlock.Text; }
            set { DateTextBlock.Text = value; }
        }
        
        public string Number
        {
            get { return NumberTextBlock.Text; }
            set { NumberTextBlock.Text = value; }
        }
        
        public string City
        {
            get { return CityTextBlock.Text; }
            set { CityTextBlock.Text = value; }
        }
        
        public string Team
        {
            get { return TeamTextBlock.Text; }
            set { TeamTextBlock.Text = value; }
        }

        public string Age
        {
            get { return AgeTextBlockGeneral.Text; }
            set { AgeTextBlockGeneral.Text = value; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = (Panel)Parent;
            parent.Children.Remove(this);
        }
    }
}