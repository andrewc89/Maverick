using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaverickExample.Models;
using Maverick.Extensions;
using MaverickExample.Repository;

namespace MaverickExample.Controllers
{
    public class PersonController : Maverick.Controllers.BaseController<Person>
    {
        public PersonController ()
        {
            this.Repository = new MaverickExample.Repository.PersonRepository();
        }

        public override ActionResult Create ()
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(Person))
                .AddSelectOptions("Status", new SocialStatusRepository().All().ToSelectElementList())
                .AllRequired());                
        }

        public override ActionResult EditModel (long Id)
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(Person), Repository.Get(Id))
                .AddSelectOptions("Status", new SocialStatusRepository().All().ToSelectElementList())
                .AllRequired());
        }
    }
}