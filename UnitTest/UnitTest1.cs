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
            Equip.Add(new Equipment(1, "Dragt", 2, DateTime.Now));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._name, "Dragt");
        }

        [TestMethod]

        public void EditEquipment()
        {
            Equip.Edit(new Equipment(1, "Hansker", 2, DateTime.Today));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._name, "Hansker");
        }

        [TestMethod]

        public void DeleteEquipment()
        {
            Equip.DeleteById(1);
            Assert.IsTrue(Equip.GetById(1) == null);
        }

        [TestMethod]
        public void SearchEquipment()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Str", "32");
            dic.Add("Tykkelse", "5mm");
            dic.Add("Tykkelse2", "4mm");
            dic.Add("Tykkelse3", "3mm");
            dic.Add("Tykkelse4", "2mm");
            dic.Add("Tykkelse5", "1mm");
            dic.Add("Tykkelse6", "6mm");
            dic.Add("Tykkelse7", "8mm");
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic2.Add("Str", "34");
            dic2.Add("Tykkelse", "4mm");
            dic2.Add("Tykkelse2", "4mm");
            dic2.Add("Tykkelse3", "3mm");
            dic2.Add("Tykkelse4", "2mm");
            dic2.Add("Tykkelse5", "1mm");
            dic2.Add("Tykkelse6", "6mm");
            dic2.Add("Tykkelse7", "8mm");


            Equip.Add(new Equipment(1, "Dragt",  2, DateTime.Now, dic));
            Equip.Add(new Equipment(2, "Dragt",  2, DateTime.Now, dic));
            Equip.Add(new Equipment(3, "Dragt",  2, DateTime.Now, dic));
            for (int i = 4; i < 250000; i++)
            {
                Equip.Add(new Equipment(i, "Dragt", 2, DateTime.Now, dic2));
            }

            List<Equipment> eq = Equip.GetAllEquipments();
            eq = Equip.GetEquipmentsFromCategory(2, eq);
            eq = Equip.SearchEquipment(dic2, eq);
            Assert.AreEqual(249996, eq.Count);


        }

        //Booking

        [TestMethod]
        public void CreateBooking()
        {
            EquipList.Add(new Equipment(1, "Dragt", 2, DateTime.Now));
            Book.Add(new Booking(1, EquipList, DateTime.Now, DateTime.Today, 41412622, true));
            Booking bk = Book.GetById(1);
            Assert.AreEqual(bk._phone, 41412622);
        }

        [TestMethod]
        public void AddTooBooking()
        {
            Booking bk = Book.GetById(1);

        }

        [TestMethod]
        public void AddEquipmentToCurrentBooking()
        {
            Booking bk = Book.GetById(1);
            Equipment eq = new Equipment(2, "Handske", 1);

            int nr = bk._equipment.Count;

            Book.AddEquipmentTooBooking(eq, bk);
            bk = Book.GetById(1);
            int newnr = bk._equipment.Count;
            Assert.AreEqual(nr + 1, newnr);
        }

        [TestMethod]
        public void DeleteEquipmentFromCurrentBooking()
        {

            Booking bk = Book.GetById(1);
            Equipment eq = new Equipment(2, "Handske",  1);

            int nr = bk._equipment.Count;

            Book.DeleteEquipmentFromBooking(eq, bk);
            bk = Book.GetById(1);
            int newnr = bk._equipment.Count;
            Assert.AreEqual(nr - 1, newnr);
        }



    }


}
