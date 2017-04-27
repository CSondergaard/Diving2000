using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SubCategoryRepo : IRepo<SubCategory>
    {
        private List<SubCategory> SubCategoryList = new List<SubCategory>();

        dbConn db = new dbConn();

        public void Add (SubCategory obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO SubCategory (Name, catId) VALUES (@name, @catid)");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@catid", obj._catId);

            int id = db.InsertDataGetNewID(cmd);

            AddToRepo(new SubCategory(id, obj._name, obj._catId));

        }
        public void AddToRepo(SubCategory obj)
        {
            SubCategoryList.Add(obj);
        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM SubCategory WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);
            DeleteByIdRepo(id);
        }


        public void DeleteByIdRepo(int id)
        {
            SubCategory cat = GetById(id);
            SubCategoryList.Remove(cat);
        }
        public SubCategory GetById(int id)
        {
            SubCategory cat = SubCategoryList.Find(x => x._id == id);
            if (cat == null)
            {
                return null;
            }
            else
            {
                return cat;
            }
        }

        public void Save(SubCategory obj)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE SubCategory Name = @name, CatId WHERE Id = @id");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@desc", obj._catId);
            cmd.Parameters.AddWithValue("@id", obj._id);

            db.ModifyData(cmd);
            SaveRepo(obj);
        }

        public void SaveRepo(SubCategory obj)
        {
            SubCategory cat = GetById(obj._id);
            if (cat == null)
            {

            }
            else
            {
                cat._catId = obj._catId;
                cat._name = obj._name;
            }

        }
    }
}
