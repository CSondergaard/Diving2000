using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CategoryRepo : IRepo<Category>
    {
        private List<Category> CategoryList = new List<Category>();

        dbConn db = new dbConn();

        public void Add(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Category (Name, Description) VALUES (@name, @desc)");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@desc", obj._Description);

            int id = db.InsertDataGetNewID(cmd);

            AddToRepo(new Category(id, obj._name, obj._Description));

        }


        public void AddToRepo(Category obj)
        {
            CategoryList.Add(obj);
        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Category WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmd);
            DeleteByIdRepo(id);
        }

        public void DeleteByIdRepo(int id)
        {
            Category cat = GetById(id);
            CategoryList.Remove(cat);

        }

        public Category GetById(int id)
        {
            Category cat = CategoryList.Find(x => x._id == id);
            if (cat == null)
            {
                return null;
            }
            else
            {
                return cat;
            }


        }

        public void Save(Category obj)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE Category Name = @name, Desc = @desc WHERE Id = @id");
            cmd.Parameters.AddWithValue("@name", obj._name);
            cmd.Parameters.AddWithValue("@desc", obj._Description);
            cmd.Parameters.AddWithValue("@id", obj._id);

            db.ModifyData(cmd);
            SaveRepo(obj);

        }

        public void SaveRepo(Category obj)
        {
            Category cat = GetById(obj._id);
            if (cat == null)
            {
                
            }
            else
            {
                cat._Description = obj._Description;
                cat._name = obj._name;
            }

        }
    }
}
