using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;

namespace MaverickExample.Repository
{
    public class Repository<T> : Maverick.Repository.IRepository<T>
        where T: Maverick.Models.ModelBase<T>, new()
    {
        public T Get (long ID)
        {
            throw new NotImplementedException();
        }

        public bool Add (T Entity)
        {
            throw new NotImplementedException();
        }

        public bool Update (T Entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete (long ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> All ()
        {
            throw new NotImplementedException();
        }
    }
}