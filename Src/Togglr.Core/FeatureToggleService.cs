using System;
using Togglr.ValueProviders;

namespace Togglr
{
    public class FeatureToggleService
    {
        private readonly IFeatureToggleValueProvider _featureToggleValueProvider;

        public FeatureToggleService(IFeatureToggleValueProvider featureToggleValueProvider)
        {
            if (featureToggleValueProvider == null)
            {
                throw new ArgumentNullException("featureToggleValueProvider");
            }

            _featureToggleValueProvider = featureToggleValueProvider;
        }

        public IFeatureToggleValueProvider FeatureToggleValueProvider
        {
            get { return _featureToggleValueProvider; }
        }

        public bool IsEnabled(IFeatureToggle featureToggle)
        {
            return IsEnabled(featureToggle.Id);
        }

        public bool IsEnabled(string identity)
        {
            var toggleValue = _featureToggleValueProvider.GetByIdentitier(identity);
            return toggleValue != null && toggleValue.IsEnabled;
        }
    }
}