using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Data
{
    public class DataFacade
    {
        BookingData booking = new BookingData();
        CategoryData cat = new CategoryData();
        PropertyData prop = new PropertyData();
        EquipmentData eq = new EquipmentData();

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
            prop.GetAll();
            cat.GetAll();
            eq.GetAll();
            

        }


    }
}
