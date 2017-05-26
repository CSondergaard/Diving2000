using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Logic.Repository;

namespace Logic.Data
{
    class BookingData
    {

        BookingRepo bookingRep = new BookingRepo();
        EquipmentRepo EqRep = new EquipmentRepo();
        dbConn db = new dbConn();

        public void Add(Booking obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Booking (StartDate, EndDate, Phone, Status VALUES (@startdate, @enddate, @phone, @status)");
            cmd.Parameters.AddWithValue("@startdate", obj._startDate);
            cmd.Parameters.AddWithValue("@enddate", obj._endDate);
            cmd.Parameters.AddWithValue("@phone", obj._phone);
            cmd.Parameters.AddWithValue("@status", obj._status);

            int id = db.InsertDataGetNewID(cmd);

            foreach (Equipment eq in obj._equipment)
            {
                MySqlCommand cmdEq = new MySqlCommand("INSERT INTO BookingList (EquipmentId, BookingId) VALUES (@eqid, @bookingid)");
                cmdEq.Parameters.AddWithValue("@eqid", eq._id);
                cmdEq.Parameters.AddWithValue("@bookingid", id);

                db.ModifyData(cmdEq);
            }

        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Booking WHERE Id = @id");
            cmd.Parameters.AddWithValue("@id", id);
            db.ModifyData(cmd);

            MySqlCommand cmdEq = new MySqlCommand("DELETE FROM BookingList WHERE BookingId = @bookingid");
            cmdEq.Parameters.AddWithValue("@bookingid", id);

        }


        public void GetAll()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Id, StartDate, EndDate, Phone, Status FROM Booking");
            DataTable dtbook = db.GetData(cmd);

            foreach (DataRow rw in dtbook.Rows)
            {

                List<Equipment> eqList = GetAllEquipmentsForBooking(Convert.ToInt32(rw["Id"]));

                bookingRep.Add(new Booking(
                       Convert.ToInt32(rw["Id"]),
                       eqList,
                       Convert.ToDateTime(rw["StartDate"]),
                       Convert.ToDateTime(rw["EndDate"]),
                       rw["Phone"].ToString(),
                       Convert.ToBoolean(rw["Status"])
                       ));


            }
           
        }

        public List<Equipment> GetAllEquipmentsForBooking(int id)
        {
            List<Equipment> eqList = new List<Equipment>();

            MySqlCommand cmdEqId = new MySqlCommand("SELECT EquipmentId, BookingId FROM BookingList WHERE BookingId = @bookingid");
            cmdEqId.Parameters.AddWithValue("@bookingid", id);

            DataTable dtEqId = db.GetData(cmdEqId);

            foreach (DataRow eq in dtEqId.Rows)
            {
                eqList.Add(EqRep.GetById(Convert.ToInt32(eq["Id"])));
            }

            return eqList;

        }




    }
}
