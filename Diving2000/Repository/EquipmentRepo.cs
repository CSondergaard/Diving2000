using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository
{
    public class EquipmentRepo : IRepo<Equipment>
    {

        private static List<Equipment> EquipmentList = new List<Equipment>();

        dbConn db = new dbConn();

        public void Add(Equipment obj)
        {
            EquipmentList.Add(obj);
            
        }

        public void DeleteById(int id)
        {
            Equipment eq = GetById(id);

            EquipmentList.Remove(eq);
        }

        public Equipment GetById(int id)
        {
            return EquipmentList.Find(x => x._id == id);

        }

        public void Edit(Equipment obj)
        {
            Equipment OldObj = GetById(obj._id);

            OldObj._name = obj._name;
            OldObj._description = obj._description;
            OldObj._catId = obj._catId;
            OldObj._values = obj._values;
            OldObj._service = obj._service;

           
            
        }

    }
}
