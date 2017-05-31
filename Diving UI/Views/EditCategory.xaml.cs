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
using Logic.Repository;
using Logic.Data;
using Logic;
using Diving_UI.Model;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : UserControl
    {
        PropertyRepo PropRep = new PropertyRepo();
        DataFacade DataFac = DataFacade.Instance;
        CategoryRepo CatRep = new CategoryRepo();

        List<Property> PropForCatList = new List<Property>();
        Category cat;

        public EditCategory()
        {
            InitializeComponent();
            FillOutProperty();
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

            cbName.ItemsSource = list;

        }

        public void FillOutData()
        {
            cat = CatRep.GetByName(cbName.SelectedValue.ToString());
            PropForCatList = cat._values;
            txtName.Text = cat._name;
            txtAlarm.Text = cat._alarm.ToString();
            lbUploadName.Content = cat._thumbnail;
            if (cat._service)
                RBYes.IsChecked = true;
            else
                RBNo.IsChecked = true;

            WriteValueOut();
        }



        private void FillOutProperty()
        {
            ObservableCollection<string> cblist = new ObservableCollection<string>();
            cblist.Clear();
            CBDefinition.Items.Clear();

            List<Property> Proplist = PropRep.GetAllProperty();

            foreach (Property item in Proplist)
            {
                CBDefinition.Items.Add(item._name);
            }

        }

        private void btnDefinition_Click(object sender, RoutedEventArgs e)
        {
            Property prop = PropRep.GetByName(CBDefinition.Text);

            if (CBDefinition.SelectedValue != null)
            {
                if (PropForCatList.Any(x => x._name == prop._name))
                {

                }
                else
                {
                    PropForCatList.Add(prop);
                }

                WriteValueOut();
            }

            

        }

        private void WriteValueOut()
        {

            spProp.Children.Clear();
            foreach (Property item in PropForCatList)
            {
                Grid DG = new Grid();
                DG.Width = 125;
                DG.Height = 25;
                DG.Background = new SolidColorBrush(Colors.Transparent);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(75);
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = new GridLength(50);
                DG.ColumnDefinitions.Add(col1);
                DG.ColumnDefinitions.Add(col2);

                Label lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Center;
                lb.Content = item._name;
                lb.FontSize = 12;
                Grid.SetColumn(lb, 0);
                DG.Children.Add(lb);

                Button Deletebtn = new Button();
                Grid.SetColumn(Deletebtn, 1);
                Deletebtn.Width = 50;
                Deletebtn.Height = 20;
                Deletebtn.Click += btnDeleteProperty;
                Deletebtn.Tag = item._name.ToString();
                Deletebtn.BorderThickness = new Thickness(0, 0, 0, 0);
                Deletebtn.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9302C"));
                Deletebtn.Background = new SolidColorBrush(Colors.Transparent);
                Deletebtn.Content = "Slet";
                Deletebtn.VerticalAlignment = VerticalAlignment.Center;
                DG.Children.Add(Deletebtn);

                spProp.Children.Add(DG);

            }

        }

        private void btnDeleteProperty(object sender, RoutedEventArgs e)
        {

            Property prop = PropRep.GetByName((sender as Button).Tag.ToString());
            PropForCatList.Remove(prop);
            WriteValueOut();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool Service = false;
            string thumb;
            int alarm;

            if (RBYes.IsChecked == true)
            {
                Service = true;
            }
            if (string.IsNullOrWhiteSpace(lbUploadName.Content.ToString()))
            {
                thumb = "";
            }
            else
            {
                thumb = lbUploadName.Content.ToString();
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lbError.Content = "Udfyld venligst et navn";
            }
            else if (string.IsNullOrWhiteSpace(txtAlarm.Text) || !Int32.TryParse(txtAlarm.Text, out alarm))
            {
                lbError.Content = "Udfyld venligst et antal for alarm";
            }
            else if (PropForCatList.Count == 0)
            {
                lbError.Content = "Du har ikke tilføjet værdier";
            }
            else
            {

                DataFac.EditCategory(new Category(
                    cat._id,
                    txtName.Text,
                    PropForCatList,
                    thumb,
                    Service,
                    Convert.ToInt32(txtAlarm.Text)
                    ));

                MessageBox.Show("kategorien " + txtName.Text + " er nu gemt");

                (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.Forms.MessageBox.Show("Er du sikker på du vil slette denne Kategori? Dette vil også slette alle items under denne kategori.", "Bekræftelse",
                System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question,
                System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                DataFac.DeleteCategory(cat._id);

                (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
                (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);
            }
        }
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.DefaultExt = ".jpg"; // Required file extension 
            fileDialog.Filter = "Billeder (.jpg)|*.jpg|.png|*.png"; // Optional file extensions
            fileDialog.Multiselect = false;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbUploadName.Content = fileDialog.FileName;
            }

        }

        private void btnCreateNewDefinition_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateValue();
            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(dialog.ResponseText))
                {

                    DataFac.AddProperty(new Property(dialog.ResponseText));
                }
                else
                {
                    MessageBox.Show("Du mangler at udfylde feltet");
                }

            }

            FillOutProperty();


        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillOutData();
        }
    }
}


