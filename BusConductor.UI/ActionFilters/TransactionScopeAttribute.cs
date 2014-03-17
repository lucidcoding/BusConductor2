using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace BusConductor.UI.ActionFilters
{
    public class TransactionScopeAttribute : ActionFilterAttribute
    {
        public TransactionScopeAttribute()
        {
            Order = 990;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var transactionScope = new TransactionScope();
            HttpContext.Current.Items["TransactionScope"] = transactionScope;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var transactionScope = HttpContext.Current.Items["TransactionScope"] as TransactionScope;

            if(filterContext.Exception == null)
            {
                transactionScope.Complete();
            }

            transactionScope.Dispose();
            HttpContext.Current.Items["TransactionScope"] = null;
        }
    }
}