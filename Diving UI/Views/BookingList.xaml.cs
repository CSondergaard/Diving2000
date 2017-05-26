using Logic;
using Logic.Data;
using Logic.Repository;
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
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : UserControl
    {
        EquipmentRepo eqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        DataFacade DataFac = new DataFacade();
        public BookingList()
        {
            InitializeComponent();
            OnBookingListLoad();
        }
        private void OnBookingListLoad()
        {
            SpInv.Children.Clear();
            List<Equipment> eqlist = eqRep.GetAllAviableEquipments(DateTime.Now);
            foreach (Equipment item in eqlist)
            {
                Category Cat = CatRep.GetById(item._catId);
                string url;
                if (!string.IsNullOrWhiteSpace(Cat._thumbnail))
                {
                    url = @"\Resources\CategoryPic\" + Cat._thumbnail;
                }
                else
                {
                    url = @"\Resources\CategoryPic\dykk.png";
                }

                Border br = new Border();
                br.BorderThickness = new Thickness(1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                br.Margin = new Thickness(0, 0, 0, 10);
                SpInv.Children.Add(br);

                Grid DG = new Grid();
                DG.Width = 600;
                DG.MinHeight = 75;
                DG.Background = new SolidColorBrush(Colors.Transparent);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(75);
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = new GridLength(425);
                ColumnDefinition col3 = new ColumnDefinition();
                col3.Width = new GridLength(100);
                DG.ColumnDefinitions.Add(col1);
                DG.ColumnDefinitions.Add(col2);
                DG.ColumnDefinitions.Add(col3);
                br.Child = DG;

                Button Delbtn = new Button();
                Grid.SetRow(Delbtn, 0);
                Grid.SetColumn(Delbtn, 2);
                Delbtn.Width = 60;
                Delbtn.Height = 20;
                Delbtn.Click += btnDel;
                Delbtn.Tag = item._id.ToString();
                Delbtn.Margin = new Thickness(0, 0, 0, 10);
                Delbtn.Foreground = Brushes.White;
                Delbtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9302C"));
                Delbtn.Content = "Slet";
                Delbtn.HorizontalAlignment = HorizontalAlignment.Center;
                Delbtn.VerticalAlignment = VerticalAlignment.Bottom;
                DG.Children.Add(Delbtn);

                Border brimg = new Border();
                brimg.BorderThickness = new Thickness(1);
                brimg.BorderBrush = new SolidColorBrush(Colors.Black);
                brimg.Margin = new Thickness(10, 10, 10, 10);
                Grid.SetColumn(brimg, 0);
                DG.Children.Add(brimg);

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                img.Height = 50;
                img.Width = 50;
                brimg.Child = img;

                StackPanel spcol1 = new StackPanel();
                Grid.SetColumn(spcol1, 1);
                DG.Children.Add(spcol1);

                Label lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Center;
                lb.Content = item._name;
                lb.FontSize = 20;
                Grid.SetColumn(lb, 1);
                spcol1.Children.Add(lb);

                WrapPanel wp = new WrapPanel();
                Grid.SetColumn(wp, 1);
                spcol1.Children.Add(wp);

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
            int id = Convert.ToInt32((sender as Button).Tag);
            DataFac.DeleteEquipmentById(id);
            OnBookingListLoad();
        }
    }
}
