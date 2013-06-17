using Togglr.ValueProviders;

namespace Togglr
{
    internal class Configuration : IConfiguration
    {
        private readonly IFeatureToggleValueProvider _valueProvider;

        public Configuration(IFeatureToggleValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public IFeatureToggleValueProvider ValueProvider
        {
            get { return _valueProvider; }
        }
    }
}