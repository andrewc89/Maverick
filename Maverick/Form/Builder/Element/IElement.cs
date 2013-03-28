
using System.Collections.Generic;

namespace Maverick.Form.Builder.Element
{
    /// <summary>
    /// Element interface
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// converts Element to html string
        /// </summary>
        /// <returns>html representation of Element</returns>
        string ToHtml();

        string Name { get; }

        List<string> Classes { get; }
    }
}
