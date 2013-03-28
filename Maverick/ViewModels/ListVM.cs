using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maverick.ViewModels
{
    public class ListVM
    {
        public ListVM () { }

        public ListVM (long Id, string DisplayName)
        {
            this.Id = Id;
            this.DisplayName = DisplayName;
        }

        public long Id { get; set; }

        public string DisplayName { get; set; }
    }
}