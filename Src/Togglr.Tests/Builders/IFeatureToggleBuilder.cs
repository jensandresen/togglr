using Moq;

namespace Togglr.Tests.Builders
{
    internal class IFeatureToggleBuilder
    {
        private string _id;

        public IFeatureToggleBuilder()
        {
            _id = "a feature toggle identity";
        }

        public IFeatureToggleBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public IFeatureToggle Build()
        {
            var stub = new Mock<IFeatureToggle>();
            stub.Setup(t => t.Id).Returns(_id);

            return stub.Object;
        }
    }
}