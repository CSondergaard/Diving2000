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
using System.Windows.Controls.Primitives;

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
            UpdateCount();

            bklist.ItemAdded += new CurrentBookingList.ItemAddedToListEventHandler(UpdateCount);


        }

        private void UpdateCount()
        {
            int count = bklist.CountBookingList();
            if ( count == 1)
            {
                btnAddBooking.Content = bklist.CountBookingList() + " Vare tilføjet";
            }
            else if (count == 0)
            {
                btnAddBooking.Content = "Ingen vare er tilføjet";
            }
            else
            {
                btnAddBooking.Content = bklist.CountBookingList() + " Varer tilføjet";
            }
            
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (bklist.GetEnddate() == null || bklist.GetStartdate() == null)
            {
                MessageBox.Show("Du mangler at udfylde dato");
            }
            else
            {
                (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\CreateBooking.xaml", UriKind.RelativeOrAbsolute);

            }
        }
    }
}
