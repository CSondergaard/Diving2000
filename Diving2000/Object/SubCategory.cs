using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CategoryValues
    {
        public int _catId { get; set; }
        public List<int> _value { get; set; }

        public CategoryValues(int catId, List<int> list)
        {
            _catId = catId;
            _value = list;
          

        }



    }
}
