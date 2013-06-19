namespace Togglr.ValueProviders
{
    public interface IFeatureToggleValueProvider
    {
        FeatureToggleValue GetByIdentitier(string id);
    }
}