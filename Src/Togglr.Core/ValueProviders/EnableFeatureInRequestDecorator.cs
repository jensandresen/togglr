using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Togglr.ValueProviders
{
    public class EnableFeatureInRequestDecorator : IFeatureToggleValueProvider
    {
        private readonly IFeatureToggleValueProvider _inner;

        public EnableFeatureInRequestDecorator(IFeatureToggleValueProvider inner)
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

        public FeatureToggleValue GetById(string id)
        {
            var toggleValue = GetFeatureToggleValues().FirstOrDefault(toggle => toggle.HasId(id));
            return toggleValue ?? _inner.GetById(id);
        }
    }
}