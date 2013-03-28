using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Maverick.Helpers.Filter
{
    internal class FilterProperty
    {
        #region Constructor

        internal FilterProperty () { }

        #endregion

        #region Properties

        /// <summary>
        /// corresponding property's PropertyInfo
        /// </summary>
        internal PropertyInfo PropertyInfo { get; set; }
        
        /// <summary>
        /// specified value of property with which to filter
        /// </summary>
        internal string Value { get; set; }

        #endregion
    }
}