namespace Togglr
{
    public abstract class FeatureToggleBase : IFeatureToggle
    {
        public virtual string Id
        {
            get { return this.GetType().Name; }
        }
    }
}