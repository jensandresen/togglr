using System.Collections.Generic;
using Moq;

namespace Togglr.Tests.Builders
{
    internal class FeatureToggleValueProviderBuilder
    {
        private Dictionary<string, bool> _toggles;

        public FeatureToggleValueProviderBuilder()
        {
            _toggles = new Dictionary<string, bool>();
        }

        public FeatureToggleValueProviderBuilder WithFeatureToggle(string identity, bool isEnabled)
        {
            if (_toggles.ContainsKey(identity))
            {
                _toggles[identity] = isEnabled;
            }
            else
            {
                _toggles.Add(identity, isEnabled);
            }

            return this;
        }

        public IFeatureToggleValueProvider Build()
        {
            var stub = new Mock<IFeatureToggleValueProvider>();
            stub.Setup(p => p.IsEnabled(It.IsAny<string>())).Returns<string>(identity => _toggles.ContainsKey(identity) ? _toggles[identity] : false);

            return stub.Object;
        }
    }
}