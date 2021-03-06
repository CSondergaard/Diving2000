﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CategoryRepo : ICategory
    {
        private static List<Category> CategoryList = new List<Category>();

        public void Add(Category obj)
        {
            CategoryList.Add(obj);
        }

        public void AddValue(int id, Property property)
        {
            Category cat = GetById(id);
            cat._values.Add(property);
        }

        public void DeleteById(int id)
        {
            CategoryList.Remove(GetById(id));
        }

        public void Edit(Category obj)
        {
            Category cat = GetById(obj._id);
            cat._name = obj._name;
            cat._thumbnail = obj._thumbnail;
            cat._values = obj._values;
            cat._service = obj._service;
            cat._alarm = obj._alarm;
        }

        public Category GetById(int id)
        {
            Category cat = CategoryList.Find(x => x._id == id);
            return cat;
        }

        public Category GetByName(string name)
        {
            Category cat = CategoryList.Find(x => x._name == name);
            return cat;
        }

        public List<Category> GetAll()
        {
            return CategoryList;
        }
    }
}
