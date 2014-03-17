using System.Web;
using System.Web.Mvc;
using Lucidity.Utilities.Contracts.Logging;
using StructureMap.Attributes;

namespace BusConductor.UI.ActionFilters
{
    public class LogAttribute : ActionFilterAttribute
    {
        [SetterProperty]
        public ILog Log { get; set; }

        public LogAttribute()
        {
            Order = 980;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //todo: no data when data is in URL (Booking.Make) - can get value from action parameters.
            var url = filterContext.HttpContext != null
                      && filterContext.HttpContext.Request != null
                      && filterContext.HttpContext.Request.Url != null
                          ? filterContext.HttpContext.Request.Url.AbsolutePath
                          : "";

            var data = filterContext.HttpContext != null
                       && filterContext.HttpContext.Request != null
                       && filterContext.HttpContext.Request.Form != null
                           ? HttpContext.Current.Server.UrlDecode(filterContext.HttpContext.Request.Form.ToString()).Split('&')
                           : null;

            Log.Add("URL: " + url, data);

            //todo: get this working.
            //var data = filterContext.HttpContext != null
            //           && filterContext.HttpContext.Request != null
            //               ? filterContext.HttpContext.Request.Form
            //               : null;

            //Log.Add("URL: " + url, data);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.Exception != null)
            {
                Log.Add(filterContext.Exception);
            }
        }
    }
}