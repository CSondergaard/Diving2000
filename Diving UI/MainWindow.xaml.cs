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
using System.Timers;

namespace Diving_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataFacade fac = new DataFacade();
        AlarmLogic alarm = new AlarmLogic();
        CategoryRepo catRep = new CategoryRepo();
        Edit ed = Edit.Instance;


        public MainWindow()
        {
            InitializeComponent();


            fac.GetAll();
            CheckForAlarms();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(1,0,0);
            dispatcherTimer.Start();

            ed.RunAlarm += new Edit.AlarmUpdateEventHandler(CheckForAlarms);


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
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
            spAlarm.Children.Clear();
            WriteAlarmCategory();
            WriteAlarmBooking();
            WriteAlarmBookingPickup();
            WriteAlarmEqService();
        }

        private void WriteAlarmCategory()
        {
            List<Category> catList = alarm.AlarmForCategory();

            foreach (Category item in catList)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(0, 0, 0, 1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(20, 5, 0, 5);
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 375;
                sp.Margin = new Thickness(20, 0, 0, 0);
                Label lbError = new Label();
                lbError.Width = 150;
                Label lbCatName = new Label();
                lbCatName.Width = 150;

                lbError.Content = "Mangel på Udstyr";
                lbCatName.Content = "Fejl på: " + item._name;

                sp.Children.Add(lbError);
                sp.Children.Add(lbCatName);
                br.Child = sp;
                spAlarm.Children.Add(br);
            }

        }

        private void WriteAlarmBooking()
        {
            List<Logic.Booking> bkList = alarm.AlarmForBooking();

            foreach (Logic.Booking item in bkList)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(0, 0, 0, 1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 375;
                sp.Margin = new Thickness(20, 5, 0, 5);
                Label lbError = new Label();
                lbError.Width = 150;
                Label lbCatName = new Label();
                lbCatName.Width = 150;

                Button btn = new Button();
                btn.Width = 50;
                btn.Height = 25;
                btn.Click += btnGoToBooking;
                btn.Content = "Gå til";
                btn.Tag = item._phone;


                lbError.Content = "Booking ikke afleveret";
                lbCatName.Content = "Fejl på: " + item._phone;

                sp.Children.Add(lbError);
                sp.Children.Add(lbCatName);
                sp.Children.Add(btn);
                br.Child = sp;
                spAlarm.Children.Add(br);

            }
        }

        private void WriteAlarmBookingPickup()
        {
            List<Logic.Booking> listBookingPickup = alarm.AlarmForBookingPickup();

            foreach (Logic.Booking item in listBookingPickup)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(0, 0, 0, 1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 375;
                sp.Margin = new Thickness(20, 5, 0, 5);
                Label lbError = new Label();
                lbError.Width = 150;
                Label lbCatName = new Label();
                lbCatName.Width = 150;

                Button btn = new Button();
                btn.Width = 50;
                btn.Height = 25;
                btn.Click += btnGoToBooking;
                btn.Content = "Gå til";
                btn.Tag = item._phone;


                lbError.Content = "Afhentning mangler";
                lbCatName.Content = "Fejl på: " + item._phone;

                sp.Children.Add(lbError);
                sp.Children.Add(lbCatName);
                sp.Children.Add(btn);
                br.Child = sp;
                spAlarm.Children.Add(br);
            }
        }

        private void WriteAlarmEqService()
        {
            List<Equipment> eqServiceList = alarm.AlarmForEquipmentService();

            foreach (Equipment item in eqServiceList)
            {
                Category cat = catRep.GetById(item._catId);

                Border br = new Border();
                br.BorderThickness = new Thickness(0, 0, 0, 1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 375;
                sp.Margin = new Thickness(20, 5, 0, 5);
                Label lbError = new Label();
                lbError.Width = 150;
                Label lbCatName = new Label();
                lbCatName.Width = 150;

                Button btn = new Button();
                btn.Width = 50;
                btn.Height = 25;
                btn.Click += btnGoToEquipment;
                btn.Content = "Gå til";
                btn.Tag = item._id;


                lbError.Content = "Service på udstyr";
                lbCatName.Content = "Kategori: " + cat._name;

                sp.Children.Add(lbError);
                sp.Children.Add(lbCatName);
                sp.Children.Add(btn);
                br.Child = sp;
                spAlarm.Children.Add(br);
            }
        }

        public void btnGoToBooking(object sender, RoutedEventArgs e)
        {
            SearchBookingList se = SearchBookingList.Instance;

            string phone = (sender as Button).Tag.ToString();

            se.SearchInBooking(phone);
            SearchTermTextBox.Text = phone; 

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\BookingList.xaml", UriKind.RelativeOrAbsolute);
        }

        public void btnGoToEquipment(object sender, RoutedEventArgs e)
        {

            int id = Convert.ToInt32((sender as Button).Tag);
            ed.SetEqId(id);

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\EditEquipment.xaml", UriKind.RelativeOrAbsolute);
        }
    }

}
