using System;
using System.Collections.Generic;

namespace Maverick.Form.Builder.Element
{
    public class TextArea : ElementBase, IElement
    {
        #region Constructor

        public TextArea () { }

        public TextArea (string Name, string Value = "")
        {
            this.Name = Name;
            this.Value = Value;
            this.Classes = new List<string> { this.Name };
        }

        #endregion

        #region Properties

        public string Value { get; set; }

        #endregion

        #region ToHtml

        public override string ToHtml ()
        {
            return String.Format("<textarea name='{0}' class='{1}' cols='75' rows='10'>{2}</textarea><br /><br />\n\n", this.Name, string.Join(" ", Classes), this.Value);
        }

        #endregion
    }
}