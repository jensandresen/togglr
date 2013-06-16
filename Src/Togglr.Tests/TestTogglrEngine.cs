using Moq;
using NUnit.Framework;
using Togglr.Tests.Builders;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestTogglrEngine
    {
        [Test]
        public void full_test()
        {
            var valueProviderStub = new FeatureToggleValueProviderBuilder()
                .WithFeatureToggle("f1", true)
                .WithFeatureToggle("f2", false)
                .Build();

            var cfg = new ConfigurationBuilder()
                .WithValueProvider(valueProviderStub)
                .Build();

            TogglrEngine.ApplyConfiguration(cfg);

            Assert.IsTrue(TogglrEngine.IsFeatureEnabled("f1"));
            Assert.IsFalse(TogglrEngine.IsFeatureEnabled("f2"));
        }

    }
}