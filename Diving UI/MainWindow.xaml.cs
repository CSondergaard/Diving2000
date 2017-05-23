using Diving_UI.Views;
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
using Logic.Data;

namespace Diving_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataFacade fac = new DataFacade();

        public MainWindow()
        {
            InitializeComponent();
            fac.GetAll();
        }

        private void btnHomeClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.FindName("FrameFilter") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameChart") as Frame).Source = null;
            (Application.Current.MainWindow.FindName("FrameContent") as Frame).Source = new Uri(@"\Views\FrontPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void GetAllDate()
        {
            fac.GetAll();
        }
    }

}
