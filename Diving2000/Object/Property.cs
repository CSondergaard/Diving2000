﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Property
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public List<string> _values { get; set; }
        

        public Property (int id, string name)
        {
            _id = id;
            _name = name;
        }
        public Property(int id, string name, List<string> value)
        {
            _id = id;
            _name = name;
            _values = value;
        }


    }
}
