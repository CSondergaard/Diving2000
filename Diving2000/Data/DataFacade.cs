using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;

namespace Logic.Data
{
    public class DataFacade
    {
        BookingData bookData = new BookingData();
        CategoryData catData = new CategoryData();
        PropertyData propData = new PropertyData();
        EquipmentData eqData = new EquipmentData();

        EquipmentRepo eqRep = new EquipmentRepo();
        CategoryRepo CatRep = new CategoryRepo();
        PropertyRepo PropRep = new PropertyRepo();
        BookingRepo BookRep = new BookingRepo();

        #region Singleton Region
        private static volatile DataFacade instance;

        public static DataFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataFacade();

                }
                return instance;
            }
        }
        #endregion

        public void GetAll()
        {
            propData.GetAll();
            catData.GetAll();
            eqData.GetAll();
            bookData.GetAll();
        }

        public void AddEquipment(Equipment obj)
        {
            Equipment newEq = eqData.Add(obj);
            eqRep.Add(newEq);
        }

        public void DeleteEquipmentById(int id)
        {
            eqData.DeleteById(id);
            eqRep.DeleteById(id);
        }

        public void DeleteEquipmentValue(string ValueName, int id, int prop)
        {
            eqData.DeleteEquipmentValue(ValueName, id, prop);
            PropRep.DeleteValue(ValueName, prop);
        }

        public void EditEquipment(Equipment obj)
        {
            eqData.Edit(obj);
            eqRep.Edit(obj);
        }

        public void AddCategory(Category obj)
        {
            Category newCat = catData.Add(obj);
            CatRep.Add(newCat);

        }

        public void DeleteCategory(int id)
        {
            List<Equipment> eqlist = eqRep.GetEquipmentsFromCategory(id, eqRep.GetAllEquipments());
            List<Booking> bookList = BookRep.GetAllBookings();

            foreach (Booking bk in bookList)
            {
                if (bk._equipment != null)
                {
                    foreach (Equipment eq in bk._equipment.ToList())
                    {
                        bool check = eqlist.Any(x => x._id == eq._id);
                        if (check)
                        {
                            DeleteEquipmentFromBooking(eq, bk);
                        }
                    }
                }
            }

            foreach (Equipment item in eqlist)
            {
                eqData.DeleteById(item._id);
                eqRep.DeleteById(item._id);
            }

            catData.DeleteById(id);
            CatRep.DeleteById(id);
        }

        public void EditCategory(Category obj)
        {
            catData.Edit(obj);
            CatRep.Edit(obj);
        }

        //Property

        public void AddProperty(Property prop)
        {
            Property newprop = propData.Add(prop);
            PropRep.Add(newprop);

        }

        public void AddValueToProp(string value, int id)
        {
            propData.AddValue(value, id);
            Property prop = PropRep.GetById(id);
            PropRep.AddValue(prop, value);
        }

        public void DeletePropertyById(int id)
        {
            propData.DeleteById(id);
            PropRep.DeleteById(id);
        }


        // Booking

        public void AddBooking(Booking obj)
        {
            Booking book = bookData.Add(obj);
            BookRep.Add(book);

        }

        public void DeleteBookingById(int id)
        {
            bookData.DeleteById(id);
            BookRep.DeleteById(id);
        }

        public void DeleteEquipmentFromBooking(Equipment eq, Booking bk)
        {
            bookData.DeleteEquipmentFromBooking(eq, bk);
            BookRep.DeleteEquipmentFromBooking(eq, bk);
        }

        public void AddEquipmentToBooking(Equipment eq, Booking bk)
        {
            bookData.AddEquipmentToBooking(eq, bk);
            BookRep.AddEquipmentToBooking(eq, bk);
        }

        public void RentBooking(int id)
        {
            bookData.RentBooking(id);
            BookRep.RentBooking(id);
        }

        public void DeleteBooking(int id)
        {
            bookData.DeleteById(id);
            BookRep.DeleteById(id);
        }



    }
}
