using System.Web.Mvc;

namespace Maverick.Attributes
{
    public class RestHttpVerbFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting (ActionExecutingContext filterContext)
        {
            var HttpMethod = filterContext.HttpContext.Request.HttpMethod;
            filterContext.ActionParameters["HttpVerb"] = HttpMethod;
            base.OnActionExecuting(filterContext);
        }
    }
}