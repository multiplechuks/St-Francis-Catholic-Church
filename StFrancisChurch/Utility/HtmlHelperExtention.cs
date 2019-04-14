using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace StFrancisChurch.Utility
{
    public static class HtmlHelperExtention
    {
        public static string IsSelectedActions(this HtmlHelper html, string controller = null, string[] actions = null, string cssClass = null, string start_text = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            //if (String.IsNullOrEmpty(action))
            //    action = currentAction;

            return controller == currentController && actions.Contains(currentAction) ?
                cssClass : String.Empty;
        }

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null, string start_text = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string IsSelectedFirst(this HtmlHelper html, string[] controllers = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            //string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            return controllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
            #if DEBUG
                return true;
            #else
                return false;
            #endif
        }
    }

    public static class TableHelperExtensions
    {
        public static string BuildTr(object _obj)
        {
            var properties = _obj.GetType().GetProperties();
            var tr = "<tr>";

            foreach (var property in properties)
            {
                tr += String.Format("<td>{0}</td>", property.GetValue(_obj));
            }

            tr += "</tr>";

            return (tr);
        }

        public static string BuildFlatTr(PropertyInfo prop, object _obj)
        {
            var tr = "<tr>";
            tr += String.Format("<td>{0}</td><td>{1}</td>", prop.Name, prop.GetValue(_obj));

            tr += "</tr>";

            return (tr);
        }

        public static string BuildTrHeader(object _obj)
        {
            var properties = _obj.GetType().GetProperties();
            var tr = "<tr>";

            foreach (var property in properties)
            {
                tr += String.Format("<th>{0}</th>", property.Name);
            }

            tr += "</tr>";

            return (tr);
        }

        public static HtmlString BuildTable(this HtmlHelper helper, object _obj)
        {
            if (!IsCollection(_obj.GetType()))
            {
                throw new InvalidOperationException("BuildTable helper can be called only on collection object");
            }

            var tableStart = String.Format(@"<table>
                            <thead>
                                {0}
                            </thead>
                            <tbody>", BuildTrHeader((_obj as IEnumerable<object>).ElementAt(0)));

            var tableBody = String.Empty;

            var coll = _obj as IEnumerable<object>;

            foreach (var _ in coll)
            {
                tableBody += BuildTr(_);
            }

            var tableEnd = @"
                            </tbody>
                        </table>"; ;

            return new HtmlString(tableStart + tableBody + tableEnd);
        }

        public static HtmlString BuildFlatTable(this HtmlHelper helper, object _obj)
        {
            Type myType = _obj.GetType();
            var tableStart = String.Format(@"<table class='table table-bordered' style='width:80%' align='center' cellspacing='0'>
                            <thead>
                                {0} Record
                            </thead>
                            <tbody>", myType.Name);

            var tableBody = String.Empty;
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                tableBody += BuildFlatTr(prop, _obj);
            }



            var tableEnd = @"
                            </tbody>
                        </table>";

            return new HtmlString(tableStart + tableBody + tableEnd);
        }

        static bool IsCollection(Type type)
        {
            return type.GetInterface(typeof(IEnumerable<>).FullName) != null;
        }
    }
}