using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaverickExample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters (GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes (RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region List

            routes.MapRoute(
                "List.json",
                "{controller}/List.{format}",
                new { action = "List" }
            );

            routes.MapRoute(
                "List",
                "{controller}/List",
                new { action = "List" }
            );

            #endregion

            #region Create

            routes.MapRoute(
                "Create",
                "{controller}/Create",
                new { action = "Create" }
            );

            #endregion

            #region Edit

            routes.MapRoute(
                "Edit w/ id",
                "{controller}/Edit/{id}",
                new { action = "Edit" }
            );

            routes.MapRoute(
                "Edit w/ id and property",
                "{controller}/Edit/{id}/{property}",
                new { action = "Edit" }
            );

            routes.MapRoute(
                "Edit w/ id, property and propertyId",
                "{controller}/Edit/{id}/{property}/{propertyId}",
                new { action = "Edit" }
            );

            #endregion

            #region JSON

            routes.MapRoute(
                "Default json w/ .json",
                "{controller}.{format}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default json",
                "{controller}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ id and .json",
                "{controller}/{id}.{format}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ id",
                "{controller}/{id}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ property and .json",
                "{controller}/{id}/{property}.{format}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ property",
                "{controller}/{id}/{property}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ property and propertyId and .json",
                "{controller}/{id}/{property}/{propertyId}.{format}",
                new { action = "Index" }
            );

            routes.MapRoute(
                "Default w/ property and propertyId",
                "{controller}/{id}/{property}/{propertyId}",
                new { action = "Index" }
            );

            #endregion

            #region Home

            routes.MapRoute(
                "Default",
                "",
                new { controller = "Home", action = "Index" }
            );

            #endregion
        }

        protected void Application_Start ()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}