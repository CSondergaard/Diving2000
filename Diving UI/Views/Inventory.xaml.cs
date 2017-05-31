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
using Logic;
using Logic.Repository;
using Logic.Data;
using Diving_UI.Model;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        EquipmentRepo eqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        BookingSearch bookSearch = new BookingSearch();
        DataFacade DataFac = DataFacade.Instance;
        Edit ed = Edit.Instance;

        SearchEquipment searcheq = SearchEquipment.Instance;

        List<Equipment> booklist = new List<Equipment>();


        public Inventory()
        {
            InitializeComponent();
            booklist = bookSearch.GetEquipmentsBookedAtTime(DateTime.Now);

            ShowEquipments(eqRep.GetAllEquipments());


            searcheq.SearchChanged += new SearchEquipment.ChangedSearchEventHandler(Searcheq_SearchChanged);
        }

        private void Searcheq_SearchChanged(List<Equipment> eqlist)
        {
            ShowEquipments(eqlist);
        }

        private void ShowEquipments(List<Equipment> eqlist)
        {
            SpInv.Children.Clear();

            foreach (Equipment item in eqlist)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                br.Margin = new Thickness(0, 0, 0, 10);
                SpInv.Children.Add(br);

                Grid DG = new Grid();
                DG.Width = 600;
                DG.MinHeight = 75;
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

                Canvas cv = new Canvas();
                cv.Width = 618;
                cv.ClipToBounds = true;

                Label lbRentDates = new Label();
                lbRentDates.Width = 100;
                lbRentDates.Height = 25;
                lbRentDates.FontSize = 10;
                lbRentDates.FontFamily = new FontFamily("Arial");
                lbRentDates.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbRentDates.VerticalContentAlignment = VerticalAlignment.Center;
                lbRentDates.Foreground = new SolidColorBrush(Colors.White);
                lbRentDates.LayoutTransform = new RotateTransform(45);
                Canvas.SetTop(lbRentDates, -25);
                Canvas.SetRight(lbRentDates, -25);
                DG.Children.Add(cv);

                Label lbRentDate = new Label();


                if (booklist.Any(x => x._id == item._id))
                {
                    lbRentDates.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9302C"));
                    lbRentDates.Content = "UDLEJET";
                    lbRentDate.Content = "Udlejet indtil: " + bookSearch.GetRentDateForEquipment(item).ToString("dd-MM-yyyy");
                }

                cv.Children.Add(lbRentDates);

                Button Editbtn = new Button();
                Grid.SetRow(Editbtn, 0);
                Grid.SetColumn(Editbtn, 2);
                Editbtn.Width = 60;
                Editbtn.Height = 20;
                Editbtn.Click += btnEdit;
                Editbtn.Tag = item._id.ToString();
                Editbtn.Margin = new Thickness(0, 0, 0, 10);
                Editbtn.Foreground = Brushes.White;
                Editbtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5FD080"));
                Editbtn.Content = "Rediger";
                Editbtn.HorizontalAlignment = HorizontalAlignment.Center;
                Editbtn.VerticalAlignment = VerticalAlignment.Bottom;
                DG.Children.Add(Editbtn);

                Border brimg = new Border();
                brimg.BorderThickness = new Thickness(1);
                brimg.BorderBrush = new SolidColorBrush(Colors.Black);
                brimg.Margin = new Thickness(10, 10, 10, 10);
                Grid.SetColumn(brimg, 0);
                DG.Children.Add(brimg);

                Image img = new Image();

                Category Cat = CatRep.GetById(item._catId);
                string url = "../Resources/18217764_1871114903128407_1044267123_n.png";
                if (Cat._thumbnail != null)
                    if (!string.IsNullOrWhiteSpace(Cat._thumbnail))
                        url = Cat._thumbnail;

                img.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                img.Height = 50;
                img.Width = 50;

                brimg.Child = img;

                StackPanel spcol1 = new StackPanel();
                Grid.SetColumn(spcol1, 1);
                DG.Children.Add(spcol1);

                Label lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Center;
                lb.Content = Cat._name;
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

                wp.Children.Add(lbRentDate);
              
            }
        }

        private void btnEdit(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Tag);
            ed.SetEqId(id);

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\EditEquipment.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
