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
using Logic;

namespace Diving_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataFacade fac = new DataFacade();
        AlarmLogic alarm = new AlarmLogic();


        public MainWindow()
        {
            InitializeComponent();
            fac.GetAll();
            CheckForAlarms();

       

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

        private void btnAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (cvAlarm.Opacity == 0)
            {
                cvAlarm.Opacity = 1;
                cvAlarm.Margin = new Thickness(0, 45, 32, 0);
            }
            else
            {
                cvAlarm.Opacity = 0;
                cvAlarm.Margin = new Thickness(0, -250, 32, 0);
            }
        }

        private void CheckForAlarms()
        {
            int nr = alarm.GetAlarms();
            if (nr > 0)
            {
                lbalarmNr.Content = nr;
                lbalarmNr.Opacity = 1;
                imgAlarm.Source = new BitmapImage(new Uri(@"/Resources/Icons/bellwithalarm.png", UriKind.RelativeOrAbsolute));

                if (nr < 10)
                {
                    lbalarmNr.FontSize = 10;
                    lbalarmNr.Margin = new Thickness(0, 4, 36, 25);
                }
                else
                {
                    lbalarmNr.Content = "9+";
                    lbalarmNr.FontSize = 8;
                    lbalarmNr.Margin = new Thickness(0, 6, 33, 25);

                }

                ShowAlarmItems();
            }
            else
            {
                lbalarmNr.Content = "";
                lbalarmNr.Opacity = 0;
                imgAlarm.Source = new BitmapImage(new Uri(@"/Resources/Icons/bellwithout.png", UriKind.RelativeOrAbsolute));
            }


        }

        private void ShowAlarmItems()
        {
            List<Category> catList = alarm.AlarmForCategory();

            foreach (Category item in catList)
            {
                StackPanel sp = new StackPanel();
                Label lbError = new Label();
                Label lbCatName = new Label();

                lbError.Content = "Mangel på Udstyr";
                lbCatName.Content = "Fejl på: " + item._name;

                sp.Children.Add(lbError);
                sp.Children.Add(lbCatName);

                spAlarm.Children.Add(sp);
            }



        }
    }

}
