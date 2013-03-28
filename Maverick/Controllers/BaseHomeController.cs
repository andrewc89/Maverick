using System.Web.Mvc;
using Maverick.ViewModels;
using System.Reflection;
using System;

namespace Maverick.Controllers
{
    public class BaseHomeController : Controller
    {
        protected Assembly Assembly;

        public BaseHomeController ()
        {
        }

        public ActionResult Index()
        {
            return View(HomeVM.Create(Assembly));
        }

    }
}
