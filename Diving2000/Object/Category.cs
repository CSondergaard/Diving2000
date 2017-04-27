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

        public Dictionary<string, string[]> _values { get; set; }
        // Dictionary<ValuesName, Values value Array

        public int WarningCount { get; set; }


        public Category(int id, string Name, Dictionary<string, string[]> dic)
        {
            _id = id;
            _name = Name;
            _values = dic;
        }

        public Category(string Name, Dictionary<string, string[]> dic)
        {
            _name = Name;
            _values = dic;
        }

    }

}
