using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaverickExample.Models
{
    public class SocialStatus : Maverick.Models.ModelBase<SocialStatus>
    {
        public string Description { get; set; }

        public override void Save ()
        {
            throw new NotImplementedException();
        }

        public override string ToString ()
        {
            return Description;
        }
    }
}