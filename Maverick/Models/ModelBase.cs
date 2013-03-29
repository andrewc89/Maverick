using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maverick.Repository;

namespace Maverick.Models
{
    public abstract class ModelBase <T> : IModelBase
        where T: class, IModelBase, new()
    {
        public long ID { get; set; }
        
        public abstract void Save ();
    }
}