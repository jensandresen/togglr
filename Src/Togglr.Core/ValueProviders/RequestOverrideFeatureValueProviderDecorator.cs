using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Togglr.ValueProviders
{
    internal class RequestOverrideFeatureValueProviderDecorator : IFeatureToggleValueProvider
    {
        private readonly IFeatureToggleValueProvider _inner;

        public RequestOverrideFeatureValueProviderDecorator(IFeatureToggleValueProvider inner)
        {
            _inner = inner;
        }

        private IEnumerable<string> GetFeatureIdentitiersFromQueryString()
        {
            if (HttpContext.Current == null)
            {
                return Enumerable.Empty<string>();
            }

            var csv = HttpContext.Current.Request.QueryString["EnableFeature"] ?? "";
            var features = csv.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            return features;
        }

        private IEnumerable<FeatureToggleValue> GetFeatureToggleValues()
        {
            var toggleValues = GetFeatureIdentitiersFromQueryString()
                .Select(id => new FeatureToggleValue(id, true))
                .ToArray();

            return toggleValues;
        }

        public FeatureToggleValue GetByIdentitier(string id)
        {
            var toggleValue = GetFeatureToggleValues().FirstOrDefault(toggle => toggle.HasId(id));
            return toggleValue ?? _inner.GetByIdentitier(id);
        }
    }
}