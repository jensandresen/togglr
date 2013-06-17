using Togglr.ValueProviders;

namespace Togglr
{
    public interface IConfiguration
    {
        IFeatureToggleValueProvider ValueProvider { get; }
    }
}