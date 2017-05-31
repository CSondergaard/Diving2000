using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Equipment
    {
        public int _id { get; set; }
        public int _catId { get; set; }
        public Dictionary<string, string> _values { get; set; }
        public DateTime? _service { get; set; }



        public Equipment(int catId, DateTime? service, Dictionary<string, string> value)
        {
            _catId = catId;
            _values = value;
            _service = service;
            
        }
        public Equipment(int id, int catId, DateTime? service, Dictionary<string, string> value)
        {
            _id = id;
            _catId = catId;
            _values = value;
            _service = service;

        }

        public Equipment(int id, int catId, DateTime? service)
        {
            _id = id;
            _catId = catId;
            _service = service;

        }

        public Equipment(int id, int catId)
        {
            _id = id;
            _catId = catId;
        }

    }
}
