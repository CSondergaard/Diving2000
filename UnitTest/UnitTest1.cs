using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Logic.Data;
using Logic.Repository;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        EquipmentRepo Equip = new EquipmentRepo();
        CategoryRepo cat = new CategoryRepo();
        BookingRepo Book = new BookingRepo();
        List<Equipment> EquipList = new List<Equipment>();

        // Categoryj
        [TestMethod]
        public void CreateCategory()
        {
            cat.Add(new Category(1, "test", "billede.jpeg", true, 2));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "test");
        }

        [TestMethod]
        public void EditCategory()
        {
            cat.Edit(new Category(1, "OtherName", "nytbillede.jpg", true, 2));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "OtherName");
            Assert.AreEqual(cats._thumbnail, "nytbillede.jpg");

        }

        [TestMethod]
        public void DeleteCategory()
        {
            cat.DeleteById(1);
            Assert.IsTrue(cat.GetById(1) == null);
        }

        //Equitment
        [TestMethod]

        public void CreateEquipment()
        {
            Equip.Add(new Equipment(1, "Dragt", "Hej", 2, DateTime.Now));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._name, "Dragt");
        }

        [TestMethod]

        public void EditEquipment()
        {
            Equip.Edit(new Equipment(1, "Hansker", "Dav", 2, DateTime.Today));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._name, "Hansker");
        }

        [TestMethod]

        public void DeleteEquipment()
        {
            Equip.DeleteById(1);
            Assert.IsTrue(Equip.GetById(1) == null);
        }

        //Booking

        [TestMethod]
        public void CreateBooking()
        {
            EquipList.Add(new Equipment(1, "Dragt", "Hej", 2, DateTime.Now));
            Book.Add(new Booking(1, EquipList, DateTime.Now, DateTime.Today, 41412622, true));
            Booking bk = Book.GetById(1);
            Assert.AreEqual(bk._phone, 41412622);
        }

        [TestMethod]
        public void AddTooBooking()
        {
            EquipList.Add(new Equipment(1, "Dragt", "Hej", 2, DateTime.Now));
            Book.Add(new Booking(1, EquipList, DateTime.Now, DateTime.Today, 41412622, true));
            Booking bk = Book.GetById(1);
            
        }



    }


}
