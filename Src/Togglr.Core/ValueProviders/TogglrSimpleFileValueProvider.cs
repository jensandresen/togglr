using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Togglr.ValueProviders
{
    public class TogglrSimpleFileValueProvider : IFeatureToggleValueProvider
    {
        private const string CacheKey = "feature_toggle_values";
        private readonly string _filename;

        public TogglrSimpleFileValueProvider(string filename)
        {
            _filename = filename;
        }

        private IEnumerable<FeatureToggleValue> ReadFeatureTogglesFromFile()
        {
            if (!File.Exists(_filename))
            {
                return Enumerable.Empty<FeatureToggleValue>();
            }

            var lines = File.ReadAllLines(_filename);

            var toggleValues = lines
                .Where(IsValid)
                .Select(FeatureToggleValue.Parse)
                .ToArray();

            return toggleValues;
        }

        private IEnumerable<FeatureToggleValue> GetFeatureToggleValues()
        {
            var values = GetFromCache();

            if (values == null)
            {
                values = ReadFeatureTogglesFromFile();
                PutInCache(values);
            }

            return values;
        }

        private static void PutInCache(object values)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            HttpContext.Current.Items[CacheKey] = values;
        }

        private static IEnumerable<FeatureToggleValue> GetFromCache()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }

            return HttpContext.Current.Items[CacheKey] as IEnumerable<FeatureToggleValue>;
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