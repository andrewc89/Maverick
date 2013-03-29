using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Maverick.Models;
using System.Collections;
using Maverick.Extensions;
using System.Web.Mvc;
using Maverick.Models;

namespace Maverick.Helpers
{
    internal static class Reflectively
    {
        /// <summary>
        /// Adds an item, represented by the Dict VM, to a list, if it exists,
        /// which is a property of the given Model
        /// </summary>
        /// <param name="Model">Model (T from controller), which has a property which is a list of some other IModelBase</param>
        /// <param name="PropertyName">the name of the property which is the list</param>
        /// <param name="Dict">VM representation of the item to be added to the list</param>
        /// <returns>whether or not the property is a list AND the item was added to said list</returns>
        internal static bool AddToList (IModelBase Model, string PropertyName, Dictionary<string, string> Dict)
        {
            var ModelType = Model.GetType();
            var Property = ModelType.GetProperty(PropertyName);
            var PropertyType = Property.PropertyType;
            if (PropertyType.IsIEnumerable() && PropertyType.GetGenericArguments()[0] != null)
            {
                var ItemType = PropertyType.GetGenericArguments()[0];
                var Item = ViewModel.Deconstruct(Dict, ItemType);
                ((IModelBase)Item).Save();                
                var List = Property.GetValue(Model, null);
                ((IList)List).Add(Item);
                Model.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Given an IEnumerable, retrieves the entry, if it exists, specified by ID
        /// </summary>
        /// <param name="List">IEnumerable from which to retrieve item</param>
        /// <param name="ID">ID of item to retrieve</param>
        /// <returns>Item from IEnumerable specified by ID</returns>
        internal static object GetListItem (IEnumerable List, long ID)
        {
            var ItemType = List.GetType().GetGenericArguments()[0];
            var ItemId = ItemType.GetProperty("ID");
            foreach (var Item in (IEnumerable)List)
            {
                if ((long)ItemId.GetValue(Item, null) == ID)
                {
                    return ViewModel.Construct(Item, ItemType);
                }
            }
            return new { error = "Item not found in list" };            
        }
    }
}