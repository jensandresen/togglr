namespace Togglr
{
    public static class TogglrEngine
    {
        private static IToggleService _toggleService;

        public static bool IsEnabled<T>() where T : IFeatureToggle, new()
        {
            var featureToggle = new T();
            return IsEnabled(featureToggle);
        }

        public static bool IsEnabled(IFeatureToggle featureToggle)
        {
            return ToggleService.IsEnabled(featureToggle);
        }

        private static IToggleService ToggleService
        {
            get
            {
                return _toggleService;
            }
        }

        public static void SetToggleService(IToggleService toggleService)
        {
            _toggleService = toggleService;
        }
    }
}