using Diving_UI.Views;
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
using Logic.Data;
using Diving_UI.Model;
using System.Text.RegularExpressions;

namespace Diving_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataFacade fac = new DataFacade();


        public MainWindow()
        {
            InitializeComponent();
            fac.GetAll();
        }

        private void btnHomeClick(object sender, RoutedEventArgs e)
        {
            SearchTermTextBox.Text = "";

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void GetAllDate()
        {
            fac.GetAll();
        }

        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            SearchBookingList se = SearchBookingList.Instance;

            se.SearchInBooking(SearchTermTextBox.Text);

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\BookingList.xaml", UriKind.RelativeOrAbsolute);

        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void SearchTermTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

    }

}
