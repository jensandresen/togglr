namespace Togglr.Tests
{
    public interface IFeatureToggleValueProvider
    {
        bool IsEnabled(string featureToggleIdentity);
    }
}