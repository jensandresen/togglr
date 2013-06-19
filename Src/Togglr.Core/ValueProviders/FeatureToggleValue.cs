using System;

namespace Togglr.ValueProviders
{
    public class FeatureToggleValue
    {
        public FeatureToggleValue(string identity, bool isEnabled)
        {
            Identity = identity;
            IsEnabled = isEnabled;
        }

        public string Identity { get; private set; }
        public bool IsEnabled { get; private set; }

        public static FeatureToggleValue Parse(string text)
        {
            var elements = text.Split('=');
                
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

        public bool HasIdentity(string identity)
        {
            var stripedOtherIdentity = identity.Replace("FeatureToggle", "");
            var stripedIdentity = Identity.Replace("FeatureToggle", "");

            return stripedIdentity.Equals(stripedOtherIdentity, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}