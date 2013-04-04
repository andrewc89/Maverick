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
    }
}