namespace Togglr.ValueProviders
{
    public interface IFeatureToggleValueProvider
    {
        bool IsEnabled(string featureToggleIdentity);
    }
}