using System.IO;
using System.Web;
using Togglr.ValueProviders;

namespace Togglr
{
    public class ConfigurationBuilder
    {
        private IFeatureToggleValueProvider _valueProvider;
        private bool _enableRequestOverrides;

        public ConfigurationBuilder()
        {
            _valueProvider = CreateDefaultValueProvider();
            _enableRequestOverrides = true;
        }

        private IFeatureToggleValueProvider CreateDefaultValueProvider()
        {
            const string fileName = "featuretoggles.txt";

            var folderName = HttpContext.Current != null
                               ? HttpContext.Current.Server.MapPath("~/App_Data/")
                               : Path.GetFullPath(@".\");

            var fullPath = Path.Combine(folderName, fileName);

            return new TogglrSimpleFileValueProvider(fullPath);
        }

        public ConfigurationBuilder WithValueProvider(IFeatureToggleValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
            return this;
        }

        public ConfigurationBuilder DisableRequestOverrides()
        {
            _enableRequestOverrides = false;
            return this;
        }

        public IConfiguration Build()
        {
            var temp = _valueProvider;

            if (_enableRequestOverrides)
            {
                temp = new RequestFeatureValueProviderDecorator(temp);
            }

            return new Configuration(temp);
        }
    }
}