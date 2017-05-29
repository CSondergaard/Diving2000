﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;

namespace Diving_UI.Model
{
    class EquipmentSearch
    {

        EquipmentRepo eqRepo = new EquipmentRepo();

        public List<Equipment> GetEquipmentsFromCategory(int CatId, List<Equipment> eq)
        {
            List<Equipment> eqlist = new List<Equipment>();
            foreach (Equipment item in eq)
            {
                if (item._catId == CatId)
                    eqlist.Add(item);
            }

            return eqlist;
        }

        public List<Equipment> GetAllAviableEquipmentsFromCategory(int CatId, DateTime date, List<Equipment> eq)
        {

            BookingSearch bookSearch = new BookingSearch();

            List<Equipment> booklist = GetEquipmentsFromCategory(CatId, eq);

            List<Equipment> EquipmentList = eqRepo.GetAllEquipments();

            booklist = bookSearch.GetEquipmentsBookedAtTime(date, EquipmentList);

            return booklist;


        }

        public List<Equipment> GetAllAviableEquipments(DateTime date)
        {
            BookingSearch bookSearch = new BookingSearch();
            List<Equipment> EquipmentList = eqRepo.GetAllEquipments();

            List<Equipment> booklist = bookSearch.GetEquipmentsBookedAtTime(date, EquipmentList);

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
