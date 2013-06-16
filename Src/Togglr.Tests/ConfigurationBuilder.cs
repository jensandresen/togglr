using System.Web;

namespace Togglr.Tests
{
    public class ConfigurationBuilder
    {
        private IFeatureToggleValueProvider _valueProvider;

        public ConfigurationBuilder()
        {
            _valueProvider = CreateDefaultValueProvider();
        }

        private IFeatureToggleValueProvider CreateDefaultValueProvider()
        {
            var filename = HttpContext.Current.Server.MapPath("~/App_Data/") + "TogglrFeatureToggles.txt";
            return new TogglrSimpleFileValueProvider(filename);
        }

        public ConfigurationBuilder WithValueProvider(IFeatureToggleValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
            return this;
        }

        public IConfiguration Build()
        {
            return new Configuration(_valueProvider);
        }
    }
}