using System.IO;
using System.Text;
using NUnit.Framework;
using Togglr.Files;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFileBasedToggleValueProvider
    {
        private string _filePath;
        private FileBasedToggleValueProvider _sut;

        [SetUp]
        public void SetUp()
        {
            _filePath = Path.GetTempFileName();
            _sut = new FileBasedToggleValueProvider(_filePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void returns_expected_when_file_is_empty()
        {
            var result = _sut.Get("foo");
            Assert.IsNull(result);
        }

        [TestCase("foo=on", "foo", true)]
        [TestCase("foo=off", "foo", false)]
        [TestCase("bar=on", "bar", true)]
        [TestCase("bar=off", "bar", false)]
        public void returns_expected_when_file_contains_requested_toggle(string input, string expectedName, bool expectedState)
        {
            File.WriteAllText(_filePath, input, Encoding.UTF8);
            var result = _sut.Get(expectedName);

            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedState, result.IsEnabled);
        }

        [Test]
        public void returns_expected_when_file_does_not_contain_requested_toggle()
        {
            File.WriteAllText(_filePath, "foo=on", Encoding.UTF8);
            var result = _sut.Get("bar");

            Assert.IsNull(result);
        }

        [Test]
        public void toggle_names_are_case_insensitive()
        {
            File.WriteAllText(_filePath, "foo=on", Encoding.UTF8);
            var result = _sut.Get("FOO");

            Assert.AreEqual("foo", result.Name);
            Assert.AreEqual(true, result.IsEnabled);
        }
    }
}