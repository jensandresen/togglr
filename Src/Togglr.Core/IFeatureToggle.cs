namespace Togglr
{
    public interface IFeatureToggle
    {
        string Identity { get; }
        bool IsEnabled { get; }
    }
}