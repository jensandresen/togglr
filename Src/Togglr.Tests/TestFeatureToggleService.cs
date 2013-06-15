﻿using System;
using Moq;
using NUnit.Framework;

namespace Togglr.Tests
{
    [TestFixture]
    public class TestFeatureToggleService
    {
        [Test]
        public void FeatureValueProvider_returns_expected()
        {
            var dummyValueProvider = new FeatureToggleValueProviderBuilder().Build();
            var sut = new FeatureToggleService(dummyValueProvider);

            Assert.AreSame(dummyValueProvider, sut.FeatureToggleValueProvider);
        }

        [Test]
        public void ctor_throws_exception_if_FeatureToggleValueProvider_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new FeatureToggleService(null));
        }

        [Test]
        public void IsEnabled_uses_FeatureToggleValueProvider_to_retrieve_value_of_feature_toggle()
        {
            var mock = new Mock<IFeatureToggleValueProvider>();

            var featureToggleStub = new IFeatureToggleBuilder().Build();

            var sut = new FeatureToggleService(mock.Object);
            sut.IsEnabled(featureToggleStub);

            mock.Verify(p => p.IsEnabled(featureToggleStub.Identity));
        }
    }

    public class FeatureToggleService
    {
        private readonly IFeatureToggleValueProvider _featureToggleValueProvider;

        public FeatureToggleService(IFeatureToggleValueProvider featureToggleValueProvider)
        {
            if (featureToggleValueProvider == null)
            {
                throw new ArgumentNullException("featureToggleValueProvider");
            }

            _featureToggleValueProvider = featureToggleValueProvider;
        }

        public IFeatureToggleValueProvider FeatureToggleValueProvider
        {
            get { return _featureToggleValueProvider; }
        }

        public bool IsEnabled(IFeatureToggle featureToggle)
        {
            return _featureToggleValueProvider.IsEnabled(featureToggle.Identity);
        }
    }

    public interface IFeatureToggleValueProvider
    {
        bool IsEnabled(string featureToggleIdentity);
    }

    internal class FeatureToggleValueProviderBuilder
    {
        public IFeatureToggleValueProvider Build()
        {
            return new Mock<IFeatureToggleValueProvider>().Object;
        }
    }

    internal class IFeatureToggleBuilder
    {
        private string _identity;

        public IFeatureToggleBuilder()
        {
            _identity = "a feature toggle identity";
        }

        public IFeatureToggleBuilder WithIdentity(string identity)
        {
            _identity = identity;
            return this;
        }

        public IFeatureToggle Build()
        {
            var stub = new Mock<IFeatureToggle>();
            stub.Setup(t => t.Identity).Returns(_identity);

            return stub.Object;
        }
    }
}