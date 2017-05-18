using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : UserControl
    {
        public FrontPage()
        {
            InitializeComponent();
        }

        private void btnBookingClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = new Uri(@"\Views\filter\BookingFilter.xaml", UriKind.RelativeOrAbsolute);
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = new Uri(@"\Views\chart\BookingChart.xaml", UriKind.RelativeOrAbsolute);
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\Booking.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnBookingListClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\BookingList.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnInventoryClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = new Uri(@"\Views\filter\InventoryFilter.xaml", UriKind.RelativeOrAbsolute);
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\Inventory.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnCreateEquipmentClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\CreateEquipment.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnCreateCategoryClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\CreateCategory.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnSupportClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\Support.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
