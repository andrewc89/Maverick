using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Specialized;
using Maverick.Helpers.Filter;

namespace Maverick.Helpers
{
    public static class ViewModel
    {
        #region Properties

        /// <summary>
        /// Domain to construct URLs for properties
        /// </summary>
        private static string Domain = "http://localhost:51310/";

        #endregion

        #region Construct

        /// <summary>
        /// Constructs a view model from an object with a specified type
        /// </summary>
        /// <param name="Model">object to convert to view model</param>
        /// <param name="ModelType">type of object to convert (used for reflection)</param>
        /// <returns>view model representation of object as Dictionary<string, string></returns>
        public static Dictionary<string,string> Construct (object Model, Type ModelType)
        {
            var ViewModel = new Dictionary<string, string>();
            long ID = (long)Model.GetType().GetProperty("ID").GetValue(Model, null);

            foreach (var Property in ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var PropertyType = Property.PropertyType;
                var Instance = Property.GetValue(Model, null);               

                // if property value is null
                if (Instance == null)
                {
                    ViewModel.Add(Property.Name, "null");
                }
                else
                {
                    var InstanceType = Instance.GetType();

                    // if custom model
                    if (!PropertyType.FullName.StartsWith("System"))
                    {
                        var PropertyID = PropertyType.GetProperty("ID").GetValue(Instance, null);
                        ViewModel.Add(Property.Name, Domain + PropertyType.Name + "/" + PropertyID.ToString() + ".json");
                    }
                    // if IEnumerable
                    else if (InstanceType != typeof(string) && (InstanceType == typeof(IEnumerable) || InstanceType.GetInterfaces().Any(x => x == typeof(IEnumerable))))
                    {
                        ViewModel.Add(Property.Name, Domain + ModelType.Name + "/" + ID + "/" + Property.Name + ".json");
                    }
                    // primitive
                    else
                    {
                        ViewModel.Add(Property.Name, Property.GetValue(Model, null).ToString());
                    }
                }
            }
            return ViewModel;
        }

        #endregion

        #region ConstructList

        /// <summary>
        /// Constructs a view model for an IEnumerable model. 
        /// Filters list by Params from Request.QueryString
        /// </summary>
        /// <param name="Model">IEnumerable model to convert</param>
        /// <param name="Params">Query args from Request.QueryString</param>
        /// <returns>view model representation of IEnumerable model as List<Dictionary<string, string>></returns>
        public static List<Dictionary<string, string>> ConstructList (IEnumerable Model, NameValueCollection Params)
        {
            var ListVM = new List<Dictionary<string, string>>();
            var ItemType = Model.GetType().GetGenericArguments()[0];
            var Filter = new ListFilter(Params, ItemType);
            if (!Filter.Valid)
            {
                return new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string> 
                    {
                        
                    }
                };
            }            
            foreach (var Item in Model)
            {
                var ItemVM = ViewModel.Construct(Item, ItemType);
                if (Filter.CheckItem(ItemVM))
                {
                    ListVM.Add(ItemVM);
                }
            }
            return ListVM;
        }

        #endregion

        #region Deconstruct

        /// <summary>
        /// Deconstructs a view model, returns object of type specified by ModelType
        /// </summary>
        /// <param name="ViewModel">view model constructed with ViewModel.Construct or Serializer.Serialize</param>
        /// <param name="ModelType">type of object to which to deconstruct</param>
        /// <returns>object of type ModelType</returns>
        public static object Deconstruct (Dictionary<string, string> ViewModel, Type ModelType)
        {
            var Instance = Activator.CreateInstance(ModelType);
            foreach (var Property in ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var PropertyType = Property.PropertyType;
                if (ViewModel.ContainsKey(Property.Name))
                {
                    Property.SetValue(Instance, Convert.ChangeType(ViewModel[Property.Name], PropertyType), null);
                }
            }
            return Instance;
        }

        #endregion
    }
}