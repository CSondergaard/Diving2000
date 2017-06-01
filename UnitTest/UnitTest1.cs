using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Logic.Data;
using Logic.Repository;
using System.Collections.Generic;
using Diving_UI.Model;
using Diving_UI.Views;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        private List<Category> CategoryList = new List<Category>();
        EquipmentRepo Equip = new EquipmentRepo();
        CategoryRepo cat = new CategoryRepo();
        BookingRepo Book = new BookingRepo();
        List<Equipment> EquipList = new List<Equipment>();
        EquipmentSearch eqSearch = new EquipmentSearch();
        public string ValueName { get; private set; }

        // Categoryj
        [TestMethod]
        public void CreateCategory()
        {
            cat.Add(new Category(1, "test", "billede.jpeg", true, 2));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "test");
        }

        [TestMethod]
        public void CreateCategoryFail()
        {
            cat.Add(new Category(2, "test", "billede.jpeg", true, 2));
            Category cats = cat.GetById(2);
            Assert.AreNotEqual(cats._name, 22);
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
        public void EditCategoryFail()
        {
            cat.Edit(new Category(2, "OtherName", "nytbillede.jpg", true, 2));
            Category cats = cat.GetById(2);
            Assert.AreNotEqual(cats._name, "OtherName!");
            Assert.AreNotEqual(cats._thumbnail, 222);
        }

        [TestMethod]
        public void DeleteCategory()
        {
            cat.DeleteById(1);
            Assert.IsTrue(cat.GetById(1) == null);
        }

        [TestMethod]
        public void DeleteCategoryFail()
        {
            cat.DeleteById(1);
            Assert.AreNotEqual(cat.GetById(1), 2);
        }

        [TestMethod]
        public void AddCategoryValue()
        {
            Property propper = new Property(1, "Value");
            cat.Add(new Category(1, "AddValue", "doen'tmatter.jpeg", true, 12));
            cat.AddValue(1, propper);
            Category cats = cat.GetById(1);
            Property pro = cats._values.Find(x => x._id == 1);
            Assert.AreEqual(pro, propper);
        }

        [TestMethod]
        public void AddCategoryValueFail()
        {
            Property propper = new Property(1, "Value");
            cat.Add(new Category(1, "AddValue", "doen'tmatter.jpeg", true, 12));
            cat.AddValue(1, propper);
            Category cats = cat.GetById(1);
            Property pro = cats._values.Find(x => x._id == 1);
            Assert.AreNotEqual(pro, 1);
        }

        [TestMethod]
        public void GetByName()
        {

            cat.Add(new Category(1, "FindMe", "doesn'tmatter.jpeg", true, 12));
            Category cats = cat.GetByName("FindMe");
            Assert.AreEqual(cats._name, "FindMe");
        }

        [TestMethod]
        public void GetByNameFail()
        {

            cat.Add(new Category(1, "FindMe", "doesn'tmatter.jpeg", true, 12));
            Category cats = cat.GetByName("FindMe");
            Assert.AreNotEqual(2, cats._name);
        }

        //Equitment
        [TestMethod]

        public void CreateEquipmentFail()
        {
            Equip.Add(new Equipment(2, 2, DateTime.Now));
            Equipment equips = Equip.GetById(2);
            Assert.AreNotEqual(equips._id, "!");
        }

        [TestMethod]

        public void CreateEquipment()
        {
            Equip.Add(new Equipment(1, 2, DateTime.Now));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._id, 1);
        }

        [TestMethod]

        public void EditEquipment()
        {
            Equip.Edit(new Equipment(1, 3, DateTime.Now));
            Equipment equips = Equip.GetById(1);
            Assert.AreEqual(equips._catId, 3);
        }

        [TestMethod]

        public void EditEquipmentFail()
        {
            Equip.Edit(new Equipment(1, 3, DateTime.Now));
            Equipment equips = Equip.GetById(1);
            Assert.AreNotEqual(equips._catId, "fisk");
        }

        [TestMethod]

        public void DeleteEquipment()
        {
            Equip.DeleteById(1);
            Assert.IsTrue(Equip.GetById(1) == null);
        }
        [TestMethod]

        public void DeleteEquipmentFail()
        {
            Equip.DeleteById(1);
            Assert.AreNotEqual(Equip.GetById(1), 1);
        }
        [TestMethod]
        public void DeleteEquipment2()
        {
            Equip.DeleteById(2);
            Assert.IsTrue(Equip.GetById(2) == null);
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


            Equip.Add(new Equipment(1, 2, DateTime.Now, dic));
            Equip.Add(new Equipment(2, 2, DateTime.Now, dic));
            Equip.Add(new Equipment(3, 2, DateTime.Now, dic));
            for (int i = 4; i < 250000; i++)
            {
                Equip.Add(new Equipment(i, 2, DateTime.Now, dic2));
            }



            List<Equipment> eq = Equip.GetAllEquipments();
            eq = Equip.GetEquipmentsFromCategory(2, eq);
            eq = eqSearch.SearchEquipment(dic2, eq);
            Assert.AreEqual(249996, eq.Count);


        }

        [TestMethod]
        public void SearchEquipmentFail()
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


            Equip.Add(new Equipment(1, 2, DateTime.Now, dic));
            Equip.Add(new Equipment(2, 2, DateTime.Now, dic));
            Equip.Add(new Equipment(3, 2, DateTime.Now, dic));
            for (int i = 4; i < 250000; i++)
            {
                Equip.Add(new Equipment(i, 2, DateTime.Now, dic2));
            }



            List<Equipment> eq = Equip.GetAllEquipments();
            eq = Equip.GetEquipmentsFromCategory(2, eq);
            eq = eqSearch.SearchEquipment(dic2, eq);
            Assert.AreNotEqual(24996, eq.Count);


        }


        //Booking

        [TestMethod]
        public void CreateBooking()
        {
            EquipList.Add(new Equipment(1, 2, DateTime.Now));
            Book.Add(new Logic.Booking(1, EquipList, DateTime.Now, DateTime.Today, "41412622", true));
            Logic.Booking bk = Book.GetById(1);
            Assert.AreEqual(bk._phone, "41412622");
        }

        [TestMethod]
        public void CreateBookingFail()
        {
            EquipList.Add(new Equipment(2, 2, DateTime.Now));
            Book.Add(new Logic.Booking(2, EquipList, DateTime.Now, DateTime.Today, "HerKommerPipi", true));
            Logic.Booking bk = Book.GetById(2);
            Assert.AreNotEqual(bk._phone, "41412622");
        }

        [TestMethod]
        public void AddEquipmentToCurrentBooking()
        {
            Logic.Booking bk = Book.GetById(1);
            Equipment eq = new Equipment(2, 1);

            int nr = bk._equipment.Count;

            Book.AddEquipmentToBooking(eq, bk);
            bk = Book.GetById(1);
            int newnr = bk._equipment.Count;
            Assert.AreEqual(nr + 1, newnr);
        }

        [TestMethod]
        public void AddEquipmentToCurrentBookingFail()
        {
            Logic.Booking bk = Book.GetById(1);
            Equipment eq = new Equipment(2, 1);

            int nr = bk._equipment.Count;

            Book.AddEquipmentToBooking(eq, bk);
            bk = Book.GetById(1);
            int newnr = bk._equipment.Count;
            Assert.AreNotEqual(nr + "heathrow", newnr);
        }

        [TestMethod]
        public void DeleteEquipmentFromCurrentBooking()
        {

            Logic.Booking bk = Book.GetById(2);
            Equipment eq = new Equipment(2, 1);

            int nr = bk._equipment.Count;

            Book.DeleteEquipmentFromBooking(eq, bk);
            bk = Book.GetById(2);
            int newnr = bk._equipment.Count;
            Assert.AreEqual(nr - 1, newnr);
        }

        [TestMethod]

        public void GetByPhone()
        {
            Book.Add(new Logic.Booking(5, EquipList, DateTime.Now, DateTime.Today, "21212121", true));
            Logic.Booking bk = Book.GetById(5);
            Book.GetByPhone("21212121");
            Assert.AreEqual("21212121", bk._phone);
        }

        [TestMethod]

        public void GetByPhoneFail()
        {
            Book.Add(new Logic.Booking(5, EquipList, DateTime.Now, DateTime.Today, "21212121", true));
            Logic.Booking bk = Book.GetById(5);
            Book.GetByPhone("21212121");
            Assert.AreNotEqual(21212121, bk._phone);
        }
    }
}

