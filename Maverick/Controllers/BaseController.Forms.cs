using System.Linq;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using Maverick.Models;
using Maverick.Repository;
using Maverick.ViewModels;
using Maverick.Form.Builder;
using Maverick.Form.ModelBinder;
using Maverick.Extensions;
using Maverick.Helpers.Filter;
using System;
using System.Web;
using System.Web.Mvc.Html;

namespace Maverick.Controllers
{
    public partial class BaseController<T> : Controller where T : ModelBase<T>, new()
    {
        #region Repository

        /// <summary>
        /// Repository of type T for controller
        /// </summary>
        protected IRepository<T> Repository;

        /// <summary>
        /// instantiates new Repository of type T
        /// </summary>
        public BaseController ()
            : base()
        {            
        }

        /// <summary>
        /// allows injection of repository into controller
        /// </summary>
        /// <param name="Repository">IRepository</param>
        public BaseController (IRepository<T> Repository)
            : base()
        {
            if (Repository == null)
            {
                throw new ArgumentException("Repository");
            }
            this.Repository = Repository;
        }

        #endregion

        #region List

        /// <summary>
        /// List all (filtered) models of type T 
        /// as ID, ToString() pairs 
        /// </summary>
        /// <param name="Format">.json format returns list in JSON</param>
        /// <returns>/{Object}/List or list as JSON</returns>
        [HttpGet]
        public ActionResult List (string Format = "")
        {
            ViewData["Type"] = typeof(T).Name;
            var VM = new List<ListVM>();
            var Filter = new ListFilter(Request.QueryString, typeof(T));
            foreach (var Item in Repository.All())
            {
                if (Filter.CheckItem(Item))
                {
                    VM.Add(new ListVM(Item.ID, Item.ToString()));
                }
            }
            VM = VM.OrderBy(x => x.DisplayName).ToList();
            if (!string.IsNullOrEmpty(Format) && Format.ToLower().Equals("json"))
            {
                return Json(VM, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(VM);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Form to create new model of type T. 
        /// Should be overridden in inheriting classes
        /// </summary>
        /// <returns>/{Object}/Create</returns>
        [HttpGet]
        public virtual ActionResult Create ()
        {
            return View(FormFactory.Create(typeof(T)));
        }

        /// <summary>
        /// Action to which /{Object}/Create POSTs. 
        /// Uses custom ModelBinder to bind form values to object of type T. 
        /// Adds Entity to repository
        /// </summary>
        /// <param name="Entity">T object created by ModelBinder</param>
        /// <returns>/{Object}/List</returns>
        [HttpPost]
        public ActionResult Create ([ModelBinder(typeof(ModelBinder))]T Entity)
        {
            Repository.Add(Entity);            
            return RedirectToAction("List");
        }

        #endregion        

        #region Edit

        [HttpGet]
        public virtual ActionResult Edit (long ID, string Property, long? PropertyID)
        {
            if (!string.IsNullOrEmpty(Property))
            {
                return EditProperty(ID, Property, PropertyID);
            }
            else
            {
                ViewData["Type"] = typeof(T).Name;
                return EditModel(ID);
            }
        }

        public virtual ActionResult EditModel (long ID)
        {
            return View("Edit", FormFactory.Create(typeof(T), Repository.Get(ID)));
        }

        internal ActionResult EditProperty (long Id, string Property, long? PropertyId)
        {
            var Model = Repository.Get(Id);
            var PropertyInfo = typeof(T).GetProperty(Property);
            var Instance = PropertyInfo.GetValue(Model, null);
            var InstanceType = Instance.GetType();
            if (InstanceType.IsIEnumerable())
            {
                return EditList(Instance, PropertyId);
            }
            else
            {
                ViewData["Type"] = PropertyInfo.PropertyType.Name;
                return RedirectToAction("Edit", PropertyInfo.PropertyType.Name, new { ID = InstanceType.GetProperty("ID").GetValue(Instance, null) });
            }
        }

        internal ActionResult EditList (object Instance, long? PropertyID)
        {
            var List = new List<ListVM>();
            Instance = (IEnumerable)Instance;
            var ItemType = Instance.GetType().GetGenericArguments()[0];
            if (PropertyID != null)
            {
                var InstanceList = ((IEnumerable)Instance).Cast<object>();
                object ReturnItem = null;
                var IdProperty = ItemType.GetProperty("ID");
                foreach (var Item in InstanceList)
                {
                    var ID = IdProperty.GetValue(Item, null);
                    if ((long)ID == (long)PropertyID)
                    {
                        ReturnItem = Item;
                    }
                }
                ViewData["Type"] = ItemType.Name;
                return View("Edit", FormFactory.Create(ItemType, ReturnItem));
            }
            else
            {
                foreach (var Item in (IEnumerable)Instance)
                {
                    List.Add(new ListVM { Id = (long)ItemType.GetProperty("ID").GetValue(Item, null), DisplayName = Item.ToString() });
                }
                ViewData["Type"] = ItemType.Name;
                return View("List", List);
            }
        }

        /// <summary>
        /// Action to which /{Object}/Edit/{Id} POSTs. 
        /// Uses custom ModelBinder to bind form values to object of type T. 
        /// Updates Entity in repository
        /// </summary>
        /// <param name="Entity">T object created by ModelBinder</param>
        /// <param name="ID">Id of entity</param>
        /// <returns>/{Object}/Edit/{Id}</returns>
        [HttpPost]
        public virtual ActionResult Edit ([ModelBinder(typeof(ModelBinder))]T Entity, long ID)
        {
            Entity.ID = ID;
            Repository.Update(Entity);
            return RedirectToAction("Edit", typeof(T).Name, new { ID = ID });
        }

        #endregion
    }
}