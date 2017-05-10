using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Equipment
    {
        private string _id { get; set; }
        private string _name { get; set; }

        private string _description { get; set; }
        private int _catId { get; set; }


        public Equipment(string name, string value, int size, int catId)
        {
            _name = name;
            _value = value;
            _size= size;
            _catId = catId;

        }
        public Equipment(int id,  string name, string value, int size, int catId)
        {
            _id = id;
            _name = name;
            _value = value;
            _size = size;
            _catId = catId;

        }

    }
}
