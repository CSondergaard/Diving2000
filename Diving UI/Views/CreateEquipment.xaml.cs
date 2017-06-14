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
using Diving_UI.Model;

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

        DataFacade DataFac = DataFacade.Instance;

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

            if (cat._service == false)
            {
                dtpService.Text = "";
                dtpService.IsReadOnly = true;
            }
            else
            {
                dtpService.IsReadOnly = false;
            }

            if (cat._values != null)
            {
                foreach (Property item in cat._values)
                {

                    Grid gr = new Grid();
                    ColumnDefinition col1 = new ColumnDefinition();
                    col1.Width = new GridLength(100);
                    ColumnDefinition col2 = new ColumnDefinition();
                    col2.Width = new GridLength(250);
                    ColumnDefinition col3 = new ColumnDefinition();
                    col3.Width = new GridLength(100);
                    RowDefinition row1 = new RowDefinition();
                    gr.ColumnDefinitions.Add(col1);
                    gr.ColumnDefinitions.Add(col2);
                    gr.ColumnDefinitions.Add(col3);
                    gr.Height = 40;

                    Label lb = new Label();
                    lb.Content = item._name;
                    lb.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(lb, 0);
                    gr.Children.Add(lb);

                    if (item._values == null || item._values.Count == 0)
                    {
                        TextBox tbox = CreateTextbox(item._name.ToString());
                        Button btn = CreateButton(item._name.ToString());

                        Grid.SetColumn(tbox, 1);
                        Grid.SetColumn(btn, 2);

                        gr.Children.Add(tbox);
                        gr.Children.Add(btn);

                    }
                    else
                    {
                        ComboBox cbox = CreateCombobox(item._name.ToString());
                        foreach (string val in item._values)
                        {
                            cbox.Items.Add(val);
                        }
                        Button btn = CreateButton(item._name.ToString());

                        Grid.SetColumn(cbox, 1);
                        Grid.SetColumn(btn, 2);

                        gr.Children.Add(cbox);
                        gr.Children.Add(btn);
                    }


                    SpProp.Children.Add(gr);
                }
            }



        }

        private Button CreateButton(string name)
        {
            Button btn = new Button();
            btn.Click += btnCreateValue;
            btn.Tag = name;
            btn.Content = "Ny værdi";
            btn.Width = 75;
            btn.Height = 25;

            return btn;
        }

        private ComboBox CreateCombobox(string name)
        {
            ComboBox cbox = new ComboBox();
            cbox.Width = 250;
            cbox.Height = 25;
            cbox.Tag = name;
            return cbox;
        }

        private TextBox CreateTextbox(string name)
        {
            TextBox tbox = new TextBox();
            tbox.Width = 250;
            tbox.Height = 25;
            tbox.Tag = name;
            return tbox;
        }

        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbError.Content = "";
            CreateProperty(CBCategory.SelectedValue.ToString());
        }

        private void btnCreateValue(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Tag.ToString();
            var dialog = new CreateValue();
            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(dialog.ResponseText))
                {
                    Property prop = PropRep.GetByName(content);
                    DataFac.AddValueToProp(dialog.ResponseText, prop._id);
                    KeepSelectedValues();
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde feltet");
                }

            }
        }   

        private void KeepSelectedValues()
        {

            Dictionary<string, string> propList = FillOutProperty();

            CreateProperty(CBCategory.SelectedValue.ToString());

            foreach (KeyValuePair<string, string> item in propList)
            {
                foreach (ComboBox cb in FindVisualChildren<ComboBox>(window))
                {
                    if (cb.Name != "CBCategory" && cb.Tag.ToString() == item.Key)
                    {
                        cb.SelectedValue = item.Value.ToString();
                    }
                }

                foreach (TextBox tb in FindVisualChildren<TextBox>(window))
                {
                    if (tb.Name != "PART_TextBox" && tb.Tag.ToString() == item.Key)
                    {
                        tb.Text = item.Value.ToString();
                    }
                }
            }
        }

        private void btnCreateEq_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;

            bool missing = false;

            Dictionary<string, string> propList = FillOutProperty();

            if (propList.Count == 0)
                missing = true;

            foreach (KeyValuePair<string, string> item in propList)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    missing = true;
                }
            }

            if (CBCategory.SelectedValue == null)
            {
                lbError.Content = "Du skal vælge en kategori";
            }
            else if (dtpService.IsReadOnly == false && !DateTime.TryParse(dtpService.Text, out date))
            {
                lbError.Content = "Du skal udfylde en dato for service";
            }
            else if (dtpService.IsReadOnly == false && Convert.ToDateTime(dtpService.Text).Ticks < DateTime.Now.Ticks)
            {
                lbError.Content = "Sæt en korrekt service";
            }
            else if (missing)
            {
                lbError.Content = "Du mangler at udfylde en værdi";
            }
            else
            {
                Category cat = CatRep.GetByName(CBCategory.SelectedValue.ToString());

                DateTime? dateend;

                if (string.IsNullOrWhiteSpace(dtpService.Text))
                    dateend = null;
                else
                    dateend = Convert.ToDateTime(dtpService.Text);

                Equipment eq = new Equipment(
                    cat._id,
                    dateend,
                    propList
                    );

                DataFac.AddEquipment(eq);

                MessageBox.Show("Udstyret: " + cat._name + " er nu oprettet");

                Edit ed = Edit.Instance;
                ed.runAlarm();

                (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);


            }

        }

        private Dictionary<string, string> FillOutProperty()
        {
            Dictionary<string, string> propList = new Dictionary<string, string>();

            foreach (ComboBox cb in FindVisualChildren<ComboBox>(window))
            {
                if (cb.Name != "CBCategory")
                {
                    if (cb.SelectedValue != null)
                        propList.Add(cb.Tag.ToString(), cb.SelectedValue.ToString());
                    else
                        propList.Add(cb.Tag.ToString(), "");
                }
            }

            foreach (TextBox tb in FindVisualChildren<TextBox>(window))
            {
                if (tb.Name != "PART_TextBox")
                {
                    propList.Add(tb.Tag.ToString(), tb.Text);
                }
            }

            return propList;
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
