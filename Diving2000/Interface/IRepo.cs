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
        void AddToRepo(T obj);
        void Save(T obj);
        void SaveRepo(T obj);
        void DeleteById(int id);
        void DeleteByIdRepo(int id);
        T GetById(int id);





    }
}
