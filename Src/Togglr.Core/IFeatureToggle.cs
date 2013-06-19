namespace Togglr
{
    public interface IFeatureToggle
    {
        string Id { get; }
        bool IsEnabled { get; }
    }
}