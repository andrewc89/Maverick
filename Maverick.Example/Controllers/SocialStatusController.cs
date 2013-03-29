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
            this.Repository = new Maverick.Example.Repository.Repository<SocialStatus>();
        }

        public override ActionResult Create ()
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(SocialStatus)));
        }

        public override ActionResult EditModel (long ID)
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(SocialStatus), Repository.Get(ID)));
        }
    }
}
