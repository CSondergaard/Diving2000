using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Booking
    {
        public int _id { get; set; }
        public List<Equipment> _equipment { get; set; }
        public DateTime _startDate { get; set; }
        public DateTime _endDate { get; set; }
        public int _phone { get; set; }
        public bool _status { get; set; }


        public Booking(int id, List<Equipment> equip, DateTime start, DateTime end, int phone, bool status)
        {
            _id = id;
            _equipment = equip;
            _startDate = start;
            _endDate = end;
            _phone = phone;
            _status = status;

        }
        public Booking(List<Equipment> equip, DateTime start, DateTime end, int phone, bool status)
        {
            _equipment = equip;
            _startDate = start;
            _endDate = end;
            _phone = phone;
            _status = status;

        }

    }
}
