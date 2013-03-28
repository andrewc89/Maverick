using System;
using System.Linq;

namespace Maverick.Form.Builder.Element
{
    public class Anchor : ElementBase, IElement
    {
        #region Constructor

        public Anchor () { }

        public Anchor (string Name, string Href, string Value, params string[] Classes)
        {
            this.Name = Name;
            this.Href = "/Objects" + Href;
            this.Value = Value;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
        }

        #endregion

        #region Properties

        public string Href { get; set; }

        public string Value { get; set; }

        #endregion

        #region Public Functions

        public override string ToHtml ()
        {
            return String.Format("<a href='{0}' class='{2}'>{1}</a><br /><br />\n\n", Href, Value, string.Join(" ", Classes));
        }

        #endregion
    }
}