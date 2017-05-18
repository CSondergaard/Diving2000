using Logic.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Data
{
    class PropertyData
    {
        PropertyRepo proprep = new PropertyRepo();
        dbConn db = new dbConn();

        public void Add(Property prop)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Equipment (Name) VALUES (@name)");
            cmd.Parameters.AddWithValue("@name", prop._name);

            int id = db.InsertDataGetNewID(cmd);

            prop._id = id;

            foreach (string item in prop._values)
            {
                MySqlCommand check = new MySqlCommand("SELECT COUNT(*) WHERE PropertyId = @id, Name = @name");

                check.Parameters.AddWithValue("@id", id);
                check.Parameters.AddWithValue("@name", item);

                DataTable count = db.GetData(check);




                MySqlCommand cmdTwo = new MySqlCommand(@"INSERT INTO 
                                                         Value (PropertyId, Name) 
                                                         VALUES (@id, @name)");
                cmdTwo.Parameters.AddWithValue("@id", id);
                cmdTwo.Parameters.AddWithValue("@name", item);
            }




        }

        public void DeleteById(int id)
        {

        }

       public void Edit(Property prop)
        {


        }

        public void GetAll()
        {



        }



    }
}
