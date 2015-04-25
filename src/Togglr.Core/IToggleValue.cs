namespace Togglr
{
    public interface IToggleValue
    {
        string Name { get; }
        bool IsEnabled { get; }
    }
}