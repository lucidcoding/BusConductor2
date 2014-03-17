using System.Web;
using System.Web.Mvc;
using BusConductor.Data.Common;
using BusConductor.Data.Core;
using StructureMap.Attributes;

namespace BusConductor.UI.ActionFilters
{
    public class EntityFrameworkReadContextAttribute : ActionFilterAttribute
    {
        [SetterProperty]
        public IContextProvider ContextProvider { get; set; }

        public EntityFrameworkReadContextAttribute()
        {
            Order = 1000;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ContextProvider.Dispose();
        }
    }
}