﻿using Diving_UI.Model;
using Logic;
using Logic.Data;
using Logic.Repository;
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

namespace Diving_UI.Views
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        EquipmentRepo eqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        DataFacade DataFac = DataFacade.Instance;
        CurrentBookingList bklist = CurrentBookingList.Instance;

        SearchBooking searchBook = SearchBooking.Instance;
        public Booking()
        {
            InitializeComponent();
            ShowEquipments(eqRep.GetAllEquipments());

            searchBook.SearchChanged += new SearchBooking.ChangedSearchEventHandler(Searcheq_SearchChanged);

        }

        private void Searcheq_SearchChanged(List<Equipment> eqlist)
        {
            ShowEquipments(eqlist);
        }

        private void ShowEquipments(List<Equipment> eqlist)
        {
            SpInv.Children.Clear();

            foreach (Equipment item in eqlist)
            {
                Border br = new Border();
                br.BorderThickness = new Thickness(1);
                br.BorderBrush = new SolidColorBrush(Colors.Black);
                br.Margin = new Thickness(0, 0, 0, 10);
                SpInv.Children.Add(br);

                Grid DG = new Grid();
                DG.Width = 600;
                DG.MinHeight = 75;
                DG.Background = new SolidColorBrush(Colors.Transparent);
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(75);
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = new GridLength(425);
                ColumnDefinition col3 = new ColumnDefinition();
                col3.Width = new GridLength(100);
                DG.ColumnDefinitions.Add(col1);
                DG.ColumnDefinitions.Add(col2);
                DG.ColumnDefinitions.Add(col3);
                br.Child = DG;

                Button Delbtn = new Button();
                Grid.SetRow(Delbtn, 0);
                Grid.SetColumn(Delbtn, 2);
                Delbtn.Width = 60;
                Delbtn.Height = 20;
                Delbtn.Click += btnAddToList;
                Delbtn.Tag = item._id.ToString();
                Delbtn.Margin = new Thickness(0, 0, 0, 10);
                Delbtn.Foreground = Brushes.White;
                Delbtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5FD080"));
                Delbtn.Content = "Tilføj";
                Delbtn.HorizontalAlignment = HorizontalAlignment.Center;
                Delbtn.VerticalAlignment = VerticalAlignment.Bottom;
                DG.Children.Add(Delbtn);

                Border brimg = new Border();
                brimg.BorderThickness = new Thickness(1);
                brimg.BorderBrush = new SolidColorBrush(Colors.Black);
                brimg.Margin = new Thickness(10, 10, 10, 10);
                Grid.SetColumn(brimg, 0);
                DG.Children.Add(brimg);

                Image img = new Image();

                Category Cat = CatRep.GetById(item._catId);
                string url = "../Resources/18217764_1871114903128407_1044267123_n.png";
                if (Cat._thumbnail != null)
                    if (!string.IsNullOrWhiteSpace(Cat._thumbnail))
                        url = Cat._thumbnail;

                img.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                img.Height = 50;
                img.Width = 50;

                brimg.Child = img;

                StackPanel spcol1 = new StackPanel();
                Grid.SetColumn(spcol1, 1);
                DG.Children.Add(spcol1);

                Label lb = new Label();
                lb.VerticalAlignment = VerticalAlignment.Center;
                lb.Content = Cat._name;
                lb.FontSize = 20;
                Grid.SetColumn(lb, 1);
                spcol1.Children.Add(lb);

                WrapPanel wp = new WrapPanel();
                Grid.SetColumn(wp, 1);
                spcol1.Children.Add(wp);

                foreach (KeyValuePair<string, string> val in item._values)
                {
                    Label lb2 = new Label();
                    lb2.VerticalAlignment = VerticalAlignment.Center;
                    lb2.Content = val.Key + ": " + val.Value;
                    wp.Children.Add(lb2);

                }
            }
        }
        private void btnAddToList(object sender, RoutedEventArgs e)
        {

            if (bklist.GetEnddate() == null || bklist.GetStartdate() == null)
            {
                MessageBox.Show("Du mangler at udfylde dato eller trykke søg");
            }
            else
            {
                int id = Convert.ToInt32((sender as Button).Tag);
                Equipment eq = eqRep.GetById(id);
                bklist.AddItemToBookingList(eq);
            }

        }
    }
}
