using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaverickExample.Models
{
    public class Person : Maverick.Example.Models.ModelBase<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SocialStatus Status { get; set; }

        public override string ToString ()
        {
            return FirstName + " " + LastName;
        }
    }
}