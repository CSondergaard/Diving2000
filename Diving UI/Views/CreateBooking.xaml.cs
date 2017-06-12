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
using System.Windows.Shapes;
using Logic;
using Logic.Repository;
using Diving_UI.Model;
using Logic.Data;
using System.Text.RegularExpressions;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for CreateBooking.xaml
    /// </summary>
    public partial class CreateBooking : UserControl
    {

        CurrentBookingList crbkList = CurrentBookingList.Instance;
        DataFacade datafac = DataFacade.Instance;
        EquipmentRepo eqRepo = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();

        public CreateBooking()
        {
            InitializeComponent();
            SetDate();
            CreateEquipmentList();
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private void SetDate()
        {
           txtStartDate.Text = Convert.ToDateTime(crbkList.GetStartdate()).ToString("MM-dd-yyyy");
           txtEndDate.Text = Convert.ToDateTime(crbkList.GetEnddate()).ToString("MM-dd-yyyy");
        }

        private void CreateEquipmentList()
        {
            spEquip.Children.Clear();

            List<Equipment> eqlist = crbkList.GetCurrentItems();

            Grid topgr = new Grid();
            ColumnDefinition col12 = new ColumnDefinition();
            col12.Width = new GridLength(75);
            ColumnDefinition col22= new ColumnDefinition();
            col22.Width = new GridLength(400);
            ColumnDefinition col32 = new ColumnDefinition();
            col32.Width = new GridLength(75);
            topgr.ColumnDefinitions.Add(col12);
            topgr.ColumnDefinitions.Add(col22);
            topgr.ColumnDefinitions.Add(col32);

            Label lbid = new Label();
            lbid.Content = "ID";
            Grid.SetColumn(lbid, 0);

            Label lbName = new Label();
            lbName.Content = "Navn";
            Grid.SetColumn(lbid, 1);

            spEquip.Children.Add(topgr);

            foreach (Equipment item in eqlist)
            {
                Category Cat = CatRep.GetById(item._catId);

                Border DGbr = new Border();
                DGbr.BorderThickness = new Thickness(0, 1, 0, 0);
                DGbr.BorderBrush = new SolidColorBrush(Colors.Black);
                spEquip.Children.Add(DGbr);

                Grid DG = new Grid();
                Grid.SetRow(DG, 1);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(190);
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = new GridLength(190);
                ColumnDefinition col3 = new ColumnDefinition();
                col3.Width = new GridLength(198);
                DG.ColumnDefinitions.Add(col1);
                DG.ColumnDefinitions.Add(col2);
                DG.ColumnDefinitions.Add(col3);
                DGbr.Child = DG;

                Button Delbtn = new Button();
                Grid.SetRow(Delbtn, 1);
                Grid.SetColumn(Delbtn, 2);
                Delbtn.Width = 60;
                Delbtn.Height = 20;
                Delbtn.Click += btnDel;
                Delbtn.Tag = item._id.ToString();
                Delbtn.Margin = new Thickness(0, 0, 0, 0);
                Delbtn.Foreground = Brushes.White;
                Delbtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9302C"));
                Delbtn.Content = "Slet";
                Delbtn.HorizontalAlignment = HorizontalAlignment.Right;
                Delbtn.VerticalAlignment = VerticalAlignment.Center;
                DG.Children.Add(Delbtn);

                Label lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Center;
                lb.Content = Cat._name;
                Grid.SetColumn(lb, 0);
                DG.Children.Add(lb);

                WrapPanel wp = new WrapPanel();
                Grid.SetColumn(wp, 1);
                DG.Children.Add(wp);

                foreach (KeyValuePair<string, string> val in item._values)
                {
                    Label lb2 = new Label();
                    lb2.VerticalAlignment = VerticalAlignment.Center;
                    lb2.Content = val.Key + ": " + val.Value;
                    wp.Children.Add(lb2);

                }
            }


        }

        private void btnDel(object sender, RoutedEventArgs e)
        {
            Equipment eq = eqRepo.GetById(Convert.ToInt32((sender as Button).Tag));
            crbkList.RemoveEquipmentFromList(eq);
            CreateEquipmentList();
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                List<Equipment> eqlist = crbkList.GetCurrentItems();
                datafac.AddBooking(new Logic.Booking(
                    eqlist,
                    Convert.ToDateTime(crbkList.GetStartdate()),
                    Convert.ToDateTime(crbkList.GetEnddate()),
                    txtPhone.Text,
                    false
                    ));


                MessageBox.Show("Booking er nu oprettet");

                Edit ed = Edit.Instance;
                ed.runAlarm();

                (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                txtPhoneError.Content = "Du mangler at udfylde et nr";
            }


                

        }
    }
}
