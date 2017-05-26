using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Logic.Repository;
using Logic.Data;
using Logic;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for CreateCategory.xaml
    /// </summary>
    public partial class CreateCategory : UserControl
    {
        PropertyRepo PropRep = new PropertyRepo();
        DataFacade DataFac = new DataFacade();

        List<Property> PropForCatList = new List<Property>();

        public CreateCategory()
        {
            InitializeComponent();
            FillOutProperty();
        }



        private void FillOutProperty()
        {
            ObservableCollection<string> cblist = new ObservableCollection<string>();

            List<Property> Proplist = PropRep.GetAllProperty();

            foreach (Property item in Proplist)
            {
                CBDefinition.Items.Add(item._name);
            }

        }

        private void btnDefinition_Click(object sender, RoutedEventArgs e)
        {
            Property prop = PropRep.GetByName(CBDefinition.Text);

            if (PropForCatList.Any(x => x._name == prop._name))
            {

            }
            else
            {
                PropForCatList.Add(prop);
                labelSelectedProperty.Content = "";
            }

            labelSelectedProperty.Content = "";

            foreach (Property item in PropForCatList)
            {
                labelSelectedProperty.Content += item._name + " ";
            }


        }

        private void btnCreateCategory_Click(object sender, RoutedEventArgs e)
        {
            bool Service = false;

            if (RBYes.IsChecked == true)
            {
                Service = true;
            }

            DataFac.AddCategory(new Category(
                txtName.Text,
                lbUploadName.Content.ToString(),
                PropForCatList,
                Service,
                Convert.ToInt32(txtAlarm.Text)
                ));

            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);
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

    }
}


