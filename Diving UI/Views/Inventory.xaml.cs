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

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        EquipmentRepo eqRep = new EquipmentRepo();

        public Inventory()
        {
            InitializeComponent();

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Equipment> eqlist = eqRep.GetAllEquipments();
            foreach (Equipment item in eqlist)
            {
                StackPanel sp = new StackPanel();
                Label lb = new Label();
                lb.Content = item._name;
                sp.Children.Add(lb);
                SpInv.Children.Add(sp);
            }
        }
    }
}
