using Moq;

namespace Togglr.Tests.Builders
{
    internal class FeatureToggleValueProviderBuilder
    {
        public IFeatureToggleValueProvider Build()
        {
            return new Mock<IFeatureToggleValueProvider>().Object;
        }
    }
}