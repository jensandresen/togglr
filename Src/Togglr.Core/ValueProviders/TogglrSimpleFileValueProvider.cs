using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Togglr.ValueProviders
{
    public class TogglrSimpleFileValueProvider : IFeatureToggleValueProvider
    {
        private readonly string _filename;

        public TogglrSimpleFileValueProvider(string filename)
        {
            _filename = filename;
        }

        private IEnumerable<FeatureToggleValue> GetFeatureToggleValues()
        {
            if (!File.Exists(_filename))
            {
                return Enumerable.Empty<FeatureToggleValue>();
            }

            var lines = File.ReadAllLines(_filename);
            var toggleValues = lines
                .Select(text => FeatureToggleValue.Parse(text))
                .ToArray();

            return toggleValues;
        }

        public FeatureToggleValue GetByIdentitier(string identifier)
        {
            return GetFeatureToggleValues().FirstOrDefault(toggleValue => toggleValue.HasIdentity(identifier));
        }
    }
}