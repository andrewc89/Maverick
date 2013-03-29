using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaverickExample.Models
{
    public class SocialStatus : Maverick.Example.Models.ModelBase<SocialStatus>
    {
        public string Description { get; set; }

        //public List<Person> Members { get; set; }

        public override string ToString ()
        {
            return Description;
        }
    }
}