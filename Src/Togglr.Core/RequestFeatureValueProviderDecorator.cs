using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Togglr.ValueProviders;

namespace Togglr
{
    internal class RequestFeatureValueProviderDecorator : IFeatureToggleValueProvider
    {
        private readonly IFeatureToggleValueProvider _inner;

        public RequestFeatureValueProviderDecorator(IFeatureToggleValueProvider inner)
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
                .Select(identity => new FeatureToggleValue(identity, true))
                .ToArray();

            return toggleValues;
        }

        public FeatureToggleValue GetByIdentitier(string identifier)
        {
            var toggleValue = GetFeatureToggleValues().FirstOrDefault(toggle => toggle.HasIdentity(identifier));
            return toggleValue ?? _inner.GetByIdentitier(identifier);
        }
    }
}