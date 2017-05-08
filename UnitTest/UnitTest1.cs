using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        CategoryRepo cat = new CategoryRepo();

        // Categoryj
        [TestMethod]
        public void CreateCategory()
        {
            cat.Add(new Category(1, "test", "billede.jpeg"));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "test");
        }

        [TestMethod]
        public void EditCategory()
        {
            cat.Add(new Category(1, "test", "jeg er en test"));
            cat.Edit(new Category(1, "OtherName", "nytbillede.jpg"));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "OtherName");
            Assert.AreEqual(cats._thumbnail, "nytbillede.jpg");

        }

        [TestMethod]
        public void DeleteCategory()
        {
            cat.Add(new Category(1, "test", "jeg er en test"));
            cat.DeleteById(1);
            Assert.IsTrue(cat.GetById(1)==null);
        }

    


    }
}
