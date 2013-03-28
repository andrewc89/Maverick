using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maverick.Models
{
    public abstract class ModelBase <T> : IModelBase
        where T: new()
    {
        public long ID { get; set; }
        
        public abstract void Save ();
    }
}