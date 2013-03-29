using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maverick.Repository;
using Maverick.Models;

namespace Maverick.Example.Models
{
    public abstract class ModelBase<T> : Maverick.Models.ModelBase<T>
        where T: class, IModelBase, new()
    {
        public static IRepository<T> Repository = new Maverick.Example.Repository.Repository<T>();

        public override void Save ()
        {            
            Repository.Update(this as T);
        }

        public static T Load (long ID)
        {
            return Repository.Get(ID);
        }
    }
}