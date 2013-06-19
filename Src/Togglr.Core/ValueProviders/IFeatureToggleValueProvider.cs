namespace Togglr.ValueProviders
{
    public interface IFeatureToggleValueProvider
    {
        FeatureToggleValue GetById(string id);
    }
}