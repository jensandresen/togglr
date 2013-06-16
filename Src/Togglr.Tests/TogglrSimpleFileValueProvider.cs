using System;
using System.IO;
using System.Linq;

namespace Togglr.Tests
{
    public class TogglrSimpleFileValueProvider : IFeatureToggleValueProvider
    {
        private readonly string _filename;

        public TogglrSimpleFileValueProvider(string filename)
        {
            _filename = filename;
        }

        public bool IsEnabled(string featureToggleIdentity)
        {
            var lines = File.ReadAllLines(_filename);

            var matchedFeatureToggleRecord = lines
                .Select(lineOfText => ToggleRecord.Parse(lineOfText))
                .FirstOrDefault(r => r.HasIdentity(featureToggleIdentity));

            return matchedFeatureToggleRecord != null && matchedFeatureToggleRecord.IsEnabled;
        }

        private class ToggleRecord
        {
            public ToggleRecord(string identity, bool isEnabled)
            {
                Identity = identity;
                IsEnabled = isEnabled;
            }

            public string Identity { get; private set; }
            public bool IsEnabled { get; private set; }

            public static ToggleRecord Parse(string text)
            {
                var elements = text.Split('=');
                
                var identity = elements[0];
                var isEnabled = ParseState(elements[1]);

                return new ToggleRecord(identity, isEnabled);
            }

            private static bool ParseState(string text)
            {
                if ("on".Equals(text, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }

                return false;
            }

            public bool HasIdentity(string identity)
            {
                return Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}