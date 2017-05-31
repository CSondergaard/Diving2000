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
    public class PropertyData
    {

        PropertyRepo proprep = new PropertyRepo();
        dbConn db = new dbConn();

        public Property Add(Property prop)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Property (Name) VALUES (@name)");
            cmd.Parameters.AddWithValue("@name", prop._name);

            int id = db.InsertDataGetNewID(cmd);

            prop._id = id;

            if (prop._values != null)
            {
                foreach (string item in prop._values)
                {

                    MySqlCommand check = new MySqlCommand("SELECT COUNT(*) AS nr FROM Value WHERE PropertyId = @id AND Name = @name");

                    check.Parameters.AddWithValue("@id", id);
                    check.Parameters.AddWithValue("@name", item);

                    DataTable count = db.GetData(check);

                    if (Convert.ToInt32(count.Rows[0]["nr"]) == 0)
                    {

                        MySqlCommand cmdTwo = new MySqlCommand(@"INSERT INTO 
                                                         Value (PropertyId, Name) 
                                                         VALUES (@id, @name)");
                        cmdTwo.Parameters.AddWithValue("@id", id);
                        cmdTwo.Parameters.AddWithValue("@name", item);

                    }
                }
            }

            return prop;
        }

        public void AddValue(string value, int id)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Value (PropertyId, Name) VALUES (@id, @name)");

            cmd.Parameters.AddWithValue("@name", value);
            cmd.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);

        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Property WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);
            MySqlCommand cmdTwo = new MySqlCommand("DELTE FROM VALUE WHERE PropertyId = @id");
            cmdTwo.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);
            db.ModifyData(cmdTwo);
        }

        public void GetAll()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Id, Name FROM Property");
            DataTable dtProp = db.GetData(cmd);

            if (dtProp != null)
            {
                foreach (DataRow rw in dtProp.Rows)
                {
                    List<string> ValueList = GetAllValuesForProperty(Convert.ToInt32(rw["id"]));

                    proprep.Add(
                        new Property(
                            Convert.ToInt32(rw["Id"]),
                            rw["name"].ToString(),
                            ValueList
                            ));
                }
            }

        }

        public List<string> GetAllValuesForProperty(int Id)
        {
            List<string> ValueList = new List<string>();
            MySqlCommand cmdVal = new MySqlCommand("SELECT Name From Value WHERE PropertyId = @id");
            cmdVal.Parameters.AddWithValue("@id", Id);
            DataTable dtVal = db.GetData(cmdVal);

            foreach (DataRow val in dtVal.Rows)
            {
                ValueList.Add(val["Name"].ToString());
            }

            return ValueList;

        }

    }
}
