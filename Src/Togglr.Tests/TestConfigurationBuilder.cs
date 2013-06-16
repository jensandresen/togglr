using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestConfigurationBuilder
    {
        [Test]
        public void Build_returns_an_initialized_configuration()
        {
            var cfg = new ConfigurationBuilder().Build();
            Assert.IsNotNull(cfg);
        }
    }
}