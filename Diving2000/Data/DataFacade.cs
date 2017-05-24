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
            catData.DeleteById(id);
            CatRep.DeleteById(id);
        }

        public void EditCategory(Category obj)
        {
            catData.Edit(obj);
            CatRep.Edit(obj);
        }

        public void AddProperty(Property prop)
        {
            Property newprop = propData.Add(prop);
            PropRep.Add(newprop);

        }

        public void AddValueToProp(string value, int id)
        {
            propData.AddValue(value, id);
            Property prop = PropRep.GetById(id);
            PropRep.AddValue(prop._name, value);
        }

        public void DeletePropertyById(int id)
        {
            propData.DeleteById(id);
            PropRep.DeleteById(id);
        }



    }
}
