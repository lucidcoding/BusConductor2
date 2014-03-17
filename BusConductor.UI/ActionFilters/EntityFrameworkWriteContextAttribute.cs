using System.Web;
using System.Web.Mvc;
using BusConductor.Data.Common;
using BusConductor.Data.Core;
using StructureMap;
using StructureMap.Attributes;

namespace BusConductor.UI.ActionFilters
{
    public class EntityFrameworkWriteContextAttribute : ActionFilterAttribute
    {
        [SetterProperty]
        public IContextProvider ContextProvider { get; set; }

        public EntityFrameworkWriteContextAttribute()
        {
            Order = 1000;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ContextProvider.SaveChanges();
            ContextProvider.Dispose();

            //todo: lookinto this?
            //ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            //http://stackoverflow.com/questions/4483659/how-to-dispose-resources-with-dependency-injection

        }
    }
}