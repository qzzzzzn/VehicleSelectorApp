using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VehicleSelectorApp;

namespace VehicleSelectorApp
{
    public partial class MainWindow : Window
    {
        private DataService _dataService;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _dataService = new DataService();
        }

        private void SearchCarsButton_Click(object sender, RoutedEventArgs e)
        {
    
            string brand = BrandTextBox.Text;
            string model = ModelTextBox.Text;
            int year = int.TryParse(YearTextBox.Text, out int yearValue) ? yearValue : 0;
            decimal minPrice = decimal.TryParse(MinPriceTextBox.Text, out decimal minPriceValue) ? minPriceValue : 0;
            decimal maxPrice = decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPriceValue) ? maxPriceValue : decimal.MaxValue;
            string color = ColorTextBox.Text;
            int mileage = int.TryParse(MileageTextBox.Text, out int mileageValue) ? mileageValue : 0;
            string condition = ConditionTextBox.Text;
            string type = TypeTextBox.Text;

            List<Car> cars = _dataService.SearchCars(brand, model, year, minPrice, maxPrice, color, mileage, condition, type);

            CarsListView.ItemsSource = cars;
        }
        private void CarsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = CarsListView.SelectedItem as Car;
            if (selectedItem != null)
            {
                CarNameTextBlock.Text = selectedItem.brand + " " + selectedItem.model;
                CarDescriptionTextBlock.Text = _dataService.GetCarDescription(selectedItem.id);
                CarPriceTextBlock.Text = selectedItem.price.ToString("C");
                CarImage.Source = (ImageSource)new ByteArrayToImageConverter().Convert(selectedItem.Image.image1, typeof(ImageSource), null, CultureInfo.InvariantCulture);
                CarDetailsGrid.Visibility = Visibility.Visible;
                CarsListView.Visibility = Visibility.Collapsed;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CarDetailsGrid.Visibility = Visibility.Collapsed;
            CarsListView.Visibility = Visibility.Visible;
        }

        private void SearchPartsButton_Click(object sender, RoutedEventArgs e)
        {
            string name = PartNameTextBox.Text;
            string description = DescriptionTextBox.Text;
            decimal minPrice = decimal.TryParse(MinPriceTextBox.Text, out decimal minPriceValue) ? minPriceValue : 0;
            decimal maxPrice = decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPriceValue) ? maxPriceValue : decimal.MaxValue;


            List<Part> Part = _dataService.SearchParts(name, description, minPrice, maxPrice);
            PartsListView.ItemsSource = Part;
        }


    }
}
