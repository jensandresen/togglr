using System;
using NUnit.Framework;
using Togglr.ValueProviders;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggleValue
    {
        [Test]
        public void Id_is_expected()
        {
            string id = Guid.NewGuid().ToString();
            const bool dummyEnabledState = true;

            var sut = new FeatureToggleValue(id, dummyEnabledState);

            Assert.AreEqual(id, sut.Id);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsEnabled_is_expected(bool expectedState)
        {
            const string dummyId = "dummy id";
            var sut = new FeatureToggleValue(dummyId, expectedState);

            Assert.AreEqual(expectedState, sut.IsEnabled);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("                    ")]
        public void ctor_throws_exception_when_id_is_illegal(string illigalId)
        {
            const bool dummyEnabledState = true;
            Assert.Throws<ArgumentException>(() => new FeatureToggleValue(illigalId, dummyEnabledState));
        }

        [Test]
        public void HasId_returns_true_if_direct_match()
        {
            const string id = "TestFeatureToggle";
            const bool dummyState = true;
            
            var sut = new FeatureToggleValue(id, dummyState);
            var result = sut.HasId(id);

            Assert.IsTrue(result);
        }

        [Test]
        public void HasId_ignores_case()
        {
            const string id = "TestFeatureToggle";
            const bool dummyState = true;

            var sut = new FeatureToggleValue(id, dummyState);
            var result = sut.HasId("TESTFEATURETOGGLE");

            Assert.IsTrue(result);
        }

        [TestCase("Test", "TestFeatureToggle")]
        [TestCase("TestFeatureToggle", "Test")]
        [TestCase("Test", "TESTFEATURETOGGLE")]
        public void Has_Id_ignores_the_words_FeatureToggle_in_the_id(string id, string otherId)
        {
            const bool dummyState = true;

            var sut = new FeatureToggleValue(id, dummyState);
            var result = sut.HasId(otherId);

            Assert.IsTrue(result);
        }

        [TestCase("Test=on", "Test", true)]
        [TestCase("Test=off", "Test", false)]
        [TestCase("OtherTest=on", "OtherTest", true)]
        [TestCase("Test=[not valid, will default to false]", "Test", false)]
        public void Parse_returns_expected(string text, string expectedId, bool expectedState)
        {
            var sut = FeatureToggleValue.Parse(text);

            Assert.AreEqual(expectedId, sut.Id);
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