using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestToggleValue
    {
        [TestCase("foo=on")]
        [TestCase("foo=off")]
        [TestCase("foo=ON")]
        [TestCase("foo=OFF")]
        [TestCase("foo=on ")]
        [TestCase(" foo=on")]
        [TestCase(" foo=on ")]
        [TestCase("foo =on")]
        [TestCase("foo= on")]
        [TestCase("foo = on")]
        [TestCase(" foo = on ")]
        [TestCase("bar=on")]
        public void returns_expected_when_validating_valid_input(string input)
        {
            var result = ToggleValue.IsValid(input);
            Assert.IsTrue(result);
        }

        [TestCase("foo=true")]
        [TestCase("foo=false")]
        [TestCase("# foo=on")]
        [TestCase("! foo=on")]
        [TestCase("foo=")]
        [TestCase("=bar")]
        [TestCase("=")]
        [TestCase("foo=bar baz qux")]
        [TestCase("foo bar=baz qux")]
        [TestCase("foo bar baz qux")]
        [TestCase("foo bar ! baz qux")]
        public void returns_expected_when_validating_invalid_input(string input)
        {
            var result = ToggleValue.IsValid(input);
            Assert.IsFalse(result);
        }

        [TestCase("foo=on # comment")]
        [TestCase("foo=on         # comment")]
        [TestCase("foo=on # !!!! comment")]
        [TestCase("foo=on #comment")]
        [TestCase("foo=on #")]
        [TestCase("foo=on ##########")]
        public void supports_trailing_comments(string input)
        {
            var result = ToggleValue.IsValid(input);
            Assert.IsTrue(result);
        }

        [TestCase("foo=on")]
        [TestCase("foo=off")]
        [TestCase("bar=on")]
        [TestCase("bar=off")]
        public void returns_expected_parsing_result_when_parsing_valid_input(string input)
        {
            ToggleValue dummy;
            var result = ToggleValue.TryParse(input, out dummy);

            Assert.IsTrue(result);
        }

        [TestCase("# foo=on")]
        [TestCase("foo ! off")]
        [TestCase("bar : on")]
        [TestCase("bar_off")]
        public void returns_expected_parsing_result_when_parsing_invalid_input(string input)
        {
            ToggleValue dummy;
            var result = ToggleValue.TryParse(input, out dummy);

            Assert.IsFalse(result);
        }

        [Test]
        public void returns_expected_when_comparing_two_equal_instances()
        {
            var first = new ToggleValue("foo", true);
            var second = new ToggleValue("foo", true);

            Assert.AreEqual(first, second);
        }

        [TestCase("foo=on", "foo", true)]
        [TestCase("foo=off", "foo", false)]
        [TestCase("bar=on", "bar", true)]
        [TestCase("bar=off", "bar", false)]
        [TestCase("   foo=on", "foo", true)]
        [TestCase("foo = on", "foo", true)]
        [TestCase("foo=on # comment", "foo", true)]
        public void returns_expected_toggle_value_when_parsing_valid_input(string input, string expectedName, bool expectedState)
        {
            ToggleValue result;
            ToggleValue.TryParse(input, out result);

            var expected = new ToggleValue(expectedName, expectedState);

            Assert.AreEqual(expected, result);
        }

        [TestCase("foo")]
        [TestCase("# foo=on")]
        [TestCase("foo ! off")]
        [TestCase("bar : on")]
        [TestCase("bar_off")]
        public void returns_expected_toggle_value_when_parsing_invalid_input(string input)
        {
            ToggleValue result;
            ToggleValue.TryParse(input, out result);

            Assert.IsNull(result);
        }
    }
}