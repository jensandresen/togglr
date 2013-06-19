namespace Togglr
{
    public class FeatureToggle : IFeatureToggle
    {
        public virtual string Id
        {
            get { return this.GetType().Name; }
        }

        public virtual bool IsEnabled
        {
            get { return TogglrEngine.IsFeatureEnabled(this.Id); }
        }
    }
}