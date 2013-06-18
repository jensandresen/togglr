using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Togglr.Extensions
{
    public static class HtmlHelperExtensions
    {
         public static MvcHtmlString PartialAsFeature(this HtmlHelper htmlHelper, string partialViewName, IFeatureToggle featureToggle)
         {
             return featureToggle.IsEnabled ? htmlHelper.Partial(partialViewName) : MvcHtmlString.Empty;
         }

         public static bool IsFeatureEnabled(this HtmlHelper htmlHelper, IFeatureToggle featureToggle)
         {
             return featureToggle.IsEnabled;
         }
    }
}