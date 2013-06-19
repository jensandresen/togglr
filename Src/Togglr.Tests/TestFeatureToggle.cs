using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggle
    {
        [Test]
        public void is_instance_of_IFeatureToggle()
        {
            var sut = new FeatureToggle();
            Assert.IsInstanceOf<IFeatureToggle>(sut);
        }

        [Test]
        public void identity_returns_name_of_class()
        {
            var sut = new FeatureToggle();
            Assert.AreEqual("FeatureToggle", sut.Id);
        }
    }
}