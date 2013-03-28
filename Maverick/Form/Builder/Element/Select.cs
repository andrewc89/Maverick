
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maverick.ViewModels.General;

namespace Maverick.Form.Builder.Element
{
    /// <summary>
    /// select element
    /// </summary>
    public class Select : ElementBase, IElement
    {
        #region Constructor

        public Select () { }

        /// <summary>
        /// new Select element
        /// </summary>
        /// <param name="Name">select name</param>
        /// <param name="Classes">select classes</param>
        public Select (string Name, bool Disabled = false, params string[] Classes)
        {
            this.Name = Name;
            this.Disabled = Disabled;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
            this.Options = new List<SelectElement>();
        }

        public Select (string Name, long ID, bool Disabled = false, params string[] Classes)
        {
            this.Name = Name;
            this.ID = ID;
            this.Disabled = Disabled;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
            this.Options = new List<SelectElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// list of select options
        /// </summary>
        public List<SelectElement> Options { get; set; }

        /// <summary>
        /// id of selected element
        /// </summary>
        public long ID { get; set; }

        public bool Disabled { get; set; }

        #endregion

        #region Public Functions

        public void AddOption (long ID, string DisplayName)
        {
            AddOption(new SelectElement(ID, DisplayName));
        }

        public void AddOption (SelectElement Option)
        {
            this.Options.Add(Option);
        }

        public void AddOptions (List<SelectElement> Options)
        {
            foreach (var Option in Options)
            {
                AddOption(Option);
            }
        }

        /// <summary>
        /// converts Select element to html string
        /// </summary>
        /// <returns>html representation of Select element</returns>
        public override string ToHtml ()
        {
            string Disabled = (this.Disabled) ? "disabled='disabled'" : "";
            var String = new StringBuilder();            
            String.AppendFormat("<select name='{0}.ID' {1} class='{2}'>\n", this.Name, Disabled, string.Join(" ", this.Classes));
            String.Append("<option value=''>Select...</option>");

            foreach (var Option in this.Options.OrderBy(x => x.DisplayName))
            {
                if (Option.ID == this.ID)
                {
                    String.AppendFormat("<option value='{0}' selected='selected'>{1}</option>\n", Option.ID, Option.DisplayName);
                }
                else
                {
                    String.AppendFormat("<option value='{0}'>{1}</option>\n", Option.ID, Option.DisplayName);
                }
            }

            String.Append("</select><br /> <br />\n\n");

            return String.ToString();
        }

        #endregion
    }
}
