
namespace Maverick.Form.Builder
{
    using System;
    using System.Collections.Generic;
    using Maverick.Form.Builder.Element;

    /// <summary>
    /// creates a new, constructed Form.
    /// all objects inheriting from interface type must have a
    /// public long ID field and a public static T Load(long ID) method
    /// </summary>
    public static class FormFactory
    {
        /// <summary>
        /// creates form elements using reflection
        /// </summary>
        /// <typeparam name="T">object to build form for</typeparam>
        /// <typeparam name="K">interface to recognize user-defined objects</typeparam>
        /// <returns>new Form object</returns>
        public static Form Create (Type ModelType)
        {
            var Form = new Form();
            Form.Action = "/" + ModelType.Name + "/Create";
            Form.Method = "post";
            Form.ClassType = ModelType;
            Form.Elements = new List<IElement>();
            new FormBuilder(Form).Construct(ModelType);
            return Form;
        }

        /// <summary>
        /// creates form elements using refection and fills form with values
        /// of passed in object for editing
        /// </summary>
        /// <typeparam name="T">object to build form for</typeparam>
        /// <typeparam name="K">interface to recongize user-defined objects</typeparam>
        /// <param name="obj">object from which to get values to fill in form</param>
        /// <returns>new Form object</returns>
        public static Form Create (Type ModelType, object Model)
        {
            var Form = new Form();
            Form.Action = "/" + ModelType.Name + "/Edit/" + ModelType.GetProperty("ID").GetValue(Model, null);
            Form.Method = "post";
            Form.ClassType = ModelType;
            Form.Elements = new List<IElement>();
            new FormBuilder(Form).Construct(ModelType, Model);
            return Form;
        }
    }
}