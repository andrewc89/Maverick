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
        /// adds an item, represented by the Dict VM, to a list, if it exists,
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

        internal static object GetPropertyInstance (IModelBase Model, string PropertyName)
        {
            return Model.GetType().GetProperty(PropertyName).GetValue(Model, null);            
        }

        internal static Type GetPropertyType (IModelBase Model, string PropertyName)
        {
            return Model.GetType().GetProperty(PropertyName).PropertyType;
        }

        internal static object GetListItem (IEnumerable List, long Id)
        {
            var ItemType = List.GetType().GetGenericArguments()[0];
            var ItemId = ItemType.GetProperty("ID");
            foreach (var Item in (IEnumerable)List)
            {
                if ((long)ItemId.GetValue(Item, null) == Id)
                {
                    return ViewModel.Construct(Item, ItemType);
                }
            }
            return new { error = "Item not found in list" };            
        }
    }
}