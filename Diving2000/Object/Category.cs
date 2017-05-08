using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{


    public class Category
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public List<Property> _values { get; set; }
        public string _thumbnail { get; set; }
        public bool _service { get; set; }

        public Category (int id, string name, List<Property> prop, string thumb, bool service)
        {
            _id = id;
            _name = name;
            _values = prop;
            _thumbnail = thumb;
            _service = service;
        }

        public Category (int id, string name, string thumb, bool service)
        {
            _id = id;
            _name = name;
            _thumbnail = thumb;
            _service = service;
        }

        public Category(string name, string thumb, List<Property> prop, bool service)
        {
            _name = name;
            _values = prop;
            _thumbnail = thumb;
            _service = service;
        }

    }

}
