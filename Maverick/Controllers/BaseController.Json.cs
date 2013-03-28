using System.Collections.Generic;
using System.Web.Mvc;
using Maverick.Extensions;
using System.Collections;
using Maverick.Attributes;
using System;
using System.Reflection;
using Maverick.Models;
using System.Linq;
using System.Diagnostics;
using Maverick.Helpers;

namespace Maverick.Controllers
{
    public partial class BaseController<T> : Controller where T : ModelBase<T>, new()
    {
        #region Index

        /// <summary>
        ///  
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Property"></param>
        /// <param name="PropertyID"></param>
        /// <param name="Format"></param>
        /// <param name="HttpVerb"></param>
        /// <returns></returns>
        [RestHttpVerbFilter]
        public ActionResult Index (long? ID, string Property, long? PropertyID, string Format, string HttpVerb)
        {            
            switch (HttpVerb)
            {
                case "GET":
                    return Json(Get(ID.Value, Property, PropertyID), JsonRequestBehavior.AllowGet);                    
                case "POST":
                    return Json(Post(ID, Property, PropertyID));
                case "PUT":
                    return Json(Put(ID.Value, Property, PropertyID));
                case "DELETE":
                    return Json(Delete(ID, Property, PropertyID));
            }
            return Json(new { error = "Unknown HTTP verb" });
        }

        #endregion

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Property"></param>
        /// <param name="PropertyID"></param>
        /// <returns></returns>
        internal object Get (long ID, string Property = "", long? PropertyID = null)
        {
            var Model = Repository.Get(ID);
            if (string.IsNullOrEmpty(Property) && !PropertyID.HasValue)
            {
                return ViewModel.Construct(Model, typeof(T));
            }
            else if (!string.IsNullOrEmpty(Property))
            {
                return GetProperty(Model, Property, PropertyID);
            }
            return new { error = "Could not get specified object" };
        }

        private object GetProperty (T Model, string Property, long? PropertyId)
        {
            var PropertyInfo = typeof(T).GetProperty(Property);
            var PropertyType = PropertyInfo.PropertyType;
            var PropertyInstance = PropertyInfo.GetValue(Model, null);
            return !PropertyType.IsIEnumerable()
                ? ViewModel.Construct(PropertyInstance, PropertyType)
                : (PropertyId.HasValue)
                    ? Reflectively.GetListItem((IEnumerable)PropertyInstance, PropertyId.Value)
                    : ViewModel.ConstructList((IEnumerable)PropertyInstance, Request.QueryString);
        }

        #endregion

        #region Post

        /// <summary>
        /// If POSTed to /{Object}, adds new T Model to repository. 
        /// If POSTed to /{Object}/{Id}/{Property} and specified property is a List<IModelBase>, adds new Model to List
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Property"></param>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        internal object Post (long? ID, string Property, long? PropertyID)
        {
            var Dict = Deserializer.Deserialize(Request);
            if (Dict.Keys.Count == 0)
            {
                return new { error = "Model is empty" };
            }
            if (!ID.HasValue && string.IsNullOrEmpty(Property) && !PropertyID.HasValue)
            {
                var Model = ViewModel.Deconstruct(Dict, typeof(T));
                if (Repository.Add((T)Model))
                {
                    return Get(((T)Model).ID);
                }
            }
            if (ID.HasValue && !string.IsNullOrEmpty(Property) && !PropertyID.HasValue)
            {
                var Model = Repository.Get(ID.Value);
                if (Reflectively.AddToList(Model, Property, Dict))
                {
                    return Get(ID.Value, Property);
                }
                return new { error = "Unable to add new item to " + Property };
            }
            return new { error = "Unable to save new " + typeof(T).Name };
        }

        #endregion

        #region Put

        /// <summary>
        /// If an Id is specified (and nothing else), the corresponding T Model is updated 
        /// with PUT JSON data in the repository
        /// </summary>
        /// <param name="ID">Id of Model to update</param>
        /// <param name="Property">should be null or ""</param>
        /// <param name="PropertyID">should be null</param>
        /// <returns>updated Model as JSON. If PUT to incorrect URL or JSON data is empty, returns error</returns>
        internal object Put (long? ID, string Property, long? PropertyID)
        {
            if (ID.HasValue && string.IsNullOrEmpty(Property) && !PropertyID.HasValue)
            {
                var Dict = Deserializer.Deserialize(Request);
                if (Dict.Keys.Count == 0)
                {
                    return new { error = "Model is empty" };
                }
                var Model = ViewModel.Deconstruct(Dict, typeof(T));
                ((T)Model).ID = ID.Value;
                if (Repository.Update((T)Model))
                {
                    return Get(ID.Value);
                }
            }
            return new { error = "Unable to update " + typeof(T).Name };
        }

        #endregion

        #region Delete

        /// <summary>
        /// If an Id is specified (and nothing else), the corresponding T Model is deleted from the repository
        /// </summary>
        /// <param name="ID">Id of Model to be deleted</param>
        /// <param name="Property">should be null or ""</param>
        /// <param name="PropertyID">should be null</param>
        /// <returns>whether or not the Model was deleted. If DELETEd to incorrect URL, returns error</returns>
        internal object Delete (long? ID, string Property, long? PropertyID)
        {
            if (ID.HasValue && string.IsNullOrEmpty(Property) && !PropertyID.HasValue)
            {
                return Repository.Delete(ID.Value);
            }
            return new { error = "Unable to delete " + typeof(T).Name };
        }

        #endregion
    }
}