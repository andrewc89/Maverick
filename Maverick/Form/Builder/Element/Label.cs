
using System;
using Maverick.Extensions;

namespace Maverick.Form.Builder.Element
{
    /// <summary>
    /// label element
    /// </summary>
    public class Label
    {
        #region Constructor

        public Label () { }

        /// <summary>
        /// new Label element
        /// </summary>
        /// <param name="For">for="" text</param>
        /// <param name="Text">html text</param>
        public Label (string For, string Text)
        {
            this.For = For;
            this.Text = Text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// for="" text
        /// </summary>
        public string For { get; set; }

        /// <summary>
        /// html text
        /// </summary>
        public string Text { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// converts Label to html string
        /// </summary>
        /// <returns>html representation of Label element</returns>
        public string ToHtml ()
        {
            return String.Format("<label for='{0}'>{1}:</label><br />\n", For, Text.SplitByWord());
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// constructs a new Label and returns its toHtml() string
        /// </summary>
        /// <param name="For">for="" text</param>
        /// <param name="Text">html text</param>
        /// <returns>new label's toHtml() string</returns>
        public static string Get (string For, string Text)
        {
            return new Label(For, Text).ToHtml();
        }

        #endregion
    }
}
