namespace Togglr.Tests
{
    public interface IConfiguration
    {
        IFeatureToggleValueProvider ValueProvider { get; }
    }
}