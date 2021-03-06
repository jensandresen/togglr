namespace Togglr
{
    public static class TogglrEngine
    {
        private static FeatureToggleService _featureToggleService;

        private static FeatureToggleService ToggleService
        {
            get
            {
                if (_featureToggleService == null)
                {
                    var defaultConfiguration = new ConfigurationBuilder().Build();
                    ApplyConfiguration(defaultConfiguration);
                }

                return _featureToggleService;
            }
        }

        public static void ApplyConfiguration(IConfiguration configuration)
        {
            _featureToggleService = new FeatureToggleService(configuration.ValueProvider);
        }

        public static bool IsFeatureEnabled(string id)
        {
            return ToggleService.IsEnabled(id);
        }
    }
}