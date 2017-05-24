using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Repository;

namespace Logic.Data
{
    public class CategoryData
    {
        CategoryRepo rep = new CategoryRepo();
        PropertyRepo PropRep = new PropertyRepo();

        dbConn db = new dbConn();
        public Category Add(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Category (Name, Thumbnail, Service, Alarm) VALUES (@name, @thumb, @serv, @alarm)");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@thumb", obj._thumbnail);
            cmd.Parameters.AddWithValue("@serv", obj._service);
            cmd.Parameters.AddWithValue("@alarm", obj._alarm);

            int id = db.InsertDataGetNewID(cmd);

            if (obj._values != null)
            {
                foreach (Property item in obj._values)
                {
                    MySqlCommand cmdtwo = new MySqlCommand("INSERT INTO CategoryValues (CategoryId, ValueId) VALUES (@catid, @valid)");
                    cmdtwo.Parameters.AddWithValue("@catid", id);
                    cmdtwo.Parameters.AddWithValue("@valid", item._id);
                    db.ModifyData(cmdtwo);
                }
            }

            obj._id = id;

            return obj;



        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Category WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);

        }

        public void Edit(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE Category SET Name = @name, Thumbnail = @thumb, Service = @serv WHERE Id = @id");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@id", obj._id);
            cmd.Parameters.AddWithValue("@thumb", obj._thumbnail);
            cmd.Parameters.AddWithValue("@serv", obj._service);

            db.ModifyData(cmd);

            foreach (Property val in obj._values)
            {
                MySqlCommand check = new MySqlCommand("SELECT COUNT(*) FROM CategoryValues AS nr WHERE CategoryId = @catid AND ValueId = @valid");
                check.Parameters.AddWithValue("@catid", obj._id);
                check.Parameters.AddWithValue("@valid", val._id);

                DataTable count = db.GetData(check);

                if (Convert.ToInt32(count.Rows[0]["nr"]) == 0)
                {
                    MySqlCommand cmdtwo = new MySqlCommand("INSERT INTO CategoryValues (CategoryId, ValueId) VALUES (@catid, @valid)");
                    cmdtwo.Parameters.AddWithValue("@catid", obj._id);
                    cmdtwo.Parameters.AddWithValue("@valid", val._id);
                    db.ModifyData(cmd);
                }
            }

        }

        public void GetAll()
        {
            MySqlCommand cmd = new MySqlCommand(@"
            SELECT Id, Name, Thumbnail, Service, Alarm
            FROM Category                  
            ");
            DataTable dt = db.GetData(cmd);

            foreach (DataRow rw in dt.Rows)
            {
                MySqlCommand cmdProp = new MySqlCommand(@"
                SELECT CategoryId, ValueId
                FROM CategoryValues
                WHERE CategoryId = @id
                ");
                cmdProp.Parameters.AddWithValue("@id", Convert.ToInt32(rw["id"]));
                DataTable dtProp = db.GetData(cmdProp);

                List<Property> ListProp = new List<Property>();
                foreach (DataRow row in dtProp.Rows)
                {
                    Property prop = PropRep.GetById(Convert.ToInt32(row["ValueId"]));
                    ListProp.Add(prop);
                }
                Category cat = new Category(Convert.ToInt32(rw["Id"]), rw["Name"].ToString(), ListProp, rw["Thumbnail"].ToString(), Convert.ToBoolean(rw["Service"]), Convert.ToInt32(rw["Alarm"]));
                rep.Add(cat);
            }

        }
    }
}
