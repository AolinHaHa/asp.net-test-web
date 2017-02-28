using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace assignment4.Helpers
{
    public static class payableList
    {

        //Based on http://stackoverflow.com/questions/5052752/adding-your-own-htmlhelper-in-asp-net-mvc-3
        //Simplified version
        public static MvcHtmlString PayableDropDownList(this HtmlHelper html)
        {
            List<SelectListItem> payablelist = new List<SelectListItem>
                    {
                        new SelectListItem() { Value= "0",Text= "Pay"},
                         new SelectListItem() {Value="1",Text="Unpay"},
                    };

            return html.DropDownList("State", payablelist);
        }

        public static MvcHtmlString PayableDropDownList<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            Dictionary<string, string> payablelist = new Dictionary<string, string>()
                          {
                             
                             {"0", " Pay"},
                             {"1", " Unpay"},

                          };

            return html.DropDownListFor(expression, new SelectList(payablelist, "key", "value"), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
    }

}
