using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaverickExample.Models;

namespace MaverickExample.Controllers
{
    public class SocialStatusController : Maverick.Controllers.BaseController<SocialStatus>
    {
        public SocialStatusController ()
        {
            this.Repository = new MaverickExample.Repository.SocialStatusRepository();
        }

        public override ActionResult Create ()
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(SocialStatus)));
        }

        public override ActionResult EditModel (long Id)
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(SocialStatus), Repository.Get(Id)));
        }
    }
}
