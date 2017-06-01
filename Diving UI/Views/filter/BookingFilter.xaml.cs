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
using Diving_UI.Model;
using System.Collections.ObjectModel;

namespace Diving_UI.Views.filter
{
    /// <summary>
    /// Interaction logic for BookingFilter.xaml
    /// </summary>
    public partial class BookingFilter : UserControl
    {

        PropertyRepo PropRep = new PropertyRepo();
        EquipmentRepo EqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        EquipmentSearch EqSearch = new EquipmentSearch();
        BookingSearch bookingSearch = new BookingSearch();
        CurrentBookingList crbooklist = CurrentBookingList.Instance;

        SearchBooking searchBooking = SearchBooking.Instance;

        List<Equipment> eqlist = new List<Equipment>();
        public BookingFilter()
        {
            InitializeComponent();
            LoadCategory();
            eqlist = EqRep.GetAllEquipments();
        }

        public void LoadCategory()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();

            List<Category> catlist = CatRep.GetAll();

            foreach (Category item in catlist)
            {
                list.Add(item._name);
            }

            cbCategory.ItemsSource = list;

        }

        public void LoadPropertyForCategory(string name)
        {
            spProp.Children.Clear();
            Category cat = CatRep.GetByName(name);
            foreach (Property item in cat._values)
            {
                if (item._values.Count == 0 || item._values == null)
                {
                    TextBox tbox = CreateTextbox(item._name);
                    Label lb = new Label();
                    lb.Content = item._name;
                    lb.Foreground = new SolidColorBrush(Colors.White);

                    spProp.Children.Add(lb);
                    spProp.Children.Add(tbox);
                }
                else
                {
                    ComboBox cb = CreateCombobox(item._name);
                    foreach (string val in item._values)
                    {
                        cb.Items.Add(val);
                    }
                    Label lb = new Label();
                    lb.Foreground = new SolidColorBrush(Colors.White);
                    lb.Content = item._name;

                    spProp.Children.Add(lb);
                    spProp.Children.Add(cb);
                }
            }
            FilterListWithCategory();

        }


        private ComboBox CreateCombobox(string name)
        {
            ComboBox cbox = new ComboBox();
            cbox.Width = 240;
            cbox.Height = 25;
            cbox.Tag = name;
            cbox.SelectionChanged += btnSearch_Click;
            return cbox;
        }

        private TextBox CreateTextbox(string name)
        {
            TextBox tbox = new TextBox();
            tbox.Width = 240;
            tbox.Height = 25;
            tbox.Tag = name;
            tbox.SelectionChanged += btnSearch_Click;
            return tbox;
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
            {
                lbDateError.Content = "Vælg start og slut dato!";
            }
            else
            {
                lbDateError.Content = "";
                LoadPropertyForCategory(cbCategory.SelectedValue.ToString());
                LoadEquipments();
            }

        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadEquipments();
        }

        private void LoadEquipments()
        {
            if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
            {
                lbDateError.Content = "Vælg start og slut dato!";
            }
            else if (cbCategory.SelectedValue == null)
            {
                lbDateError.Content = "Vælg en kategori";
            }
            else if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
            {
                lbDateError.Content = "Start dato skal være før slut dato!";
            }
            else
            {
                lbDateError.Content = "";
                Dictionary<string, string> propList = new Dictionary<string, string>();

                foreach (ComboBox cb in FindVisualChildren<ComboBox>(spProp))
                {
                    if (cb.SelectedValue != null)
                        if (!string.IsNullOrWhiteSpace(cb.SelectedValue.ToString()))
                            propList.Add(cb.Tag.ToString(), cb.SelectedValue.ToString());
                }

                foreach (TextBox tb in FindVisualChildren<TextBox>(spProp))
                {
                    if (!string.IsNullOrWhiteSpace(tb.Text))
                        propList.Add(tb.Tag.ToString(), tb.Text);
                }

                FilterListWithCategory();
                if (dpStartDate != null && dpEndDate != null && dpStartDate.Text != "Select a date")
                    eqlist = bookingSearch.GetEquipmentsNotRentedBetweenDates(Convert.ToDateTime(dpStartDate.Text), Convert.ToDateTime(dpEndDate.Text), eqlist);

                eqlist = EqSearch.SearchEquipment(propList, eqlist);

                FireSearchEvent();

                crbooklist.SetDates(Convert.ToDateTime(dpStartDate.Text), Convert.ToDateTime(dpEndDate.Text));
            }
        }

        private void FilterListWithCategory()
        {
            eqlist = EqRep.GetAllEquipments();
            Category cat = CatRep.GetByName(cbCategory.SelectedValue.ToString());
            eqlist = EqRep.GetEquipmentsFromCategory(cat._id, eqlist);
        }

        private void FireSearchEvent()
        {
            searchBooking.Search(eqlist);
        }


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
                lbDateError.Content = "Vælg start og slut dato!";

            else if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
                lbDateError.Content = "Start dato skal være før slut dato!";

            else
                lbDateError.Content = "";


        }
    }
}
