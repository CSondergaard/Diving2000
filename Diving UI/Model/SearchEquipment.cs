using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diving_UI.Model
{
    public class SearchEquipment
    {
        public delegate void ChangedSearchEventHandler(List<Equipment> list);

        public event ChangedSearchEventHandler SearchChanged;

        #region Singleton Region
        private static volatile SearchEquipment instance;

        public static SearchEquipment Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SearchEquipment();
                }
                return instance;
            }
        }
        #endregion

        public void Search(List<Equipment> list)
        {
            OnSearchChanged(list);
        }

        protected virtual void OnSearchChanged(List<Equipment> list)
        {
            SearchChanged?.Invoke(list);
        }

    }
}
