using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusConductor.UI.Extensions
{
    public static class SelectListExtensions
    {
        public static SelectList AddDefaultOption(this SelectList selectList)
        {
            var selectListItems = selectList.ToList();
            selectListItems.Insert(0, new SelectListItem() { Value = null, Text = "[Please Select...]" });
            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}