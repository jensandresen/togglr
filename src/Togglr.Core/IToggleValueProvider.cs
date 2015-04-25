namespace Togglr
{
    public interface IToggleValueProvider
    {
        IToggleValue Get(string id);
    }
}