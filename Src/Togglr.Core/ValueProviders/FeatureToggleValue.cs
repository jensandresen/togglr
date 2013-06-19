using System;

namespace Togglr.ValueProviders
{
    public class FeatureToggleValue
    {
        public FeatureToggleValue(string id, bool isEnabled)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Null, empty or whitespace is not accepted as an id.", "id");
            }

            Id = id;
            IsEnabled = isEnabled;
        }

        public string Id { get; private set; }
        public bool IsEnabled { get; private set; }

        public bool HasId(string id)
        {
            var stripedId = Id.ToLowerInvariant().Replace("featuretoggle", "");
            var stripedOtherId = id.ToLowerInvariant().Replace("featuretoggle", "");

            return stripedId.Equals(stripedOtherId, StringComparison.InvariantCultureIgnoreCase);
        }

        public static FeatureToggleValue Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException();
            }

            var elements = text.Split('=');

            if (elements.Length != 2 || string.IsNullOrWhiteSpace(elements[0]) || string.IsNullOrWhiteSpace(elements[1]))
            {
                throw new FormatException("Not expected format of text. It must be id=on or id=off");
            }
                
            var id = elements[0];
            var isEnabled = ParseToggleState(elements[1]);

            return new FeatureToggleValue(id, isEnabled);
        }

        private static bool ParseToggleState(string text)
        {
            if ("on".Equals(text, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}