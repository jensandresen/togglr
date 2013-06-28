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
                .Where(text => IsValid(text))
                .Select(text => FeatureToggleValue.Parse(text))
                .ToArray();

            return toggleValues;
        }

        private bool IsValid(string text)
        {
          if (string.IsNullOrWhiteSpace(text))
          {
            return false;
          }

          if (text.Trim().StartsWith("#"))
          {
            return false;
          }

          return true;
        }

        public FeatureToggleValue GetById(string id)
        {
            return GetFeatureToggleValues().FirstOrDefault(toggleValue => toggleValue.HasId(id));
        }
    }
}