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
using Diving_UI.Model;

namespace Diving_UI.Views.chart
{
    /// <summary>
    /// Interaction logic for BookingChart.xaml
    /// </summary>
    public partial class BookingChart : UserControl
    {

        CurrentBookingList bklist = CurrentBookingList.Instance;

        public BookingChart()
        {
            InitializeComponent();

            bklist.ItemAdded += new CurrentBookingList.ItemAddedToListEventHandler(UpdateCount);

        }

        private void UpdateCount()
        {
            lbCount.Content = bklist.CountBookingList();
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\CreateBooking.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
