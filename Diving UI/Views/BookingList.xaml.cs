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
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : UserControl
    {
        EquipmentRepo eqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        DataFacade DataFac = DataFacade.Instance;
        BookingRepo BookRep = new BookingRepo();

        SearchBookingList search = SearchBookingList.Instance;

        public BookingList()
        {
            InitializeComponent();
            OnBookingListLoad(BookRep.GetAllBookings());

            search.Search += new SearchBookingList.SearchEventHandler(BookingGetSearched);

        }

        private void BookingGetSearched(string id)
        {
            List<Logic.Booking> list = BookRep.GetByPhone(id);

            OnBookingListLoad(list);
        }


        private void OnBookingListLoad(List<Logic.Booking> Booklist)
        {
            SpInv.Children.Clear();
            if (Booklist.Count == 0)
            {

            }
            else
            {
                foreach (Logic.Booking item2 in Booklist)
                {
                    Border br = new Border();
                    br.BorderThickness = new Thickness(1);
                    br.BorderBrush = new SolidColorBrush(Colors.Black);
                    br.Margin = new Thickness(0, 0, 0, 10);
                    SpInv.Children.Add(br);

                    Grid bigDG = new Grid();
                    RowDefinition row1 = new RowDefinition();
                    RowDefinition row2 = new RowDefinition();
                    bigDG.RowDefinitions.Add(row1);
                    bigDG.RowDefinitions.Add(row2);
                    br.Child = bigDG;

                    Label BookPhone = new Label();
                    Label BookInfo = new Label();

                    string StartDate = item2._startDate.ToString("dd-MM-yyyy");
                    string EndDate = item2._startDate.ToString("dd-MM-yyyy");

                    BookPhone.Content = "TLF: " + item2._phone;
                    BookInfo.Content = "Udlejet fra: " + StartDate + " til " + EndDate;
                    BookInfo.FontSize = 15;
                    Grid.SetRow(BookInfo, 0);
                    StackPanel spBookInfo = new StackPanel();
                    spBookInfo.Children.Add(BookPhone);
                    spBookInfo.Children.Add(BookInfo);
                    bigDG.Children.Add(spBookInfo);

                    if (item2._status == false)
                    {
                        Button RentBtn = new Button();
                        Grid.SetRow(RentBtn, 0);
                        RentBtn.HorizontalAlignment = HorizontalAlignment.Right;
                        RentBtn.VerticalAlignment = VerticalAlignment.Center;
                        RentBtn.Height = 20;
                        RentBtn.Width = 60;
                        RentBtn.Margin = new Thickness(0, 0, 20, 0);
                        RentBtn.Background = new SolidColorBrush(Colors.Green);
                        RentBtn.Content = "Udlej";
                        RentBtn.Click += btnRent;
                        RentBtn.Tag = item2._id;
                        bigDG.Children.Add(RentBtn);
                    }
                    else
                    {
                        Label RentLbl = new Label();
                        Grid.SetRow(RentLbl, 0);
                        RentLbl.HorizontalAlignment = HorizontalAlignment.Right;
                        RentLbl.Height = 30;
                        RentLbl.Width = 60;
                        RentLbl.Margin = new Thickness(0, 0, 20, 0);
                        RentLbl.Content = "Udlejet";
                        bigDG.Children.Add(RentLbl);
                    }

                    StackPanel RentedItems = new StackPanel();
                    Grid.SetRow(RentedItems, 1);
                    bigDG.Children.Add(RentedItems);

                    foreach (Equipment item in item2._equipment)
                    {
                        Category Cat = CatRep.GetById(item._catId);

                        Border DGbr = new Border();
                        DGbr.BorderThickness = new Thickness(0, 1, 0, 0);
                        DGbr.BorderBrush = new SolidColorBrush(Colors.Black);
                        RentedItems.Children.Add(DGbr);

                        Grid DG = new Grid();
                        Grid.SetRow(DG, 1);
                        ColumnDefinition col1 = new ColumnDefinition();
                        col1.Width = new GridLength(190);
                        ColumnDefinition col2 = new ColumnDefinition();
                        col2.Width = new GridLength(190);
                        ColumnDefinition col3 = new ColumnDefinition();
                        col3.Width = new GridLength(198);
                        DG.ColumnDefinitions.Add(col1);
                        DG.ColumnDefinitions.Add(col2);
                        DG.ColumnDefinitions.Add(col3);
                        DGbr.Child = DG;

                        Button Delbtn = new Button();
                        Grid.SetRow(Delbtn, 1);
                        Grid.SetColumn(Delbtn, 2);
                        Delbtn.Width = 60;
                        Delbtn.Height = 20;
                        Delbtn.Click += btnDel;
                        Delbtn.Tag = item._id.ToString() + "," + item2._id.ToString();
                        Delbtn.Margin = new Thickness(0, 0, 0, 0);
                        Delbtn.Foreground = Brushes.White;
                        Delbtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C9302C"));
                        Delbtn.Content = "Slet";
                        Delbtn.HorizontalAlignment = HorizontalAlignment.Right;
                        Delbtn.VerticalAlignment = VerticalAlignment.Center;
                        DG.Children.Add(Delbtn);

                        Label lb = new Label();
                        lb.VerticalAlignment = VerticalAlignment.Center;
                        lb.Content = item._name;
                        Grid.SetColumn(lb, 0);
                        DG.Children.Add(lb);

                        WrapPanel wp = new WrapPanel();
                        Grid.SetColumn(wp, 1);
                        DG.Children.Add(wp);

                        foreach (KeyValuePair<string, string> val in item._values)
                        {
                            Label lb2 = new Label();
                            lb2.VerticalAlignment = VerticalAlignment.Center;
                            lb2.Content = val.Key + ": " + val.Value;
                            wp.Children.Add(lb2);

                        }
                    }
                }
            }
        }
        private void btnDel(object sender, RoutedEventArgs e)
        {
            string[] ids = (sender as Button).Tag.ToString().Split(',');
            int id = Convert.ToInt32(ids[0]);
            Logic.Booking book = BookRep.GetById(Convert.ToInt32(ids[1]));
            Equipment eq = eqRep.GetById(id);


            DataFac.DeleteEquipmentFromBooking(eq, book);
            BookingGetSearched(search.GetPhone());
        }

        private void btnRent(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Tag);



        }
    }
}
