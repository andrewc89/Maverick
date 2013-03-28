using Maverick.Models;
using System.Collections.Generic;
using Maverick.ViewModels.General;

namespace Maverick.Extensions
{
    public static class SelectElementExtensions
    {
        /// <summary>
        /// converts IEnumerable of T elements to List of SelectElement objects 
        /// designed to be used to fill select element dropdowns
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Elements"></param>
        /// <returns></returns>
        public static List<SelectElement> ToSelectElementList<T> (this IEnumerable<T> Elements)
            where T : ModelBase<T>, new()
        {
            var Temp = new List<SelectElement>();
            foreach (var Element in Elements)
            {
                Temp.Add(new SelectElement(Element.ID, Element.ToString()));
            }
            return Temp;
        }
    }
}