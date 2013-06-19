using System;

namespace Togglr.ValueProviders
{
    public class FeatureToggleValue
    {
        public FeatureToggleValue(string identity, bool isEnabled)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException("Null, empty or whitespace is not accepted as an identifier.", "identity");
            }

            Identity = identity;
            IsEnabled = isEnabled;
        }

        public string Identity { get; private set; }
        public bool IsEnabled { get; private set; }

        public bool HasIdentity(string identity)
        {
            var stripedIdentity = Identity.ToLowerInvariant().Replace("featuretoggle", "");
            var stripedOtherIdentity = identity.ToLowerInvariant().Replace("featuretoggle", "");

            return stripedIdentity.Equals(stripedOtherIdentity, StringComparison.InvariantCultureIgnoreCase);
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
                throw new FormatException("Not expected format of text. It must be identifier=on or identifier=off");
            }
                
            var identity = elements[0];
            var isEnabled = ParseToggleState(elements[1]);

            return new FeatureToggleValue(identity, isEnabled);
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