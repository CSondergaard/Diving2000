using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    public interface IRepo<T> where T : class
    {
        void Add(T obj);
        void Edit(T obj);
        void DeleteById(int id);
        T GetById(int id);

    }
}
