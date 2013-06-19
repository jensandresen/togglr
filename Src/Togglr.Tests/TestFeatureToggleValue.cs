using System;
using NUnit.Framework;
using Togglr.ValueProviders;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggleValue
    {
        [Test]
        public void Identity_is_expected()
        {
            string identity = Guid.NewGuid().ToString();
            const bool dummyEnabledState = true;

            var sut = new FeatureToggleValue(identity, dummyEnabledState);

            Assert.AreEqual(identity, sut.Identity);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsEnabled_is_expected(bool expectedState)
        {
            const string dummyIdentity = "dummy identifier";
            var sut = new FeatureToggleValue(dummyIdentity, expectedState);

            Assert.AreEqual(expectedState, sut.IsEnabled);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("                    ")]
        public void ctor_throws_exception_when_identifier_is_illegal(string illigalIdentifier)
        {
            const bool dummyEnabledState = true;
            Assert.Throws<ArgumentException>(() => new FeatureToggleValue(illigalIdentifier, dummyEnabledState));
        }

        [Test]
        public void HasIdentity_returns_true_if_direct_match()
        {
            const string identity = "TestFeatureToggle";
            const bool dummyState = true;
            
            var sut = new FeatureToggleValue(identity, dummyState);
            var result = sut.HasIdentity(identity);

            Assert.IsTrue(result);
        }

        [Test]
        public void HasIdentity_ignores_case()
        {
            const string identity = "TestFeatureToggle";
            const bool dummyState = true;

            var sut = new FeatureToggleValue(identity, dummyState);
            var result = sut.HasIdentity("TESTFEATURETOGGLE");

            Assert.IsTrue(result);
        }

        [TestCase("Test", "TestFeatureToggle")]
        [TestCase("TestFeatureToggle", "Test")]
        [TestCase("Test", "TESTFEATURETOGGLE")]
        public void Has_Identity_ignores_the_words_FeatureToggle_in_the_identifier(string identity, string otherIdentity)
        {
            const bool dummyState = true;

            var sut = new FeatureToggleValue(identity, dummyState);
            var result = sut.HasIdentity(otherIdentity);

            Assert.IsTrue(result);
        }

        [TestCase("Test=on", "Test", true)]
        [TestCase("Test=off", "Test", false)]
        [TestCase("OtherTest=on", "OtherTest", true)]
        [TestCase("Test=[not valid, will default to false]", "Test", false)]
        public void Parse_returns_expected(string text, string expectedIdentity, bool expectedState)
        {
            var sut = FeatureToggleValue.Parse(text);

            Assert.AreEqual(expectedIdentity, sut.Identity);
            Assert.AreEqual(expectedState, sut.IsEnabled);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("                    ")]
        public void Parse_throws_exception_if_text_is_illegal(string illegalText)
        {
            Assert.Throws<ArgumentException>(() => FeatureToggleValue.Parse(illegalText));
        }

        [TestCase("illegal format")]
        [TestCase("=")]
        public void Parse_throws_exception_if_text_is_in_wrong_format(string text)
        {
            Assert.Throws<FormatException>(() => FeatureToggleValue.Parse(text));
        }

    }
}