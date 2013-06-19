using Moq;

namespace Togglr.Tests.Builders
{
    internal class IFeatureToggleBuilder
    {
        private string _identity;

        public IFeatureToggleBuilder()
        {
            _identity = "a feature toggle identity";
        }

        public IFeatureToggleBuilder WithIdentity(string identity)
        {
            _identity = identity;
            return this;
        }

        public IFeatureToggle Build()
        {
            var stub = new Mock<IFeatureToggle>();
            stub.Setup(t => t.Id).Returns(_identity);

            return stub.Object;
        }
    }
}