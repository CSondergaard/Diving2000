﻿using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diving_UI.Model
{
    public class SearchEquipment
    {

        private List<Equipment> eqlist = new List<Equipment>();

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

        public List<Equipment> GetList()
        {
            return eqlist;
        }

        protected virtual void OnSearchChanged(List<Equipment> list)
        {
            if (SearchChanged != null)
            {
                SearchChanged(list);
            }
        }



    }
}
