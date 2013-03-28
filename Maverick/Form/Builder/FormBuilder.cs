
namespace Maverick.Form.Builder
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Maverick.Form.Builder.Element;
    using System.Diagnostics;

    /// <summary>
    /// builds out the actual form via reflection
    /// </summary>
    public class FormBuilder
    {
        #region Constructors

        public FormBuilder () { }

        public FormBuilder (Form Form)
        {
            this.Form = Form;
        }

        #endregion
        
        #region Properties

        public Form Form { get; set; }

        #endregion

        #region Construct

        /// <summary>
        /// constructs form, converts object properties into form elements
        /// </summary>
        public void Construct (Type ModelType)
        {
            foreach (var Property in this.Form.ClassType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 && PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(this.Form.InterfaceType)))
                {
                    if (PropertyType.GetInterfaces().Contains(this.Form.InterfaceType))
                    {
                        AddSelect(Property.Name, Disabled: Property.DeclaringType != ModelType);
                    }
                    else
                    {
                        if (Property.GetSetMethod() != null)
                        {
                            AddInput(PropertyType, Property.Name, Disabled: Property.DeclaringType != ModelType);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// constructs form, converts object properties into form elements
        /// adds values to form elements from given object
        /// to be used on forms for editing already existing objects
        /// </summary>
        /// <param name="Model"></param>
        public void Construct (Type ModelType, object Model)
        {
            foreach (var Property in this.Form.ClassType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 && PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(this.Form.InterfaceType)))
                {
                    if (PropertyType.GetInterfaces().Contains(this.Form.InterfaceType))
                    {
                        var PropertyValue = Property.GetValue(Model, null);
                        long ID = 0;
                        if (PropertyValue != null)
                        {
                            ID = (long)PropertyValue.GetType().GetProperty("ID").GetValue(PropertyValue, null);
                        }
                        AddSelect(Property.Name, ID, Property.DeclaringType != ModelType);
                    }
                    else
                    {
                        if (Property.GetSetMethod() != null)
                        {
                            string Value = (Property.GetValue(Model, null) != null) ? Property.GetValue(Model, null).ToString() : "";
                            AddInput(PropertyType, Property.Name, Value, Property.DeclaringType != ModelType);
                        }
                    }
                }
                else
                {
                    string PropertyTypeName = PropertyType.GetGenericArguments().First().Name;
                    string Url = "/" + ModelType.Name + "/Edit/" + ModelType.GetProperty("ID").GetValue(Model, null) + "/" + Property.Name;
                    AddAnchor(PropertyTypeName, Url, Property.Name);
                }
            }
        }
        
        #endregion

        #region Add Elements

        /// <summary>
        /// adds a select element to the form with no option selected
        /// </summary>
        /// <param name="Name">name of object property</param>
        public void AddSelect (string Name, bool Disabled = false)
        {
            this.Form.Elements.Add(new Select(Name, Disabled));
        }

        /// <summary>
        /// adds a select element to the form with an option selected
        /// based on the ID of the value of the property of the supplied object
        /// (obj.[property name].ID)
        /// </summary>
        /// <param name="Name">property name</param>
        /// <param name="ID">id of obj's property value</param>
        public void AddSelect (string Name, long ID, bool Disabled = false)
        {
            if (ID == 0)
            {
                AddSelect(Name, Disabled);
            }

            else
            {
                this.Form.Elements.Add(new Select(Name, ID, Disabled));
            }
        }

        /// <summary>
        /// adds an input element to the form with or w/o a value specified
        /// </summary>
        /// <param name="PropertyType">property type</param>
        /// <param name="Name">property name</param>
        /// <param name="Value">property value (as string)</param>
        public void AddInput (Type PropertyType, string Name, string Value = "", bool Disabled = false, params string[] Classes)
        {
            if (PropertyType.IsAssignableFrom(typeof(DateTime)))
            {
                string Class = "DateTime";
                if (!string.IsNullOrEmpty(Value))
                {                    
                    var Date = DateTime.Parse(Value);

                    if (Date == Date.Date)
                    {
                        Value = Date.ToShortDateString();
                        Class = "Date";
                    }
                    else
                    {
                        Value = Date.ToShortDateString() + " " + Date.ToShortTimeString();
                    }

                        
                }
                this.Form.Elements.Add(new Input(Name, "text", Value, Disabled, Class));
            }
            else if (PropertyType.IsAssignableFrom(typeof(Boolean)))
            {
                this.Form.Elements.Add(new Input(Name, "checkbox", Value, Disabled));
            }
            else
            {
                this.Form.Elements.Add(new Input(Name, "text", Value, Disabled));
            }
        }

        /// <summary>
        /// adds a textarea element to the form with the specified value (defaults to empty string)
        /// </summary>
        /// <param name="Name">property name</param>
        /// <param name="Value">property value (as string)</param>
        public void AddTextArea (string Name, string Value = "", params string[] Classes)
        {
            this.Form.Elements.Add(new TextArea(Name, Value));
        }

        public void AddAnchor (string Name, string Href, string Text)
        {
            this.Form.Elements.Add(new Anchor(Name, Href, Text));
        }

        #endregion
    }
}
