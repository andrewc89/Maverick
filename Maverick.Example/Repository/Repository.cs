using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;
using Maverick.Example.Database;
using Maverick.Models;

namespace Maverick.Example.Repository
{
    public class Repository<T> : Maverick.Repository.IRepository<T>
        where T: class, IModelBase, new()
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



        //private MockDb Db;

        //public Repository ()
        //{
        //    Db = new MockDb();
        //}

        //public T Get (long ID)
        //{
        //    if (typeof(T).Name == "Person")
        //    {
        //        return Db.Persons[ID] as T;
        //    }
        //    else if (typeof(T).Name == "SocialStatus")
        //    {
        //        return Db.SocialStatuses[ID] as T;
        //    }
        //    else return null;
        //}

        //public bool Add (T Entity)
        //{
        //    return false;
        //}

        //public bool Update (T Entity)
        //{
        //    return false;
        //}

        //public bool Delete (long ID)
        //{
        //    if (typeof(T).Name == "Person")
        //    {
        //        return Db.Persons.Remove(ID);
        //    }
        //    else if (typeof(T).Name == "SocialStatus")
        //    {
        //        return Db.SocialStatuses.Remove(ID);
        //    }
        //    else return false;
        //}

        //public IEnumerable<T> All ()
        //{
        //    if (typeof(T).Name == "Person")
        //    {
        //        return Db.Persons.Values.ToList() as List<T>;
        //    }
        //    else if (typeof(T).Name == "SocialStatus")
        //    {
        //        return Db.SocialStatuses.Values.ToList() as List<T>;
        //    }
        //    else return new List<T>();
        //}
    }
}