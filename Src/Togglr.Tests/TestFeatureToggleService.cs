using System;
using Moq;
using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggleService
    {
        [Test]
        public void FeatureValueProvider_returns_expected()
        {
            var dummyValueProvider = new Mock<IFeatureToggleValueProvider>().Object;
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
            const string featureToggleIdentity = "a feature toggle identifier";

            var mock = new Mock<IFeatureToggleValueProvider>();

            var featureToggleStub = new Mock<IFeatureToggle>();
            featureToggleStub.Setup(t => t.Identity).Returns(featureToggleIdentity);

            var sut = new FeatureToggleService(mock.Object);
            sut.IsEnabled(featureToggleStub.Object);

            mock.Verify(p => p.IsEnabled(featureToggleIdentity));
        }
    }

    public class FeatureToggleService
    {
        private readonly IFeatureToggleValueProvider _featureToggleValueProvider;

        public FeatureToggleService(IFeatureToggleValueProvider featureToggleValueProvider)
        {
            if (featureToggleValueProvider == null)
            {
                throw new ArgumentNullException("featureToggleValueProvider");
            }

            _featureToggleValueProvider = featureToggleValueProvider;
        }

        public IFeatureToggleValueProvider FeatureToggleValueProvider
        {
            get { return _featureToggleValueProvider; }
        }

        public bool IsEnabled(IFeatureToggle featureToggle)
        {
            return _featureToggleValueProvider.IsEnabled(featureToggle.Identity);
        }
    }

    public interface IFeatureToggleValueProvider
    {
        bool IsEnabled(string featureToggleIdentity);
    }
}