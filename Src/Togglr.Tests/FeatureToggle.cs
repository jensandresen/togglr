namespace Togglr.Tests
{
    public class FeatureToggle : IFeatureToggle
    {
        public string Identity
        {
            get { return this.GetType().Name; }
        }
    }
}