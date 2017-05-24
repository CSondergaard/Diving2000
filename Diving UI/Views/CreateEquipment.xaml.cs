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
using System.Collections.ObjectModel;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for CreateEquipment.xaml
    /// </summary>
    public partial class CreateEquipment : UserControl
    {

        PropertyRepo PropRep = new PropertyRepo();
        EquipmentRepo EqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();

        DataFacade DataFac = new DataFacade();


        public CreateEquipment()
        {
            InitializeComponent();
            FillCategory();
        }

        public void FillCategory()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();

            List<Category> catlist = CatRep.GetAll();

            foreach (Category item in catlist)
            {
                list.Add(item._name);
            }

            CBCategory.ItemsSource = list;

        }

        public void CreateProperty(string catName)
        {
            SpProp.Children.Clear();
            Category cat = CatRep.GetByName(catName);

            foreach (Property item in cat._values)
            {
                if (item._values.Count == 0 ||item._values == null)
                {

                }
                else
                {
                    ComboBox cbox = CreateCombobox(item._name.ToString());
                    foreach (string val in item._values)
                    {
                        cbox.Items.Add(val);
                    }
                    Label lb = new Label();
                    lb.Content = item._name;

                    Button btn = CreateButton(item._name.ToString());
                    StackPanel sp = new StackPanel();
                    sp.Children.Add(lb);
                    sp.Children.Add(cbox);
                    sp.Children.Add(btn);

                    SpProp.Children.Add(sp);
                }
            }
        }

        private Button CreateButton(string name)
        {
            Button btn = new Button();
            btn.Click += btnCreateValue;
            btn.Name = name;
            btn.Content = "Tilføj værdi";
            btn.Width = 100;
            btn.Height = 25;

            return btn;
        }

        private ComboBox CreateCombobox(string name)
        {
            ComboBox cbox = new ComboBox();
            cbox.Width = 200;
            cbox.Height = 25;
            cbox.Name = name;
            return cbox;
        }

        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateProperty(CBCategory.SelectedValue.ToString());
        }

        private void btnCreateValue(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Name.ToString();
            var dialog = new CreateValue();
            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(dialog.ResponseText))
                {
                    PropRep.AddValue(content, dialog.ResponseText);

                    Property prop = PropRep.GetByName(content);
                    DataFac.AddValueToProp(dialog.ResponseText, prop._id);
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde feltet");
                }

            }

            CreateProperty(CBCategory.SelectedValue.ToString());
        }

        private void btnCreateEq_Click(object sender, RoutedEventArgs e)
        {
            Category cat = CatRep.GetByName(CBCategory.SelectedValue.ToString());

            Dictionary<string, string> propList = new Dictionary<string, string>();

            foreach (ComboBox cb in FindVisualChildren<ComboBox>(window))
            {
                if(cb.Name != "CBCategory")
                {
                    propList.Add(cb.Name, cb.SelectedValue.ToString());
                }
            }


            Equipment eq = new Equipment(
                cat._name,
                cat._id,
                Convert.ToDateTime(dtpService.Text),
                propList
                );

            DataFac.AddEquipment(eq);

       
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
