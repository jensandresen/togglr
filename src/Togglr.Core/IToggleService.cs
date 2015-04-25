namespace Togglr
{
    public interface IToggleService
    {
        bool IsEnabled(IFeatureToggle featureToggle);
    }

    public class ToggleService : IToggleService
    {
        private readonly IToggleValueProvider _toggleValueProvider;

        public ToggleService(IToggleValueProvider toggleValueProvider)
        {
            _toggleValueProvider = toggleValueProvider;
        }

        public bool IsEnabled(IFeatureToggle featureToggle)
        {
            var value = _toggleValueProvider.Get(featureToggle.Id);

            if (value == null)
            {
                return false;
            }

            return value.IsEnabled;
        }
    }
}