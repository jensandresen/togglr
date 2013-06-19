using System.Collections.Generic;
using Moq;
using Togglr.ValueProviders;

namespace Togglr.Tests.Builders
{
    internal class FeatureToggleValueProviderBuilder
    {
        private readonly Dictionary<string, FeatureToggleValue> _toggles;

        public FeatureToggleValueProviderBuilder()
        {
            _toggles = new Dictionary<string, FeatureToggleValue>();
        }

        public FeatureToggleValueProviderBuilder WithFeatureToggle(string identity, bool isEnabled)
        {
            var toggleValue = new FeatureToggleValue(identity, isEnabled);

            if (_toggles.ContainsKey(identity))
            {
                _toggles[identity] = toggleValue;
            }
            else
            {
                _toggles.Add(identity, toggleValue);
            }

            return this;
        }

        public IFeatureToggleValueProvider Build()
        {
            var stub = new Mock<IFeatureToggleValueProvider>();
            stub.Setup(p => p.GetByIdentitier(It.IsAny<string>())).Returns<string>(identity => _toggles.ContainsKey(identity) ? _toggles[identity] : null);

            return stub.Object;
        }
    }
}