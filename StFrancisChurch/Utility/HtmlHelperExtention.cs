using System;
using System.Collections.Generic;
using System.Linq;
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
}