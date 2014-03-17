using System;
using System.Data.Entity;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusConductor.Data.Core;
using Lucidity.Utilities.Logging;
using BusConductor.Data.Common;
using BusConductor.UI.Common;
using BusConductor.UI.Core;
using StructureMap;

namespace BusConductor.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    //TODO: put last line error logging in.
    //todo: need to include release transforms?
    //todo: include stuff for dbdeploy.

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //TOdo: research this:
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Main",
                "vw-camper-hire-manchester",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );


            routes.MapRoute(
                  "Index",
                  "{controller}",
                  new { controller = "Home", action = "Index" }
            );

            //routes.MapRoute(
            //    "ClassicVwCamperHireManchester",
            //    "Home/Index/classic-vw-camper-hire-manchester",
            //    new { controller = "Home", action = "Index" }
            //);

            //routes.MapRoute(
            //    "CoolCatCampers",
            //    "Home/Index/cool-cat-campers",
            //    new { controller = "Home", action = "Index" }
            //);

            routes.MapRoute(
                "BusDetails",
                "Bus/Details/{id}",
                new { controller = "Bus", action = "Details", }
            );

            routes.MapRoute(
                "BookingMake", 
                "Booking/Make/{busId}",
                new { controller = "Booking", action = "Make" }
            );

            routes.MapRoute(
                "CalendarGet",
                "Calendar/Get/{year}/{month}/{busId}", 
                new { controller = "Calendar", action = "DisplayMonth" } 
            );

            routes.MapRoute(
                "AvailabilityDisplayFrom",
                "Availability/DisplayFrom/{startYear}/{startMonth}/{startDay}",
                new { controller = "Availability", action = "DisplayFrom" }
            );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //todo: is this the correct place for this?
            Database.SetInitializer<Context>(null);


            AreaRegistration.RegisterAllAreas();
            ObjectFactory.Container.Configure(x => x.AddRegistry<UiRegistry>());
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerActivator());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }
    }
}