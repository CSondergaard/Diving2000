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
        CategoryRepo CatRep = new CategoryRepo();

        public Inventory()
        {
            InitializeComponent();
            OnInventoryLoad();
           
        }
        private void OnInventoryLoad()
        {
            List<Equipment> eqlist = eqRep.GetAllEquipments();
            foreach (Equipment item in eqlist)
            {
                Category Cat = CatRep.GetById(item._catId);
                string url = @"\Resources\CategoryPic\" + Cat._thumbnail;
                //string url2 = @""

                WrapPanel wp = new WrapPanel();
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                Label lb = new Label();
                lb.Content = item._name;
                wp.Children.Add(img);
                wp.Children.Add(lb);
                SpInv.Children.Add(wp);
            }
        }
    }
}
