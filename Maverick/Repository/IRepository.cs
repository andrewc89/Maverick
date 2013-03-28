using System.Collections.Generic;
using Maverick.Models;

namespace Maverick.Repository
{
    public interface IRepository<T> where T : class, IModelBase
    {
        T Get (long Id);
        bool Add (T Entity);
        bool Update (T Entity);
        bool Delete (long Id);
        IEnumerable<T> All();
    }
}