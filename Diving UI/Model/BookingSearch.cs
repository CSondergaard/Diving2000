using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;

namespace Diving_UI.Model
{
    class BookingSearch
    {

        BookingRepo bookrep = new BookingRepo();

        public List<Equipment> GetEquipmentsBookedAtTime(DateTime date, List<Equipment> equiplist)
        {
            Booking[] bookings = GetRentedEquipmentFromDate(date);
            List<Equipment> eq = equiplist;

            foreach (Booking item in bookings)
            {
                foreach (Equipment equip in item._equipment)
                {
                    bool exist = eq.Any(x => x._id == equip._id);
                    if (exist)
                        eq.Remove(equip);
                }

            }

            return eq;
        }

        public Booking[] GetRentedEquipmentFromDate(DateTime date)
        {
            List<Booking> BookingList = bookrep.GetAllBookings();
            return BookingList.Where(x => x._startDate.Ticks < date.Ticks && x._endDate.Ticks > date.Ticks).ToArray();

        }

        public List<Equipment> GetEquipmentsNotRentedBetweenDates(DateTime startdate, DateTime endDate, List<Equipment> eqlist)
        {
            Booking[] bookings = GetRentedEquipmentsBetweenDates(startdate, endDate);
            List<Equipment> eq = eqlist;

            foreach (Booking item in bookings)
            {
                foreach (Equipment equip in item._equipment)
                {
                    bool exist = eq.Any(x => x._id == equip._id);
                    if (exist)
                        eq.Remove(equip);
                }

            }
            return eq;
        }

        private Booking[] GetRentedEquipmentsBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<Booking> BookingList = bookrep.GetAllBookings();
            return BookingList.Where(x => x._startDate.Ticks <= endDate.Ticks && x._endDate.Ticks >= startDate.Ticks).ToArray();
        }

        public DateTime? GetRentDateForEquipment(Equipment eq)
        {
            List<Booking> BookingList = bookrep.GetAllBookings();
            foreach (Booking item in BookingList)
            {
                foreach (Equipment equip in item._equipment)
                {
                    if (equip._id == eq._id)
                    {
                        return item._endDate;
                    }
                }
            }
            return null;
        }
    }
}
