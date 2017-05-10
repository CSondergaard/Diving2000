using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;
using MySql.Data.MySqlClient;

namespace Logic.Data
{
    class EquipmentData
    {

        EquipmentRepo rep = new EquipmentRepo();
        dbConn db = new dbConn();

        public void Add(Equipment ojb)
        {
            MySqlCommand cmd = new MySqlCommand("");




       }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Equipment WHERE id = @id");
            cmd.Parameters.AddWithValue("@id", id);
            db.ModifyData(cmd);

            rep.DeleteById(id);



        }


    }
}
