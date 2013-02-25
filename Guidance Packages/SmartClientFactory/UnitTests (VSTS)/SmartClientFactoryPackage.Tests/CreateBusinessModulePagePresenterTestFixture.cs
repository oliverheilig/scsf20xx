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
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for CreateBusinessModulePagePresenterTestFixture
	/// </summary>
	[TestClass]
	public class CreateBusinessModulePagePresenterTestFixture
	{
		public CreateBusinessModulePagePresenterTestFixture()
		{
		}

		private IDictionaryService dictionary;
		private MockCreateBusinessModulePage view;
		private CreateBusinessModulePagePresenter presenter;
		private CreateBusinessModulePageModel model;

		[TestInitialize]
		public void SetUp()
		{
			dictionary = new MockDictionaryService();
			view = new MockCreateBusinessModulePage();
			model = new CreateBusinessModulePageModel(dictionary);
			presenter = new CreateBusinessModulePagePresenter(view, model);
		}

		[TestMethod]
		public void ShowModulespaceNameOnViewReady()
		{
			dictionary.SetValue("ModuleNamespace", "Some.Namespace");

			presenter.OnViewReady();

			Assert.AreEqual(model.ModuleNamespace, view.ModuleNamespace);
			Assert.AreEqual("Some.Namespace", view.ModuleNamespace);
		}

		[TestMethod]
		public void SetModuleNameOnViewReady()
		{
			dictionary.SetValue("ModuleName", "Module1");
			presenter.OnViewReady();

			Assert.AreEqual(model.ModuleName, view.ModuleName);
			Assert.AreEqual("Module1", view.ModuleName);
		}

		[TestMethod]
		public void SetLanguageOnViewReady()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			presenter.OnViewReady();

			Assert.AreEqual(model.Language, view.Language);
			Assert.AreEqual("CS", view.Language);
		}

		[TestMethod]
		public void ShouldUpdateModelOnCreateModuleInterfaceLibraryChanged()
		{
			view.FireCreateModuleInterfaceLibraryChanged(true);
			Assert.AreEqual(view.CreateModuleInterfaceLibrary, model.CreateModuleInterfaceLibrary);
		}

		[TestMethod]
		public void ShouldUpdateModelOnShowDocumentationChanged()
		{
			view.FireShowDocumentationChanged(true);
			Assert.AreEqual(view.ShowDocumentation, model.ShowDocumentation);
		}

		[TestMethod]
		public void OnViewReadyShowsolutionPreview()
		{
			presenter.OnViewReady();

			Assert.IsTrue(view.PreviewRefreshed);
		}

		[TestMethod]
		public void ShouldUpdateModelOnCreateTestProjectChanged()
		{
			view.FireCreateTestProjectChanged(true);
			Assert.AreEqual(view.CreateTestProject, model.CreateTestProject);
		}

		[TestMethod]
		public void ModelIsValidAlways()
		{
			Assert.IsTrue(model.IsValid);
		}
	}

	internal class MockCreateBusinessModulePage : ICreateBusinessModulePage
	{
		public event EventHandler<EventArgs> CreateModuleInterfaceLibraryChanged;
		public event EventHandler<EventArgs> ShowDocumentationChanged;
		public event EventHandler<EventArgs> CreateTestProjectChanged;
		public event EventHandler<EventArgs<bool>> RequestingValidation;

		private string _moduleNamespace;
		private bool _createModuleInterfaceLibrary;
		private bool _showDocumentation;
		private bool _previewRefreshed;
		private bool _createTestProject;
		private string _moduleName;
		private string _language;

		public void ShowModuleNamespace(string moduleNamespace)
		{
			_moduleNamespace = moduleNamespace;
		}

		public void SetModuleName(string moduleName)
		{
			_moduleName = moduleName;
		}

		public void SetLanguage(string language)
		{
			_language = language;
		}

		public bool CreateModuleInterfaceLibrary
		{
			get { return _createModuleInterfaceLibrary; }
		}

		public bool ShowDocumentation
		{
			get { return _showDocumentation; }
		}

		public bool CreateTestProject
		{
			get { return _createTestProject; }
		}

		public string ModuleNamespace
		{
			get { return _moduleNamespace; }
		}

		public string ModuleName
		{
			get { return _moduleName; }
		}

		public string Language
		{
			get { return _language; }
		}

		public void FireCreateModuleInterfaceLibraryChanged(bool value)
		{
			_createModuleInterfaceLibrary = value;
			if (CreateModuleInterfaceLibraryChanged != null)
				CreateModuleInterfaceLibraryChanged(this, new EventArgs());
		}

		public void FireShowDocumentationChanged(bool value)
		{
			_showDocumentation = value;
			if (ShowDocumentationChanged != null)
				ShowDocumentationChanged(this, new EventArgs());
		}

		public void FireCreateTestProjectChanged(bool value)
		{
			_createTestProject = value;
			if (CreateTestProjectChanged != null)
				CreateTestProjectChanged(this, new EventArgs());
		}

		internal void FireRequestingValidation()
		{
			if (RequestingValidation != null)
				RequestingValidation(this, new EventArgs<bool>());
		}

		public bool PreviewRefreshed
		{
			get { return _previewRefreshed; }
		}

		public void RefreshSolutionPreview()
		{
			_previewRefreshed = true;
		}
	}
}