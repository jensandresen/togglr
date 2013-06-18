using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Togglr.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString PartialAsFeature(this HtmlHelper htmlHelper, string partialViewName, IFeatureToggle featureToggle)
        {
            return PartialAsFeature(htmlHelper, partialViewName, null, htmlHelper.ViewData, featureToggle);
        }

        public static MvcHtmlString PartialAsFeature(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData, IFeatureToggle featureToggle)
        {
            return PartialAsFeature(htmlHelper, partialViewName, null, viewData, featureToggle);
        }

        public static MvcHtmlString PartialAsFeature(this HtmlHelper htmlHelper, string partialViewName, object model, IFeatureToggle featureToggle)
        {
            return PartialAsFeature(htmlHelper, partialViewName, model, htmlHelper.ViewData, featureToggle);
        }

        public static MvcHtmlString PartialAsFeature(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData, IFeatureToggle featureToggle)
        {
            return featureToggle.IsEnabled ? htmlHelper.Partial(partialViewName, model, viewData) : MvcHtmlString.Empty;
        }

        public static bool IsFeatureEnabled(this HtmlHelper htmlHelper, IFeatureToggle featureToggle)
        {
            return featureToggle.IsEnabled;
        }
    }
}