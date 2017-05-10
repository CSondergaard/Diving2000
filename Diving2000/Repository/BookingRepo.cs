using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository
{
    class BookingRepo : IRepo<Booking>
    {

        private List<Booking> BookingList = new List<Booking>();

        public void Add(Booking obj)
        {
            BookingList.Add(obj);
        }

        public void DeleteById(int id)
        {
            Booking book = GetById(id);

            BookingList.Remove(book);
        }

        public void Edit(Booking obj)
        {
            Booking OldBook = GetById(obj._id);

            OldBook._equipment = obj._equipment;
            OldBook._startDate = obj._startDate;
            OldBook._endDate = obj._endDate;
            OldBook._phone = obj._phone;
            OldBook._status = obj._status;

        }

        public Booking GetById(int id)
        {
            return BookingList.Find(x => x._id == id);
        }
    }
}
