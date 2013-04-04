using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maverick.Example.Repository;

namespace MaverickExample.Models
{
    public class SocialStatus : Maverick.Example.Models.ModelBase<SocialStatus>
    {
        public string Description { get; set; }

        public List<Person> Members { get; set; }

        public override string ToString ()
        {
            return Description;
        }

        public override void Save ()
        {
            throw new NotImplementedException();
        }

        public static SocialStatus Load (long ID)
        {
            return new SocialStatusRepository().Get(ID);
        }
    }
}