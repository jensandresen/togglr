using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFileValueParser
    {
        [Test]
        public void returns_expected_when_parsing_empty_content()
        {
            var sut = new FileValueParser();
            var result = sut.Parse(new[] { string.Empty });

            CollectionAssert.IsEmpty(result);
        }

        [TestCase("foo", true)]
        [TestCase("bar", true)]
        [TestCase("foo", false)]
        [TestCase("bar", false)]
        public void returns_expected_when_parsing_single_value(string expectedName, bool expectedState)
        {
            var input = string.Format("{0}={1}", expectedName, expectedState ? "on" : "off");

            var sut = new FileValueParser();
            var result = sut.Parse(new[] { input });

            var expected = new ToggleValue(expectedName, expectedState);

            CollectionAssert.AreEquivalent(new[] {expected}, result);
        }

        [Test]
        public void returns_expected_when_parsing_multiple_values()
        {
            var input = new[]
            {
                "foo=on",
                "bar=off",
            };

            var expected = new[]
            {
                new ToggleValue("foo", true),
                new ToggleValue("bar", false),
            };

            var sut = new FileValueParser();
            var result = sut.Parse(input);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void returns_expected_when_input_contains_comments()
        {
            var input = new[]
            {
                "# comment",
                "foo=on",
                "bar=off",
            };

            var expected = new[]
            {
                new ToggleValue("foo", true),
                new ToggleValue("bar", false),
            };

            var sut = new FileValueParser();
            var result = sut.Parse(input);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void returns_expected_when_input_contains_trailing_comments()
        {
            var input = new[]
            {
                "foo=on # trailing comment",
                "bar=off",
            };

            var expected = new[]
            {
                new ToggleValue("foo", true),
                new ToggleValue("bar", false),
            };

            var sut = new FileValueParser();
            var result = sut.Parse(input);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void returns_expected_when_input_contains_empty_lines()
        {
            var input = new[]
            {
                "foo=on",
                "",
                "         ",
                "bar=off",
            };

            var expected = new[]
            {
                new ToggleValue("foo", true),
                new ToggleValue("bar", false),
            };

            var sut = new FileValueParser();
            var result = sut.Parse(input);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void returns_expected_when_input_contains_invalid_lines()
        {
            var input = new[]
            {
                "foo=on",
                "!",
                "123",
                "qwertyuiop",
                "bar=off",
            };

            var expected = new[]
            {
                new ToggleValue("foo", true),
                new ToggleValue("bar", false),
            };

            var sut = new FileValueParser();
            var result = sut.Parse(input);

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}