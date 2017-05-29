using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository
{
    public class BookingRepo : IRepo<Booking>
    {

        private static List<Booking> BookingList = new List<Booking>();

        public void Add(Booking obj)
        {
            BookingList.Add(obj);
        }

        public void AddEquipmentToBooking(Equipment eq, Booking bk)
        {
            bk._equipment.Add(eq);
            Edit(bk);
        }

        public void DeleteById(int id)
        {
            Booking book = GetById(id);

            BookingList.Remove(book);
        }

        public void DeleteEquipmentFromBooking(Equipment eq, Booking bk)
        {
            Equipment equip = bk._equipment.SingleOrDefault(x => x._id == eq._id);
            if (equip != null)
            {
                bk._equipment.Remove(equip);
            }
            Edit(bk);
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

        public Booking GetByPhone(string phone)
        {
            return BookingList.Find(x => x._phone == phone);
        }

        public List<Booking> GetAllBookings()
        {
            return BookingList;
        }

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
            return BookingList.Where(x => x._startDate.Ticks <= endDate.Ticks && x._endDate.Ticks >= startDate.Ticks).ToArray();

        }

        public DateTime? GetRentDateForEquipment(Equipment eq)
        {
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
