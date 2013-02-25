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
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for CreateBusinessModulePagePresenterTestFixture
	/// </summary>
	[TestClass]
	public class CreateEventPublicationPagePresenterTestFixture
	{
		public CreateEventPublicationPagePresenterTestFixture()
		{
		}

		private IDictionaryService dictionary;
		private MockCreateEventPublicationPage view;
		private CreateEventPublicationPagePresenter presenter;
		private CreateEventPublicationPageModel model;

		[TestInitialize]
		public void SetUp()
		{
			dictionary = new MockDictionaryService();
			view = new MockCreateEventPublicationPage();
			model = new CreateEventPublicationPageModel(dictionary, null);
			presenter = new CreateEventPublicationPagePresenter(view, model, null);

			dictionary.SetValue("EventArgs", "Some.EventArgs");
			dictionary.SetValue("PublicationScope", PublicationScope.Global);
		}

		[TestMethod]
		public void ShouldUpdateModelOnEventTopicChange()
		{
			view.FireEventTopicChanging("EventA");
			Assert.AreEqual("EventA", model.EventTopic);
		}

		[TestMethod]
		public void ShouldNotValidateOnEmptyEventTopic()
		{
			view.FireEventTopicChanging(String.Empty);

			// Assert.IsTrue(context.ContainsFailureFor(view));
			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ShouldUpdateModelOnPublicationScopeChange()
		{
			view.FirePublicationScopeChanging(PublicationScope.Descendants);

			Assert.AreEqual(PublicationScope.Descendants, model.PublicationScope);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullEventTopic()
		{
			model.EventTopic = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptyEventTopic()
		{
			model.EventTopic = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullEventArgs()
		{
			model.EventArgs = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptyEventArgs()
		{
			model.EventArgs = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		//[TestMethod]
		//public void ModelIsNotValidOnNotExistingClass()
		//{
		//    model.EventTopic = "SomeEventTopic";
		//    model.EventArgs = "Some.EventArgs";

		//    Assert.IsFalse(model.IsValid);
		//}

		[TestMethod]
		public void ShouldUpdateModelOnEventArgsChange()
		{
			view.FireEventArgsChanging("System.EventArgs");

			Assert.AreEqual("System.EventArgs", model.EventArgs);
		}

		[TestMethod]
		public void ShouldShowEventArgsOnViewReady()
		{
			dictionary.SetValue("EventArgs", "Some.EventArgs");

			presenter.OnViewReady();

			Assert.AreEqual(model.EventArgs, view.EventArgs);
			Assert.AreEqual("Some.EventArgs", view.EventArgs);
		}

		[TestMethod]
		public void ShouldShowPublicationScopeOnViewReady()
		{
			dictionary.SetValue("PublicationScope", PublicationScope.WorkItem);

			presenter.OnViewReady();

			Assert.IsTrue(view.PublicationScopes.Count == 3);
			Assert.AreEqual(model.PublicationScope, view.PublicationScope);
			Assert.AreEqual(PublicationScope.WorkItem, view.PublicationScope);
		}
	}

	internal class MockCreateEventPublicationPage : ICreateEventPublicationPage
	{
		public event EventHandler<EventArgs<bool>> RequestingValidation;
		public event EventHandler<EventArgs> EventTopicChanged;
		public event EventHandler<EventArgs> PublicationScopeChanged;
		public event EventHandler<EventArgs> EventArgsChanged;
        public event EventHandler<EventArgs> ShowDocumentationChanged;

		private string _eventTopic;
		private PublicationScope _publicationScope;
		private string _eventArgs;
		private IList<PublicationScope> _publicationScopes;
        private bool _showDocumentation;

		public string EventTopic
		{
			get { return _eventTopic; }
		}

		public PublicationScope PublicationScope
		{
			get { return _publicationScope; }
		}

		public string EventArgs
		{
			get { return _eventArgs; }
		}

		public IList<PublicationScope> PublicationScopes
		{
			get { return _publicationScopes; }
		}

		public void FireEventTopicChanging(string value)
		{
			_eventTopic = value;
			if (EventTopicChanged != null)
				EventTopicChanged(this, new EventArgs());
		}

		public void FirePublicationScopeChanging(PublicationScope publicationScope)
		{
			_publicationScope = publicationScope;

			if (PublicationScopeChanged != null)
				PublicationScopeChanged(this, new EventArgs());
		}

		public void FireEventArgsChanging(string value)
		{
			_eventArgs = value;

			if (EventArgsChanged != null)
				EventArgsChanged(this, new EventArgs());
		}

		internal void FireRequestingValidation()
		{
			if (RequestingValidation != null)
				RequestingValidation(this, new EventArgs<bool>());
		}

        public void FireShowDocumentationChanged(bool value)
        {
            _showDocumentation = value;
            if (ShowDocumentationChanged != null)
                ShowDocumentationChanged(this, new EventArgs());
        }
        
        public void ShowEventArgs(string eventArgs)
		{
			_eventArgs = eventArgs;
		}

		public void ShowPublicationScope(List<PublicationScope> publicationScopes, PublicationScope selected)
		{
			_publicationScopes = publicationScopes;
			_publicationScope = selected;
		}

        public bool ShowDocumentation
        {
            get { return _showDocumentation; }
        }

        public void ShowShowDocumentation(bool showDocumentation)
        {
            _showDocumentation=showDocumentation;
        }
    }
}