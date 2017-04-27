using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        CategoryRepo cat = new CategoryRepo();
        SubCategoryRepo subCat = new SubCategoryRepo();
        

        // Categoryj
        [TestMethod]
        public void CreateCategory()
        {
            cat.AddToRepo(new Category(1, "test", "jeg er en test"));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "test");
        }

        [TestMethod]
        public void EditCategory()
        {
            cat.AddToRepo(new Category(1, "test", "jeg er en test"));
            cat.SaveRepo(new Category(1, "OtherName", "jeg er en ny test"));
            Category cats = cat.GetById(1);
            Assert.AreEqual(cats._name, "OtherName");
            Assert.AreEqual(cats._Description, "jeg er en ny test");

        }

        [TestMethod]
        public void DeleteCategory()
        {
            cat.AddToRepo(new Category(1, "test", "jeg er en test"));
            cat.DeleteByIdRepo(1);
            Assert.IsTrue(cat.GetById(1)==null);
        }

        //SubCategory
        

        [TestMethod]
        public void CreateSubCategory()
        {
            subCat.AddToRepo(new SubCategory(1, "test", 1));
            SubCategory scat = subCat.GetById(1);
            Assert.AreEqual(scat._name, "test");
        }

        [TestMethod]
        public void EditSubCategory()
        {
            subCat.AddToRepo(new SubCategory(1, "test", 1));
            subCat.SaveRepo(new SubCategory(1, "OtherName", 2));
            SubCategory scat = subCat.GetById(1);
            Assert.AreEqual(scat._name, "OtherName");
            Assert.AreEqual(scat._catId, 2);

        }

        [TestMethod]
        public void DeleteSubCategory()
        {
            subCat.AddToRepo(new SubCategory(1, "test", 1));
            subCat.DeleteByIdRepo(1);
            Assert.IsTrue(cat.GetById(1) == null);
        }


    }
}
