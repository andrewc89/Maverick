
namespace Maverick.Form.ModelBinder
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Reflection;

    /// <summary>
    /// generic model binder
    /// </summary>
    public class ModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// bind form data to object
        /// </summary>
        /// <param name="controllerContext">controller context</param>
        /// <param name="bindingContext">binding context</param>
        /// <returns>form data-bound object</returns>
        public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var ModelType = bindingContext.ModelType;
            var Instance = Activator.CreateInstance(ModelType);
            var Form = bindingContext.ValueProvider;

            foreach (var Property in ModelType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 ) && Property.GetSetMethod() != null)
                {
                    if (!PropertyType.FullName.StartsWith("System") && Form.GetValue(Property.Name + ".ID") != null && !string.IsNullOrEmpty(Form.GetValue(Property.Name + ".ID").AttemptedValue))
                    {
                        var Load = PropertyType.GetMethod("Load", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static, null, new Type[] { typeof(long) }, null);
                        var Value = Load.Invoke(new object(), new object[] { long.Parse(Form.GetValue(Property.Name + ".ID").AttemptedValue) });
                        Property.SetValue(Instance, Value, null);
                    }
                    else if (PropertyType.Equals(typeof(bool)))
                    {
                        if (Form.GetValue(Property.Name) == null)
                        {                           
                            Property.SetValue(Instance, false, null);
                        }
                        else //if (Form.GetValue(Property.Name).AttemptedValue.Equals("on"))
                        {                            
                            Property.SetValue(Instance, true, null);                            
                        }
                    }
                    else
                    {
                        if (Form.GetValue(Property.Name) != null)
                        {
                            Property.SetValue(Instance, Convert.ChangeType(bindingContext.ValueProvider.GetValue(Property.Name).AttemptedValue, PropertyType), null);
                        }
                    }
                }
            }

            return Instance;
        }
    }
}
