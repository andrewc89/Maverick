using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using Maverick.Models;
using System.Reflection;

namespace Maverick.Helpers.Filter
{
    internal class ListFilter
    {
        #region Constructor

        internal ListFilter (NameValueCollection Params, Type ModelType)
        {
            this.Params = Params;
            this.ModelType = ModelType;
            Valid = true;
            GetProperties();
        }

        #endregion

        #region Properties

        /// <summary>
        /// parameters from query string
        /// </summary>
        private NameValueCollection Params;

        /// <summary>
        /// Type of model, the list of which is being filtered
        /// </summary>
        private Type ModelType;

        /// <summary>
        /// FilterProperty encapsulates the value the filter by
        /// and the corresponding PropertyInfo for all of the 
        /// properties found in Params
        /// </summary>
        private List<FilterProperty> Properties;
        
        /// <summary>
        /// invalid properties found in the query string causes Valid == false
        /// </summary>
        internal bool Valid { get; set; }

        #endregion

        #region Internal

        /// <summary>
        /// check IModelBase item against specified property values
        /// </summary>
        /// <param name="Model">IModelBase item from repository</param>
        /// <returns>whether or not item made it through filter</returns>
        internal bool CheckItem (IModelBase Model)
        {
            foreach (var Property in Properties)
            {
                if (Property.PropertyInfo.GetValue(Model, null).ToString() != Property.Value)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// check Dictionary<string, string> VM item against specified property values
        /// </summary>
        /// <param name="Model">VM item from ViewModel helper class</param>
        /// <returns>whether or not item made it through filter</returns>
        internal bool CheckItem (Dictionary<string, string> Item)
        {        
            foreach (var Param in Params.AllKeys)
            {
                if (Item[Param].ToLowerInvariant() != Params[Param].ToLowerInvariant())
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Private

        /// <summary>
        /// get properties from Params, make sure they are valid, load their PropertyInfos
        /// </summary>
        private void GetProperties ()
        {
            var Properties = ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            this.Properties = new List<FilterProperty>();
            foreach (var Param in Params.AllKeys)
            {
                if (!Properties.Exists(x => x.Name == Param))
                {
                    Valid = false;
                    break;
                }
                this.Properties.Add(new FilterProperty
                {
                    PropertyInfo = Properties.Find(x => x.Name == Param),
                    Value = Params[Param]
                });
            }
        }

        #endregion
    }
}