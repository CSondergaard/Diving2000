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
        PropertyRepo proprep = new PropertyRepo();
        dbConn db = new dbConn();

        public void Add(Equipment obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Equipment (Name, Description, catId, service) VALUES (@name, @desc, @catid, @service)");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@desc", obj._description);
            cmd.Parameters.AddWithValue("@catid", obj._catId);
            cmd.Parameters.AddWithValue("@service", obj._service);

            int id = db.InsertDataGetNewID(cmd);


            foreach (KeyValuePair<string, string> item in obj._values)
            {
                Property prop = proprep.GetByName(item.Key);

                MySqlCommand cmdTwo = new MySqlCommand("INSERT INTO EquipmentValues (Property, Equipment, Values) VALUES(@prop, @eq, @val)");
                cmdTwo.Parameters.AddWithValue("@prop", prop._id);
                cmdTwo.Parameters.AddWithValue("@eq", id);
                cmdTwo.Parameters.AddWithValue("val", item.Value);

                db.ModifyData(cmdTwo);

            }

            rep.Add(obj);



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
