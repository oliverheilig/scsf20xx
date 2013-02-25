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
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for EndpointPagePresenterTestFixture
	/// </summary>
	[TestClass]
	public class EndpointPagePresenterTestFixture
	{
		public EndpointPagePresenterTestFixture()
		{
		}

		private EndpointPagePresenter presenter;
		private MockEndpointPage view;
		private IDictionaryService dictionary;
		private MockEndpointPageModel mockModel;
		private EndpointPageModel model;

		[TestInitialize]
		public void Setup()
		{
			dictionary = new MockDictionaryService();
			mockModel = new MockEndpointPageModel();
			view = new MockEndpointPage();
			presenter = new EndpointPagePresenter(view, mockModel);
			model = new EndpointPageModel(dictionary, null);
		}

		[TestMethod]
		public void ShowEndpointOnViewReady()
		{
			mockModel.Endpoint = "Endpoint1";

			presenter.OnViewReady();

			Assert.AreEqual(view.EndpointDisplayed, "Endpoint1");
		}

		[TestMethod]
		public void ShowProxyTypeOnViewReady()
		{
			mockModel.ProxyType = typeof (String);

			presenter.OnViewReady();

			Assert.AreEqual(view.ProxyType, typeof (String));
		}

		[TestMethod]
		public void ShowTypeMethodsListOnViewReady()
		{
			mockModel.OriginalTypeMethods = new List<MethodInfo>(typeof (MockProxyType).GetMethods());

			presenter.OnViewReady();

			Assert.IsTrue(view.IsMethodsShown);
			Assert.IsTrue(
				view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {typeof (object)})));
			Assert.IsTrue(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {})));
			Assert.IsTrue(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void ShowTypeMethodsListOnProxyTypeNameChanged()
		{
			mockModel.OriginalTypeMethods = new List<MethodInfo>(typeof (MockProxyType).GetMethods());

			view.FireProxyTypeChanged(typeof (MockProxyType).FullName);

			Assert.IsTrue(view.IsMethodsShown);
			Assert.IsTrue(
				view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {typeof (object)})));
			Assert.IsTrue(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {})));
			Assert.IsTrue(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void ShowOnlyWsdlOperationsOnViewReady()
		{
			mockModel.OriginalTypeMethods = new List<MethodInfo>(typeof (MockProxyType).GetMethods());
			mockModel.ServiceAgentMethods = mockModel.OriginalTypeMethods;
			mockModel.Operations = new List<string>(new string[] {"Foo"});

			presenter.OnViewReady();

			Assert.IsTrue(view.IsMethodsShown);
			Assert.IsFalse(
				view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {typeof (object)})));
			Assert.IsFalse(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("HelloWorld", new Type[] {})));
			Assert.IsTrue(view.MethodsShown.Contains(typeof (MockProxyType).GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void SetLanguageOnViewReady()
		{
			mockModel.Language = "CS";
			presenter.OnViewReady();

			Assert.AreEqual(mockModel.Language, view.Language);
			Assert.AreEqual("CS", view.Language);
		}

		[TestMethod]
		public void ShowsolutionPreviewOnViewReady()
		{
			presenter.OnViewReady();

			Assert.IsTrue(view.PreviewRefreshed);
		}

		[TestMethod]
		public void ShowDsaFoldersOnViewReady()
		{
			mockModel.Language = "cs";
			mockModel.DisconnectedAgentsFolder = "DisconnectedAgents";
			mockModel.ProxyFolder = "MockType";

			presenter.OnViewReady();

			Assert.AreEqual("DisconnectedAgents", view.DisconnectedAgentsFolder);
			Assert.AreEqual("MockType", view.ProxyFolder);
			Assert.AreEqual("MockTypeAgent", view.AgentFileName);
			Assert.AreEqual("MockTypeAgentCallback", view.AgentCallbackFileName);
			Assert.AreEqual("MockTypeAgentCallbackbase", view.AgentCallbackBaseFileName);
		}

		[TestMethod]
		public void ShouldUpdateModelOnProxyTypeNameChange()
		{
			view.FireProxyTypeChanged("System.String");

			Assert.AreEqual("System.String", mockModel.ProxyTypeName);
		}

		[TestMethod]
		public void ShouldUpdateModelProxyTypeOnProxyTypeNameChange()
		{
			model.ProxyTypeName = "System.String";

			Assert.AreEqual(typeof (String), model.ProxyType);
		}

		[TestMethod]
		public void ShouldUpdateModelGenericProxyTypeOnProxyTypeNameChange()
		{
			model.ProxyTypeName = "System.Collections.Generic.Dictionary<T,D>";

			Assert.AreEqual(typeof (Dictionary<,>), model.ProxyType);
		}

		[TestMethod]
		public void ShouldUpdateModelOnMethodsChange()
		{
			mockModel.OriginalTypeMethods = new List<MethodInfo>(typeof (MockProxyType).GetMethods());
			List<MethodInfo> methods = new List<MethodInfo>();
			methods.Add(typeof (MockProxyType).GetMethod("Foo"));

			view.FireMethodsChanged(methods);

			Assert.AreNotEqual(mockModel.ServiceAgentMethods, null);
			Assert.AreEqual(1, mockModel.ServiceAgentMethods.Count);
			Assert.IsTrue(mockModel.ServiceAgentMethods.Contains(typeof (MockProxyType).GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptyEndpoint()
		{
			model.Endpoint = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullProxyType()
		{
			model.ProxyType = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsValidWithValidArguments()
		{
			dictionary.SetValue("Built", true);
			dictionary.SetValue("ProxyType", typeof (MockProxyType));
			dictionary.SetValue("Endpoint", "AnyEndpoint");
			List<MethodInfo> methods = new List<MethodInfo>();
			methods.Add(typeof (MockProxyType).GetMethod("Foo", new Type[] {}));
			dictionary.SetValue("ServiceAgentMethods", methods);

            dictionary.SetValue("Stamps", 1);
            dictionary.SetValue("MaxRetries", 1);
            dictionary.SetValue("Expiration", TimeSpan.Parse("00.00:00:30"));
            dictionary.SetValue("ProxyFactoryTypeFullName", typeof(MockProxyFactory).FullName);

			Assert.IsTrue(model.IsValid);
		}

        [TestMethod]
        public void ShouldUpdateModelOnStampsChange()
        {
            view.FireStampsChanging("5");

            Assert.AreEqual("5", mockModel.Stamps);
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

            Assert.AreEqual("1", mockModel.MaxRetries);
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

            Assert.AreEqual("01.13:14:15", mockModel.Expiration);
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

            Assert.AreEqual("SomeTag", mockModel.Tag);
        }

        [TestMethod]
        public void ShowStampsOnViewReady()
        {
            mockModel.Stamps = "1";
            presenter.OnViewReady();

            Assert.AreEqual("1", view.Stamps);
        }

        [TestMethod]
        public void ShowMaxRetriesOnViewReady()
        {
            mockModel.MaxRetries= "1";
            presenter.OnViewReady();

            Assert.AreEqual("1", view.MaxRetries);
        }

        [TestMethod]
        public void ShowExpirationOnViewReady()
        {
            mockModel.Expiration = "00.00:00:30";
            presenter.OnViewReady();

            Assert.AreEqual("00.00:00:30", view.Expiration);
        }

        [TestMethod]
        public void ShowTagOnViewReady()
        {
            mockModel.Tag = "TagValue";
            presenter.OnViewReady();

            Assert.AreEqual("TagValue", view.TagValue);
        }

        [TestMethod]
        public void ShowProxyFactoryTypeOnViewReady()
        {
            mockModel.ProxyFactoryTypeFullName=typeof(MockProxyFactory).FullName;

            presenter.OnViewReady();

            Assert.AreEqual(typeof(MockProxyFactory).FullName, view.ProxyFactoryTypeFullName);
        }


		private class MockProxyType
		{
			public void HelloWorld()
			{
			}

			public void HelloWorld(object foo)
			{
			}

			public string Foo()
			{
				return "Bar";
			}
		}

		private class MockGenericClass<T>
		{
		}

		private class MockEndpointPage : IEndpointPage
		{
			public string EndpointDisplayed;
			public Type ProxyTypeDisplayed;
			public bool IsMethodsShown;
			public List<MethodInfo> MethodsShown;
			public List<MethodInfo> SelectedMethodsShown;
			private string _language;
			private bool _previewRefreshed;
			private string _disconnectedAgentsFolder;
			private string _proxyFolder;
			private string _agentFileName;
			private string _agentCallbackFileName;
			private string _agentCallbackBaseFileName;
			private string _proxyTypeName;
            private bool _showDocumentation;
            private bool _showAdvancedSettings;
            private string _expiration;
            private string _maxRetries;
            private string _stamps;
            private string _tag;
            public string _proxyFactoryTypeFullName;

			public event EventHandler<EventArgs> EndpointChanged;
			public event EventHandler<EventArgs> ProxyTypeChanged;
			public event EventHandler<EventArgs> MethodsChanged;
			public event EventHandler<EventArgs<bool>> RequestingValidation;
            public event EventHandler<EventArgs> ShowDocumentationChanged;
            public event EventHandler<EventArgs> ExpirationChanged;
            public event EventHandler<EventArgs> MaxRetriesChanged;
            public event EventHandler<EventArgs> StampsChanged;
            public event EventHandler<EventArgs> TagChanged;
            public event EventHandler<EventArgs> ProxyFactoryTypeFullNameChanged;

			public void FireRequestingValidation(bool value)
			{
				if (RequestingValidation != null)
				{
					RequestingValidation(this, new EventArgs<bool>(value));
				}
			}

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

			public string Endpoint
			{
				get { return EndpointDisplayed; }
			}

			public Type ProxyType
			{
				get { return ProxyTypeDisplayed; }
			}

			public List<MethodInfo> Methods
			{
				get { return MethodsShown; }
			}

			public void ShowEndpoint(string endpoint)
			{
				EndpointDisplayed = endpoint;
			}

			public void ShowProxyType(Type proxyType)
			{
				ProxyTypeDisplayed = proxyType;
			}

			public void ShowMethods(List<MethodInfo> methods, List<MethodInfo> selectedMethods)
			{
				IsMethodsShown = true;
				MethodsShown = methods;
				SelectedMethodsShown = selectedMethods;
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

			public void FireEndpointChanged(string endpoint)
			{
				EndpointDisplayed = endpoint;
				if (EndpointChanged != null)
					EndpointChanged(this, new EventArgs());
			}

			public void FireProxyTypeChanged(string proxyTypeName)
			{
				_proxyTypeName = proxyTypeName;
				if (ProxyTypeChanged != null)
					ProxyTypeChanged(this, new EventArgs());
			}

			public void FireMethodsChanged(List<MethodInfo> methods)
			{
				MethodsShown = methods;
				if (MethodsChanged != null)
					MethodsChanged(this, new EventArgs());
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

            public void FireShowDocumentationChanged(bool value)
            {
                _showDocumentation = value;
                if (ShowDocumentationChanged != null)
                    ShowDocumentationChanged(this, new EventArgs());
            }

			public void SetLanguage(string language)
			{
				_language = language;
			}

			public void RefreshSolutionPreview(string disconnectedAgentsFolder,
			                                   string proxyFolder,
			                                   string agentFileName,
			                                   string agentCallbackFileName,
			                                   string agentCallbackBaseFileName,
			                                   IProjectModel activeModuleProject)
			{
				_previewRefreshed = true;
				_disconnectedAgentsFolder = disconnectedAgentsFolder;
				_proxyFolder = proxyFolder;
				_agentFileName = agentFileName;
				_agentCallbackFileName = agentCallbackFileName;
				_agentCallbackBaseFileName = agentCallbackBaseFileName;
			}

			public string Language
			{
				get { return _language; }
				set { _language = value; }
			}

			public string DisconnectedAgentsFolder
			{
				get { return _disconnectedAgentsFolder; }
				set { _disconnectedAgentsFolder = value; }
			}

			public string ProxyFolder
			{
				get { return _proxyFolder; }
				set { _proxyFolder = value; }
			}

			public string AgentFileName
			{
				get { return _agentFileName; }
				set { _agentFileName = value; }
			}

			public string AgentCallbackFileName
			{
				get { return _agentCallbackFileName; }
				set { _agentCallbackFileName = value; }
			}

			public string AgentCallbackBaseFileName
			{
				get { return _agentCallbackBaseFileName; }
				set { _agentCallbackBaseFileName = value; }
			}

			public bool PreviewRefreshed
			{
				get { return _previewRefreshed; }
			}

			public string ProxyTypeName
			{
				get { return _proxyTypeName; }
			}

            public void ShowNotBuildPanel(bool built, bool existsProxyClass, string proxyTypeName)
			{
			}

            public void ShowShowDocumentation(bool showDocumentation)
            {
                _showDocumentation=showDocumentation;
            }

            public void ShowShowAdvancedSettings(bool showAdvancedSettings)
            {
                _showAdvancedSettings = showAdvancedSettings;
            }

            public bool ShowDocumentation
            {
                get { return _showDocumentation; }
            }

            public bool ShowAdvancedSettings
            {
                get { return _showAdvancedSettings; }
            }
        }

		private class MockEndpointPageModel : IEndpointPageModel
		{
			private bool _built = false;
            private bool _existsProxyClass = true;
			private string _endpoint;
			private string _language;
			private string _disconnectedAgentsFolder;
			private string _proxyFolder;
			private IProjectModel _currentProject = null;

			private Type _proxyType;
			private string _proxyTypeName;
            private string _currentProxyTypeName;
			private List<MethodInfo> _serviceAgentMethods;
			private List<MethodInfo> _originalTypeMethods;
			private List<string> _operations;
            private bool _showDocumentation;
            private bool _showAdvancedSettings;
            private string _stamps;
            private string _maxRetries;
            private string _tag;
            private string _proxyFactoryTypeFullName;
            private string _expiration;


			public List<string> Operations
			{
				get { return _operations; }
				set { _operations = value; }
			}

			public string Endpoint
			{
				get { return _endpoint; }
				set { _endpoint = value; }
			}

			public Type ProxyType
			{
				get { return _proxyType; }
				set { _proxyType = value; }
			}

			public List<MethodInfo> ServiceAgentMethods
			{
				get { return _serviceAgentMethods; }
				set { _serviceAgentMethods = value; }
			}

			public List<MethodInfo> OriginalTypeMethods
			{
				get { return _originalTypeMethods; }
				set { _originalTypeMethods = value; }
			}

			public bool Built
			{
				get { return _built; }
			}

			public string Language
			{
				get { return _language; }
				set { _language = value; }
			}

			public string DisconnectedAgentsFolder
			{
				get { return _disconnectedAgentsFolder; }
				set { _disconnectedAgentsFolder = value; }
			}

			public string ProxyFolder
			{
				get { return _proxyFolder; }
				set { _proxyFolder = value; }
			}

			public string AgentFileName
			{
				get { return string.Format("{0}Agent", ProxyFolder); }
			}

			public string AgentCallbackFileName
			{
				get { return string.Format("{0}AgentCallback", ProxyFolder); }
			}

			public string AgentCallbackBaseFileName
			{
				get { return string.Format("{0}AgentCallbackbase", ProxyFolder); }
			}

			public bool IsValid
			{
				get
				{
					return (!String.IsNullOrEmpty(_endpoint)
					        && _proxyType != null
					        && _serviceAgentMethods != null
					        && _serviceAgentMethods.Count > 0);
				}
			}

			public IProjectModel CurrentProject
			{
				get { return _currentProject; }
			}

			public string ProxyTypeName
			{
				get { return _proxyTypeName; }
				set { _proxyTypeName = value; }
			}

            public bool ShowDocumentation
            {
                get
                {
                    return _showDocumentation;
                }
                set
                {
                    _showDocumentation = value;
                }
            }

            public bool ShowAdvancedSettings
            {
                get
                {
                    return _showAdvancedSettings;
                }
                set
                {
                    _showAdvancedSettings = value;
                }
            }

            public string Stamps
            {
                get
                {
                    return _stamps;
                }
                set
                {
                    _stamps = value;
                }
            }

            public string MaxRetries
            {
                get
                {
                    return _maxRetries;
                }
                set
                {
                    _maxRetries = value;
                }
            }

            public string Expiration
            {
                get
                {
                    return _expiration;
                }
                set
                {
                    _expiration = value;
                }
            }

            public string Tag
            {
                get
                {
                    return _tag;
                }
                set
                {
                    _tag=value;
                }
            }

            public string ProxyFactoryTypeFullName
            {
                get { return _proxyFactoryTypeFullName; }
                set { _proxyFactoryTypeFullName = value; }
            }

            public bool ExistsProxyClass
            {
                get { return _existsProxyClass; }
            }

            public string CurrentProxyTypeName
            {
                get { return _currentProxyTypeName; }
                set { _currentProxyTypeName = value; }
            }
        }
	}
}