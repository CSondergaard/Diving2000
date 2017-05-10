using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository
{
    class EquipmentRepo : IRepo<Equipment>
    {

        private List<Equipment> EquipmentList = new List<Equipment>();

        dbConn db = new dbConn();

        public void Add(Equipment obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Equipment ");
            
        }

        public void AddToRepo(Equipment obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIdRepo(int id)
        {
            throw new NotImplementedException();
        }

        public Equipment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Equipment obj)
        {
            throw new NotImplementedException();
        }

        public void SaveRepo(Equipment obj)
        {
            throw new NotImplementedException();
        }
    }
}
