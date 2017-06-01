using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diving_UI.Model
{
    public class Edit
    {
        private int eqid;

        private int catid;

        public delegate void AlarmUpdateEventHandler();
        public event AlarmUpdateEventHandler RunAlarm;

        #region Singleton Region
        private static volatile Edit instance;

        public static Edit Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Edit();
                }
                return instance;
            }
        }
        #endregion

        public void runAlarm()
        {
            OnAlarmRun();
        }

        protected virtual void OnAlarmRun()
        {
            RunAlarm?.Invoke();
        }

        public void SetEqId(int id)
        {
            eqid = id;
        }

        public int GetEqId()
        {
            return eqid;
        }

        public void SetCatId(int id)
        {
            catid = id;
        }

        public int GetCatId()
        {
            return catid;
        }

    }
}
