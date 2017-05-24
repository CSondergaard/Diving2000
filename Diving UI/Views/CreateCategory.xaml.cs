using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Logic.Repository;
using Logic.Data;
using Logic;
using Microsoft.Win32;
using System.Linq;

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for CreateCategory.xaml
    /// </summary>
    public partial class CreateCategory : UserControl
    {
        PropertyRepo PropRep = new PropertyRepo();
        CategoryData CatData = new CategoryData();

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

            if(PropForCatList.Any(x => x._name == prop._name))
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

            CatData.Add(new Category(
                txtName.Text,
                "",
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
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".jpg"; // Required file extension 
            fileDialog.Filter = "Billeder (.jpg)|*.jpg"; // Optional file extensions

            bool? res = fileDialog.ShowDialog();

            if (res.HasValue && res.Value)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(fileDialog.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

    }
}


