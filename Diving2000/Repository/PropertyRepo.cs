﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository
{
    public class PropertyRepo : IRepo<Property>
    {

        private List<Property> PropertyList = new List<Property>();

        public void Add(Property obj)
        {
            PropertyList.Add(obj);
        }

        public void DeleteById(int id)
        {
            Property prop = GetById(id);

            PropertyList.Remove(prop);
        }

        public void Edit(Property obj)
        {
            Property OldProp = GetById(obj._id);

            OldProp._name = obj._name;
            OldProp._values = obj._values;

        }

        public Property GetById(int id)
        {
            return PropertyList.Find(x => x._id == id);
        }

        public Property GetByName(string name)
        {
            return PropertyList.Find(x => x._name == name);

        }

    }
}
