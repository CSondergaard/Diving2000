using Diving_UI.Model;
using Logic;
using Logic.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Diving_UI.Views.filter
{
    /// <summary>
    /// Interaction logic for BookingListChart.xaml
    /// </summary>
    public partial class BookingListChart : UserControl
    {

        PropertyRepo PropRep = new PropertyRepo();
        EquipmentRepo EqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        EquipmentSearch EqSearch = new EquipmentSearch();
        BookingSearch bookingSearch = new BookingSearch();

        SearchEquipment searchequip = SearchEquipment.Instance;

        List<Equipment> eqlist = new List<Equipment>();

        public BookingListChart()
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

                    StackPanel sp = new StackPanel();
                    sp.Children.Add(tbox);
                    sp.Children.Add(lb);
                    spProp.Children.Add(sp);
                }
                else
                {
                    ComboBox cb = CreateCombobox(item._name);
                    foreach (string val in item._values)
                    {
                        cb.Items.Add(val);
                    }
                    Label lb = new Label();
                    lb.Content = item._name;

                    StackPanel sp = new StackPanel();
                    sp.Children.Add(cb);
                    sp.Children.Add(lb);
                    spProp.Children.Add(sp);
                }
            }
            FilterListWithCategory();
            FireSearchEvent();

        }


        private ComboBox CreateCombobox(string name)
        {
            ComboBox cbox = new ComboBox();
            cbox.Width = 200;
            cbox.Height = 25;
            cbox.Name = name;
            return cbox;
        }

        private TextBox CreateTextbox(string name)
        {
            TextBox tbox = new TextBox();
            tbox.Width = 200;
            tbox.Height = 25;
            tbox.Name = name;
            return tbox;
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPropertyForCategory(cbCategory.SelectedValue.ToString());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> propList = new Dictionary<string, string>();

            foreach (ComboBox cb in FindVisualChildren<ComboBox>(spProp))
            {
                if(cb.SelectedValue != null)
                    if (!string.IsNullOrWhiteSpace(cb.SelectedValue.ToString()))
                        propList.Add(cb.Name, cb.SelectedValue.ToString());
            }

            foreach (TextBox tb in FindVisualChildren<TextBox>(spProp))
            {
                if (!string.IsNullOrWhiteSpace(tb.Text))
                    propList.Add(tb.Name, tb.Text);
            }

            FilterListWithCategory();
            eqlist = EqSearch.SearchEquipment(propList, eqlist);

            FireSearchEvent();
        }

        private void FilterListWithCategory()
        {
            eqlist = EqRep.GetAllEquipments();
            Category cat = CatRep.GetByName(cbCategory.SelectedValue.ToString());
            eqlist = EqSearch.GetEquipmentsFromCategory(cat._id, eqlist);
        }

        private void FireSearchEvent()
        {
            searchequip.Search(eqlist);
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
    }

}
