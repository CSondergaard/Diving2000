using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Repository;

namespace Diving_UI.Model
{
    public class AlarmLogic
    {

        CategoryRepo catRep = new CategoryRepo();
        EquipmentRepo eqRep = new EquipmentRepo();
        BookingRepo bkRep = new BookingRepo();

        public int GetAlarms()
        {
            int nr = 0;

            List<Category> catlist = AlarmForCategory();
            if(catlist.Count > 0)
                nr = nr + catlist.Count;

            List<Booking> bklist = AlarmForBooking();
            if (bklist.Count > 0)
                nr = nr + bklist.Count;

            List<Booking> NoPickupList = AlarmForBookingPickup();
            if (NoPickupList.Count > 0)
                nr = nr + NoPickupList.Count;

            return nr;
        }

        public List<Category> AlarmForCategory()
        {
            int nreq = 0;
            List<Category> catList = new List<Category>();
            foreach (Category cat in catRep.GetAll())
            {
                int alarm = cat._alarm;              
                foreach (Equipment item in eqRep.GetEquipmentsFromCategory(cat._id, eqRep.GetAllEquipments()))
                {
                    nreq++;
                }
                if (alarm > nreq)
                {
                    catList.Add(cat);
                }

                nreq = 0;

            }

            return catList;

        }

        public List<Booking> AlarmForBooking()
        {
            List<Booking> bookList = new List<Booking>();
            foreach (Booking bk in bkRep.GetAllBookings())
            {
                if (bk._endDate.Ticks < DateTime.Now.Ticks && bk._status == true)
                    bookList.Add(bk);
            }

            return bookList;
        }

        public List<Booking> AlarmForBookingPickup()
        {
            List<Booking> bookList = new List<Booking>();
            foreach (Booking bk in bkRep.GetAllBookings())
            {
                if (bk._startDate.Ticks > DateTime.Now.Ticks && bk._status == false)
                    bookList.Add(bk);
            }

            return bookList;
        }


    }
}
