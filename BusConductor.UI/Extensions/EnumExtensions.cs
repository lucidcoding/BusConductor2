using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace BusConductor.UI.Extensions
{
    public static class EnumExtensions
    {
        public static List<T> GetValues<T>(this T enumeration)
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList(); 
        }

        public static string GetDescription(this Enum enumeration)
        {
            var fi = enumeration.GetType().GetField(enumeration.ToString());

            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return enumeration.ToString(CultureInfo.InvariantCulture);
        }


        public static SelectList ToSelectList(this Enum enumeration, Enum selectedValue)
        {
            var list = (Enum.GetValues(enumeration.GetType()).Cast<Enum>().Select(d => new
                                                                                           {
                                                                                               Value = (int) Enum.Parse(
                                                                                                   enumeration.GetType(),
                                                                                                   Enum.GetName(
                                                                                                       enumeration.
                                                                                                           GetType(), d)),
                                                                                               Text = d.GetDescription()
                                                                                           })).ToList();

            return new SelectList(list, "Value", "Text", selectedValue);
        }
    }
}