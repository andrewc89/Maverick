using System.Collections.Generic;
using Maverick.Models;

namespace Maverick.Repository
{
    public interface IRepository<T>
        where T : class, IModelBase, new()
    {
        T Get (long ID);
        bool Add (T Entity);
        bool Update (T Entity);
        bool Delete (long ID);
        IEnumerable<T> All();
    }
}