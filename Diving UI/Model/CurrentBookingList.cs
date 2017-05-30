using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diving_UI.Model
{
    public class CurrentBookingList
    {
        private static List<Equipment> eqlist = new List<Equipment>();

        private DateTime? StartDate;
        private DateTime? EndDate;

        public delegate void ItemAddedToListEventHandler();

        public event ItemAddedToListEventHandler ItemAdded;

        #region Singleton Region
        private static volatile CurrentBookingList instance;

        public static CurrentBookingList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentBookingList();
                }
                return instance;
            }
        }
        #endregion

        public List<Equipment> GetCurrentItems()
        {
            return eqlist;
        }

        public void AddItemToBookingList(Equipment eq)
        {
            eqlist.Add(eq);
            OnItemAdded();
        }

        public DateTime? GetStartdate()
        {
            return StartDate;
        }

        public DateTime? GetEnddate()
        {
            return EndDate;
        }

        public void SetDates(DateTime startdate, DateTime enddate)
        {
            StartDate = startdate;
            EndDate = enddate;
        }


        public void ClearBookingList()
        {
            eqlist.Clear();
        }

        public void RemoveEquipmentFromList(Equipment eq)
        {
            eqlist.Remove(eq);
        }

        public int CountBookingList()
        {
            return eqlist.Count();
        }

        protected virtual void OnItemAdded()
        {
            ItemAdded?.Invoke();
        }



    }
}
