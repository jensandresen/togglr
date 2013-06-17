using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Togglr.Tests
{
    internal class RequestFeatureValueProviderDecorator : IFeatureToggleValueProvider
    {
        private readonly IFeatureToggleValueProvider _inner;

        public RequestFeatureValueProviderDecorator(IFeatureToggleValueProvider inner)
        {
            _inner = inner;
        }

        private IEnumerable<string> GetEnabledFeaturesFromQueryString()
        {
            if (HttpContext.Current == null)
            {
                return Enumerable.Empty<string>();
            }

            var csv = HttpContext.Current.Request.QueryString["EnableFeature"] ?? "";
            var features = csv.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            return features;
        }

        public bool IsEnabled(string featureToggleIdentity)
        {
            var enabledFeatures = GetEnabledFeaturesFromQueryString();
            var isEnabledInRequest = enabledFeatures.Any(feature => feature.Equals(featureToggleIdentity, StringComparison.InvariantCultureIgnoreCase));

            return isEnabledInRequest || _inner.IsEnabled(featureToggleIdentity);
        }
    }
}