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

        public FeatureToggleValueProviderBuilder WithFeatureToggle(string id, bool isEnabled)
        {
            var toggleValue = new FeatureToggleValue(id, isEnabled);

            if (_toggles.ContainsKey(id))
            {
                _toggles[id] = toggleValue;
            }
            else
            {
                _toggles.Add(id, toggleValue);
            }

            return this;
        }

        public IFeatureToggleValueProvider Build()
        {
            var stub = new Mock<IFeatureToggleValueProvider>();
            stub.Setup(p => p.GetById(It.IsAny<string>())).Returns<string>(id => _toggles.ContainsKey(id) ? _toggles[id] : null);

            return stub.Object;
        }
    }
}