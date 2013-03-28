
using System;
using System.Collections.Generic;
using System.Text;
using Maverick.Form.Builder.Element;
using Maverick.ViewModels.General;
using Maverick.Models;

namespace Maverick.Form.Builder
{
    /// <summary>
    /// constructs a form element from a given object.
    /// use: FormFactory.Create&lt;Class,Interface&gt;().ToHtml() + chaining functions
    /// </summary>
    public class Form
    {
        #region Constructor

        public Form () { }

        public Form (Type ClassType, Type InterfaceType, string Method = "post")
        {            
            this.ClassType = ClassType;
            this.InterfaceType = InterfaceType;
            this.Action = "/" + ClassType.Name + "/Create";
            this.Method = Method;
            this.Elements = new List<IElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// form action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// form method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// form elements
        /// </summary>
        public List<IElement> Elements { get; set; }

        /// <summary>
        /// type of object specified for form
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// interface type for user-defined objects
        /// </summary>
        public Type InterfaceType = typeof(IModelBase);

        #endregion

        #region Chaining Functions

        /// <summary>
        /// add options to a Select element specified by name
        /// </summary>
        /// <param name="ElementName">name of Select element to add options to</param>
        /// <param name="Options">list of SelectElement objects to add</param>
        /// <returns>this</returns>
        public Form AddSelectOptions (string ElementName, List<SelectElement> Options)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)) && this.Elements.Find(x => x.Name.Equals(ElementName)) is Select)
            {
                var Element = (Select)this.Elements.Find(x => x.Name.Equals(ElementName));
                Element.AddOptions(Options);
            }
            return this;
        }

        /// <summary>
        /// allows for changing the Form's Action while chaining
        /// </summary>
        /// <param name="Action">string Action</param>
        /// <returns>this</returns>
        public Form SetAction (string Action)
        {
            this.Action = Action;
            return this;
        }

        /// <summary>
        /// allows for changing the Form's Method while chaining
        /// </summary>
        /// <param name="Method">string Method</param>
        /// <returns>this</returns>
        public Form SetMethod (string Method)
        {
            this.Method = Method;
            return this;
        }

        /// <summary>
        /// adds "required" class to all elements for jquery validation
        /// </summary>
        /// <returns>this</returns>
        public Form AllRequired ()
        {
            foreach (var Element in this.Elements)
            {
                Element.Classes.Add("required");
            }
            return this;
        }

        /// <summary>
        /// adds "required" class to specified element for jquery validation
        /// </summary>
        /// <param name="ElementName">Element name to match</param>
        /// <returns>this</returns>
        public Form IsRequired (string ElementName)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)))
            {
                this.Elements.Find(x => x.Name.Equals(ElementName)).Classes.Add("required");
            }
            return this;
        }

        /// <summary>
        /// adds "required" class to specified elements for jquery validation
        /// </summary>
        /// <param name="Elements">Element names to match</param>
        /// <returns>this</returns>
        public Form AreRequired (List<string> Elements)
        {
            foreach (var ElementName in Elements)
            {
                IsRequired(ElementName);
            }
            return this;
        }

        /// <summary>
        /// removes any "required" classes from specified element
        /// </summary>
        /// <param name="ElementName">Element name to match</param>
        /// <returns>this</returns>
        public Form IsNotRequired (string ElementName)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)))
            {
                this.Elements.Find(x => x.Name.Equals(ElementName)).Classes.RemoveAll(x => x.Equals("required"));
            }
            return this;
        }

        /// <summary>
        /// removes any "required" classes from specified elements
        /// </summary>
        /// <param name="Elements">Element names to match</param>
        /// <returns>this</returns>
        public Form AreNotRequired (params string[] Elements)
        {
            foreach (var ElementName in Elements)
            {
                IsNotRequired(ElementName);
            }
            return this;
        }

        /// <summary>
        /// sets the value of an input specified by name
        /// </summary>
        /// <param name="Element">element name</param>
        /// <param name="Value">element value</param>
        /// <returns>this</returns>
        public Form SetValue (string Element, string Value)
        {
            var Input = (Input)this.Elements.Find(x => x.Name.Equals(Element));
            Input.Value = Value;
            return this;
        }

        /// <summary>
        /// sets the value of a select element by id
        /// </summary>
        /// <param name="Element">element name</param>
        /// <param name="Value">element value</param>
        /// <returns>this</returns>
        public Form SetValue (string Element, long Value)
        {
            var Select = (Select)this.Elements.Find(x => x.Name.Equals(Element));
            Select.ID = Value;
            return this;
        }

        /// <summary>
        /// removes an element specified by name
        /// </summary>
        /// <param name="Element">element name</param>
        /// <returns>this</returns>
        public Form RemoveElement (string Element)
        {
            this.Elements.RemoveAll(x => x.Name.Equals(Element));
            return this;
        }

        /// <summary>
        /// removes multiple elements specified by name
        /// </summary>
        /// <param name="Elements">element name</param>
        /// <returns>this</returns>
        public Form RemoveElements (params string[] Elements)
        {
            foreach (var Element in Elements)
            {
                RemoveElement(Element);
            }
            return this;
        }

        /// <summary>
        /// adds an input element to the form
        /// </summary>
        /// <param name="Type">property type</param>
        /// <param name="Name">property name</param>
        /// <param name="Value">property value (defaults to empty string)</param>
        /// <returns>this</returns>
        public Form AddInput (Type Type, string Name, string Value = "")
        {
            new FormBuilder(this).AddInput(Type, Name, Value);
            return this;
        }

        /// <summary>
        /// adds a select element to the form
        /// </summary>
        /// <param name="Name">property name</param>
        /// <param name="ID">id of selected value (defaults to 0)</param>
        /// <returns>this</returns>
        public Form AddSelect (string Name, long ID = 0)
        {
            new FormBuilder(this).AddSelect(Name, ID);
            return this;
        }

        /// <summary>
        /// adds a textarea element to the form
        /// </summary>
        /// <param name="Name">property name</param>
        /// <param name="Value">property value (defaults to empty string)</param>
        /// <returns>this</returns>
        public Form AddTextArea (string Name, string Value = "")
        {
            new FormBuilder(this).AddTextArea(Name, Value);
            return this;
        }

        #endregion

        #region ToHtml

        /// <summary>
        /// outputs form as html
        /// </summary>
        /// <returns>html string</returns>
        public string ToHtml ()
        {
            var Form = new StringBuilder();

            Form.AppendFormat("<form id='{0}' action='' method='{1}'>\n\n", this.ClassType.Name, this.Method);

            foreach (var Element in Elements)
            {
                if (!(Element.GetType().Equals(typeof(Input)) && ((Input)Element).Type.Equals("hidden")) && !Element.GetType().Equals(typeof(Anchor)))
                {
                    Form.Append(Label.Get(Element.Name, Element.Name));
                }
                Form.Append(Element.ToHtml());
            }

            Form.Append("<input type='submit' value='Submit' />");

            Form.Append("</form>");

            return Form.ToString();
        }
        
        #endregion
    }
}