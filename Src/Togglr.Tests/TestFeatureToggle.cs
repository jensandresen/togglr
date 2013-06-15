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
    }

    public interface IFeatureToggle
    {
    }

    public class FeatureToggle : IFeatureToggle
    {
    }
}