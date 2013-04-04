using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaverickExample.Models;
using Maverick.Extensions;

namespace MaverickExample.Controllers
{
    public class PersonController : Maverick.Controllers.BaseController<Person>
    {
        private Maverick.Example.Repository.SocialStatusRepository SocialStatusRepository;

        public PersonController ()
        {
            this.Repository = new Maverick.Example.Repository.PersonRepository();
            this.SocialStatusRepository = new Maverick.Example.Repository.SocialStatusRepository();
        }

        public override ActionResult Create ()
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(Person))
                .AddSelectOptions("Status", SocialStatusRepository.All().ToSelectElementList())
                .AllRequired());                
        }

        public override ActionResult EditModel (long ID)
        {
            return View(Maverick.Form.Builder.FormFactory.Create(typeof(Person), Repository.Get(ID))
                .AddSelectOptions("Status", SocialStatusRepository.All().ToSelectElementList())
                .AllRequired());
        }
    }
}