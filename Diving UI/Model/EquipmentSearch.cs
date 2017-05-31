using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;

namespace Diving_UI.Model
{
    public class EquipmentSearch
    {

        EquipmentRepo eqRepo = new EquipmentRepo();

        public List<Equipment> GetAllAviableEquipmentsFromCategory(int CatId, DateTime date, List<Equipment> eq)
        {

            BookingSearch bookSearch = new BookingSearch();

            List<Equipment> booklist = eqRepo.GetEquipmentsFromCategory(CatId, eq);

            List<Equipment> EquipmentList = eqRepo.GetAllEquipments();

            booklist = bookSearch.GetEquipmentsNotBookedAtTime(date, EquipmentList);

            return booklist;


        }

        public List<Equipment> GetAllAviableEquipments(DateTime date)
        {
            BookingSearch bookSearch = new BookingSearch();
            List<Equipment> EquipmentList = eqRepo.GetAllEquipments();

            List<Equipment> booklist = bookSearch.GetEquipmentsNotBookedAtTime(date, EquipmentList);

            return booklist;
        }


        public List<Equipment> SearchEquipment(Dictionary<string, string> dic, List<Equipment> eq)
        {
            List<Equipment> eqlist = eq;
            Equipment[] eqarr = null;

            foreach (KeyValuePair<string, string> item in dic)
            {
                eqarr = eqlist.Where(x => x._values.ContainsKey(item.Key) && x._values.ContainsValue(item.Value)).ToArray();
                eqlist.Clear();
                foreach (Equipment items in eqarr)
                {
                    eqlist.Add(items);
                }
            }

            return eqlist;
        }
    }
}
