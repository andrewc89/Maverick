using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Maverick.Models;

namespace Maverick.ViewModels
{
    public class HomeVM
    {
        public HomeVM ()
        {
            Objects = new List<string>();
        }

        public List<string> Objects { get; set; }

        public static HomeVM Create (System.Reflection.Assembly Assembly)
        {
            var Temp = new HomeVM();

            foreach (var Type in Assembly.GetTypes().Where(x => x.IsClass && !x.IsCOMObject && x.IsPublic && x.GetInterface("IModelBase", true) != null).OrderBy(x => x.Name))
            {
                Temp.Objects.Add(Type.Name);
            }

            return Temp;
        }
    }
}