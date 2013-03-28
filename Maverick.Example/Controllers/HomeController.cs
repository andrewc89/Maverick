using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaverickExample.Controllers
{
    public class HomeController : Maverick.Controllers.BaseHomeController
    {
        public HomeController ()
        {
            this.Assembly = System.Reflection.Assembly.GetAssembly(typeof(MaverickExample.Models.Person));
        }
    }
}