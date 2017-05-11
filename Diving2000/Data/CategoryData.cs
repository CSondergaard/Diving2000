using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Data
{
    public class CategoryData
    {
        CategoryRepo rep = new CategoryRepo();
        dbConn db = new dbConn();
        public void Add(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Category (Name, Thumbnail, Service, Alarm) VALUES (@name, @thumb, @serv, @alarm)");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@thumb", obj._thumbnail);
            cmd.Parameters.AddWithValue("@serv", obj._service);
            cmd.Parameters.AddWithValue("@alarm", obj._alarm);



            int id = db.InsertDataGetNewID(cmd);

            foreach (Property item in obj._values)
            {
                MySqlCommand cmdtwo = new MySqlCommand("INSERT INTO CategoryValues (CategoryId, ValueId) VALUES (@catid, @valid)");
                cmdtwo.Parameters.AddWithValue("@catid", id);
                cmdtwo.Parameters.AddWithValue("@valid", item._id);
                db.ModifyData(cmd);
            }

            rep.Add(obj);
            


        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Category WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);

        }

        public void Edit(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE Category Name = @name, Thumbnail = @thumb, Service = @serv WHERE Id = @id");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@id", obj._id);
            cmd.Parameters.AddWithValue("@thumb", obj._thumbnail);
            cmd.Parameters.AddWithValue("@serv", obj._service);
           
            db.ModifyData(cmd);

            foreach (var item in obj._values)
            {

            }

            rep.Edit(obj);
         

        }

        public void GetAll()
        {
            MySqlCommand cmd = new MySqlCommand(@"
            SELECT Id, Name, Thumbnail, Service, Alarm
            FROM Category
            JOIN CategoryValues ON Category.Id = CategoryValues.CategoryId                    
            ");
            DataTable dt = db.GetData(cmd);

            foreach (DataRow rw in dt.Rows)
            {
                Category cat = new Category(Convert.ToInt32(rw["Id"]), rw["Name"].ToString(), rw["Thumbnail"].ToString(), Convert.ToBoolean(rw["Service"]), Convert.ToInt32(rw["Alarm"]));
                rep.Add(cat);
            }
             
        }
    }
}
