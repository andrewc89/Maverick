
using System;
using System.Linq;

namespace Maverick.Form.Builder.Element
{
    /// <summary>
    /// input element
    /// </summary>
    public class Input : ElementBase, IElement
    {
        #region Constructor

        public Input () { }

        /// <summary>
        /// new Input element
        /// </summary>
        /// <param name="Type">input type (checkbox, text, etc.)</param>
        /// <param name="Name">input name (for postback)</param>
        /// <param name="Classes">input classes</param>
        public Input (string Name, string Type, string Value = "", bool Disabled = false, params string[] Classes)
        {
            this.Type = Type;
            this.Name = Name;
            this.Value = Value;
            this.Disabled = Disabled;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
        }

        #endregion

        #region Properties

        /// <summary>
        /// input type (checkbox, text, etc.)
        /// </summary>
        public string Type { get; set; }

        public string Value { get; set; }

        public bool Disabled { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// converts Input to html string
        /// </summary>
        /// <returns>html representation of Input element</returns>
        public override string ToHtml ()
        {
            string Disabled = (this.Disabled) ? "disabled='disabled'" : "";
            if (this.Type.Equals("checkbox"))
            {
                string Checked = (this.Value.ToLower().Equals("true")) ? "checked='checked'" : "";                
                return String.Format("<input type='{0}' name='{1}' {2} {3} class='{4}' /><br /><br />\n\n", this.Type, this.Name, Checked, Disabled, string.Join(" ", Classes.RemoveAll(x => x.Equals("required"))));
            }
            else if (this.Type.Equals("hidden"))
            {
                return String.Format("<input type='{0}' name='{1}' value='{2}' {3} class='{4}' />\n\n", this.Type, this.Name, this.Value, Disabled, string.Join(" ", Classes));
            }
            else
            {
                return String.Format("<input type='{0}' name='{1}' value='{2}' {3} class='{4}' /><br /><br />\n\n", this.Type, this.Name, this.Value, Disabled, string.Join(" ", Classes));
            }
        }

        #endregion
    }
}