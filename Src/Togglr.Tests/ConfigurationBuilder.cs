using System.IO;
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
            const string fileName = "TogglrFeatureToggles.txt";

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

        public IConfiguration Build()
        {
            return new Configuration(_valueProvider);
        }
    }
}