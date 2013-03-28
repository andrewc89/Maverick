
using System.Collections.Generic;

namespace Maverick.Form.Builder.Element
{
    /// <summary>
    /// base class for form elements
    /// </summary>
    public abstract class ElementBase : IElement
    {
        #region Constructor

        public ElementBase () { }

        #endregion

        #region Properties

        /// <summary>
        /// element name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// element classes
        /// </summary>
        public List<string> Classes { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// converts Element to html string
        /// </summary>
        /// <returns>html representation of Element</returns>
        public abstract string ToHtml();

        #endregion
    }
}
