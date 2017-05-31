using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diving_UI.Model
{
    public class SearchBookingList
    {
        private string phone = "";

        public delegate void SearchEventHandler(string phone);

        public event SearchEventHandler Search;

        #region Singleton Region
        private static volatile SearchBookingList instance;

        public static SearchBookingList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchBookingList();
                }
                return instance;
            }
        }
        #endregion

        public void SearchInBooking(string phone)
        {
            OnSearchChanged(phone);
        }

        public string GetPhone()
        {
            return phone;
        }

        protected virtual void OnSearchChanged(string id)
        {
            Search?.Invoke(id);
            phone = id;
        }
    }
}
