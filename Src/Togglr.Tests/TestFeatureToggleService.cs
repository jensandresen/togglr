using System;
using Moq;
using NUnit.Framework;
using Togglr.Tests.Builders;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggleService
    {
        [Test]
        public void FeatureValueProvider_returns_expected()
        {
            var dummyValueProvider = new FeatureToggleValueProviderBuilder().Build();
            var sut = new FeatureToggleService(dummyValueProvider);

            Assert.AreSame(dummyValueProvider, sut.FeatureToggleValueProvider);
        }

        [Test]
        public void ctor_throws_exception_if_FeatureToggleValueProvider_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new FeatureToggleService(null));
        }

        [Test]
        public void IsEnabled_uses_FeatureToggleValueProvider_to_retrieve_value_of_feature_toggle()
        {
            var mock = new Mock<IFeatureToggleValueProvider>();

            var featureToggleStub = new IFeatureToggleBuilder().Build();

            var sut = new FeatureToggleService(mock.Object);
            sut.IsEnabled(featureToggleStub);

            mock.Verify(p => p.IsEnabled(featureToggleStub.Identity));
        }
    }
}