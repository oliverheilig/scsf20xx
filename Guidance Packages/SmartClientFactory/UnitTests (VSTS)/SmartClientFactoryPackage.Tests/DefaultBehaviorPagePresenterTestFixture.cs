//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for DefaultBehaviorPagePresenterTestFixture
	/// </summary>
	[TestClass]
	public class DefaultBehaviorPagePresenterTestFixture
	{
		public DefaultBehaviorPagePresenterTestFixture()
		{
		}

		private DefaultBehaviorPagePresenter presenter;
		private MockDefaultBehaviorPage view;
		private IDictionaryService dictionary;
		private DefaultBehaviorPageModel model;

		[TestInitialize]
		public void Setup()
		{
			dictionary = new MockDictionaryService();
			model = new DefaultBehaviorPageModel(dictionary, null);
			view = new MockDefaultBehaviorPage();
			presenter = new DefaultBehaviorPagePresenter(view, model);
		}

		[TestMethod]
		public void ShouldUpdateModelOnStampsChange()
		{
			view.FireStampsChanging("5");

			Assert.AreEqual("5", model.Stamps);
		}

		[TestMethod]
		public void ModelIsNotValidOnInvalidStamps()
		{
			model.Stamps = "invalid";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ShouldUpdateModelOnMaxRetriesChange()
		{
			view.FireMaxRetriesChanging("1");

			Assert.AreEqual("1", model.MaxRetries);
		}

		[TestMethod]
		public void ModelIsNotValidOnInvalidMaxRetries()
		{
			model.MaxRetries = "invalid";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ShouldUpdateModelOnExpirationChange()
		{
			view.FireExpirationChanging("01.13:14:15");

			Assert.AreEqual("01.13:14:15", model.Expiration);
		}

		[TestMethod]
		public void ModelIsNotValidOnInvalidExpiration()
		{
			model.Expiration = "invalid";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ShouldUpdateModelOnTagChange()
		{
			view.FireTagChanging("SomeTag");

			Assert.AreEqual("SomeTag", model.Tag);
		}

		[TestMethod]
		public void ModelIsValidWithValidArguments()
		{
			dictionary.SetValue("Stamps", 1);
			dictionary.SetValue("MaxRetries", 1);
			dictionary.SetValue("Expiration", TimeSpan.Parse("00.00:00:30"));
			dictionary.SetValue("ProxyFactoryTypeFullName", typeof (MockProxyFactory).FullName);

			Assert.IsTrue(model.IsValid);
		}

		[TestMethod]
		public void ShowStampsOnViewReady()
		{
            dictionary.SetValue("ShowAdvancedSettings", true);
            dictionary.SetValue("Stamps", 1);
			presenter.OnViewReady();

			Assert.AreEqual("1", view.Stamps);
		}

		[TestMethod]
		public void ShowMaxRetriesOnViewReady()
		{
            dictionary.SetValue("ShowAdvancedSettings", true);
            dictionary.SetValue("MaxRetries", 1);
			presenter.OnViewReady();

			Assert.AreEqual("1", view.MaxRetries);
		}

		[TestMethod]
		public void ShowExpirationOnViewReady()
		{
            dictionary.SetValue("ShowAdvancedSettings",true);
			dictionary.SetValue("Expiration", TimeSpan.Parse("00.00:00:30"));
			presenter.OnViewReady();

			Assert.AreEqual("00.00:00:30", view.Expiration);
		}

		[TestMethod]
		public void ShowTagOnViewReady()
		{
            dictionary.SetValue("ShowAdvancedSettings", true);
            dictionary.SetValue("Tag", "TagValue");
			presenter.OnViewReady();

			Assert.AreEqual("TagValue", view.TagValue);
		}

		[TestMethod]
		public void ShowProxyFactoryTypeOnViewReady()
		{
            dictionary.SetValue("ShowAdvancedSettings", true);
            dictionary.SetValue("ProxyFactoryTypeFullName", typeof(MockProxyFactory).FullName);

			presenter.OnViewReady();

			Assert.AreEqual(typeof (MockProxyFactory).FullName, view.ProxyFactoryTypeFullName);
		}
	}

	internal class MockProxyFactory : IProxyFactory
	{
		#region IProxyFactory Members

		public object GetOnlineProxy(Request request, string networkName)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}

	internal class MockDefaultBehaviorPage : IDefaultBehaviorPage
	{
		public event EventHandler<EventArgs> ExpirationChanged;
		public event EventHandler<EventArgs> MaxRetriesChanged;
		public event EventHandler<EventArgs> StampsChanged;
		public event EventHandler<EventArgs> TagChanged;
		public event EventHandler<EventArgs> ProxyFactoryTypeFullNameChanged;
		public event EventHandler<EventArgs> WizardActivated;
		public event EventHandler<EventArgs<bool>> RequestingValidation;

		private string _expiration;
		private string _maxRetries;
		private string _stamps;
		private string _tag;
		public string _proxyFactoryTypeFullName;
        public bool _advancedSettingsSkipped;

		public string Expiration
		{
			get { return _expiration; }
		}

		public string MaxRetries
		{
			get { return _maxRetries; }
		}

		public string Stamps
		{
			get { return _stamps; }
		}

		public string TagValue
		{
			get { return _tag; }
		}

		public string ProxyFactoryTypeFullName
		{
			get { return _proxyFactoryTypeFullName; }
		}

		public void FireStampsChanging(string value)
		{
			_stamps = value;
			if (StampsChanged != null)
				StampsChanged(this, new EventArgs());
		}

		public void FireMaxRetriesChanging(string value)
		{
			_maxRetries = value;
			if (MaxRetriesChanged != null)
				MaxRetriesChanged(this, new EventArgs());
		}

		public void FireExpirationChanging(string value)
		{
			_expiration = value;
			if (ExpirationChanged != null)
				ExpirationChanged(this, new EventArgs());
		}

		public void FireProxyFactoryTypeChanged(string proxyFactoryTypeName)
		{
			_proxyFactoryTypeFullName = proxyFactoryTypeName;
			if (ProxyFactoryTypeFullNameChanged != null)
				ProxyFactoryTypeFullNameChanged(this, new EventArgs());
		}

		public void FireTagChanging(string value)
		{
			_tag = value;
			if (TagChanged != null)
				TagChanged(this, new EventArgs());
		}

		internal void FireWizardActivated()
		{
			if (WizardActivated != null)
				WizardActivated(this, new EventArgs());
		}

		internal void FireRequestingValidation()
		{
			if (RequestingValidation != null)
				RequestingValidation(this, new EventArgs<bool>());
		}

		public void ShowExpiration(string expiration)
		{
			_expiration = expiration;
		}

		public void ShowMaxRetries(string maxRetries)
		{
			_maxRetries = maxRetries;
		}

		public void ShowStamps(string stamps)
		{
			_stamps = stamps;
		}

		public void ShowTag(string tag)
		{
			_tag = tag;
		}

		public void ShowProxyFactoryTypeFullName(string proxyFactoryTypeFullName)
		{
			_proxyFactoryTypeFullName = proxyFactoryTypeFullName;
		}
    }
}