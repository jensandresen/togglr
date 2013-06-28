using System.IO;
using System.Reflection;
using NUnit.Framework;
using Togglr.ValueProviders;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestTogglrSimpleFileValueProvider
    {
        private string _filename;

        [SetUp]
        public void SetUp()
        {
            _filename = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filename))
            {
                File.Delete(_filename);
            }
        }

        [Test]
        public void __can_create_temp_file()
        {
            var exists = File.Exists(_filename);
            Assert.IsTrue(exists);
        }

        [Test]
        public void is_a_valueprovider()
        {
            var sut = new TogglrSimpleFileValueProvider("");
            Assert.IsInstanceOf<IFeatureToggleValueProvider>(sut);
        }

        [TestCase("toggle_id", "on", true)]
        [TestCase("toggle_id", "ON", true)]
        [TestCase("toggle_id", "off", false)]
        public void can_read_a_single_toggle(string id, string state, bool expectedState)
        {
            var content = new[]
                              {
                                  string.Format("{0}={1}", id, state),
                              };

            File.AppendAllLines(_filename, content);

            var sut = new TogglrSimpleFileValueProvider(_filename);
            var result = sut.GetById(id);

            Assert.AreEqual(expectedState, result.IsEnabled);
        }

      private Stream GetSampleTogglesAsStream()
      {
        var stream = Assembly
        .GetExecutingAssembly()
        .GetManifestResourceStream("Togglr.Tests.FeatureToggles.txt");

        return stream;
      }

      [Test]
      public void test_sample_file_format()
      {
        using (var sourceStream = GetSampleTogglesAsStream())
        {
          using (var fileStream = File.OpenWrite(_filename))
          {
            sourceStream.CopyTo(fileStream);
            fileStream.Flush();
          }
        }

        var valueProvider = new TogglrSimpleFileValueProvider(_filename);
        var toggle = valueProvider.GetById("TestFeature");

        Assert.IsNotNull(toggle);
      }

    }
}