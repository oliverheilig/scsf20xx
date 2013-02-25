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
using System.IO;
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for CreateViewPagePresenterTestFixture
	/// </summary>
	[TestClass]
	public class CreateViewPagePresenterTestFixture
	{
		public CreateViewPagePresenterTestFixture()
		{
		}

		private CreateViewPagePresenter presenter;
		private MockCreateViewPage view;
		private IDictionaryService dictionary;
		private MockCreateViewPageModel mockModel;
		private CreateViewPageModel model;

		[TestInitialize]
		public void Setup()
		{
			dictionary = new MockDictionaryService();
			mockModel = new MockCreateViewPageModel();
			mockModel.ProjectItem = new MockProjectItemModel(
				new object(),
				"Views",
				new DirectoryInfo(@".\Support\MockViewsFolder").FullName);
			view = new MockCreateViewPage();
			presenter = new CreateViewPagePresenter(view, mockModel);
			model = new CreateViewPageModel(dictionary, null);
			model.ProjectItem = mockModel.ProjectItem;
		}

		[TestMethod]
		public void ShowViewNameOnViewReady()
		{
			mockModel.ViewName = "View1";

			presenter.OnViewReady();

			Assert.AreEqual(view.ViewNameDisplayed, "View1");
		}

		[TestMethod]
		public void OnViewReadyShowsolutionPreview()
		{
			presenter.OnViewReady();

			Assert.IsTrue(view.PreviewRefreshed);
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
		public void FireViewNameChangedUpdatesModel()
		{
			view.FireViewNameChanged("View1");

			Assert.AreEqual(view.ViewName, mockModel.ViewName);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullViewName()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			model.ViewName = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptyViewName()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			model.ViewName = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnViewNameBeginningWithNumbers()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			model.ViewName = "1MyView";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnViewNameWithBlanks()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			model.ViewName = "My View";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnViewNameReservedSystemWord()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
			model.ViewName = "CON";

			Assert.IsFalse(model.IsValid);
		}

        [TestMethod]
        [DeploymentItem(@"Support\MockViewsFolder",@"Support\MockViewsFolder")]
        public void ModelIsNotValidOnXamlFileExistsForNonWpfView()
        {
            dictionary.SetValue("RecipeLanguage", "CS");
            dictionary.SetValue("ValidateExistingFileExtension", "xaml");
            dictionary.SetValue("IsWpfView",false);
            model.ViewName = "MockView2";

            Assert.IsFalse(model.IsValid);
        }

        [TestMethod]
        [DeploymentItem(@"Support\MockViewsFolder", @"Support\MockViewsFolder")]
        public void ModelIsNotValidOnCsFileExistsForWpfView()
        {
            dictionary.SetValue("RecipeLanguage", "CS");
            dictionary.SetValue("ValidateExistingFileExtension", "cs");
            dictionary.SetValue("IsWpfView", true);
            model.ViewName = "MockView1";

            Assert.IsFalse(model.IsValid);
        }

        [TestMethod]
        //[Ignore]//Views are no more validated against existing filenames.
        [DeploymentItem(@"Support\MockViewsFolder", @"Support\MockViewsFolder")]
        public void ErrorMessageIsShownOnExistingViewName()
        {
            dictionary.SetValue("RecipeLanguage", "CS");
            dictionary.SetValue("ValidateExistingFileExtension", "xaml");
            dictionary.SetValue("IsWpfView", false);
            model.ViewName = "MockView2";
            view.FireRequestingValidation(true);

            Assert.IsFalse(model.IsValid);
            Assert.IsTrue(view.ErrorMessageShowed);
        }

		[TestMethod]
		public void FireShowDocumentation()
		{
			view.FireShowDocumentChanged(true);
			Assert.AreEqual(view.ShowDocumentation, mockModel.ShowDocumentation);
		}

		[TestMethod]
		public void FireCreateViewFolder()
		{
			view.FireCreateViewFolderChanged(true);
			Assert.AreEqual(view.CreateViewFolder, mockModel.CreateViewFolder);
		}

		[TestMethod]
		public void SetTestProjectExistsOnViewReady()
		{
			mockModel.TestProjectExists = false;
			presenter.OnViewReady();

			Assert.AreEqual(mockModel.TestProjectExists, view.TestProjectExists);
			Assert.AreEqual(false, view.TestProjectExists);
		}

		[TestMethod]
		public void ModelIsValidWithValidArguments()
		{
			dictionary.SetValue("RecipeLanguage", "CS");
            dictionary.SetValue("ValidateExistingFileExtension", "xaml");
            dictionary.SetValue("IsWpfView", false);

			model.ViewName = "MockView1";

			Assert.IsTrue(model.IsValid);
		}

	}

	internal class MockCreateViewPage : ICreateViewPage
	{
		public string ViewNameDisplayed;
		public bool _CreateViewFolder;
		private bool _ShowDocumentation;
		private string _language;
		private bool _previewRefreshed;
		private bool _testProjectExists;
		public bool ErrorMessageShowed;

		public event EventHandler<EventArgs> ViewNameChanged;
		public event EventHandler<EventArgs<bool>> RequestingValidation;
		public event EventHandler<EventArgs> ShowDocumentationChanged;
		public event EventHandler<EventArgs> CreateViewFolderChanged;

		public void FireRequestingValidation(bool value)
		{
			if (RequestingValidation != null)
			{
				RequestingValidation(this, new EventArgs<bool>(value));
			}
		}

		public string ViewName
		{
			get { return ViewNameDisplayed; }
		}

		public void ShowViewName(string viewName)
		{
			ViewNameDisplayed = viewName;
		}

		public string Language
		{
			get { return _language; }
			set { _language = value; }
		}

		public bool TestProjectExists
		{
			get { return _testProjectExists; }
			set { _testProjectExists = value; }
		}

		public void SetLanguage(string language)
		{
			_language = language;
		}

		public void FireViewNameChanged(string viewName)
		{
			ViewNameDisplayed = viewName;
			if (ViewNameChanged != null)
				ViewNameChanged(this, new EventArgs());
		}

		public bool ShowDocumentation
		{
			get { return _ShowDocumentation; }
			set { _ShowDocumentation = value; }
		}

		public void FireShowDocumentChanged(bool value)
		{
			_ShowDocumentation = value;
			if (ShowDocumentationChanged != null)
				ShowDocumentationChanged(this, new EventArgs());
		}

		public bool CreateViewFolder
		{
			get { return _CreateViewFolder; }
			set { _CreateViewFolder = value; }
		}

		public void FireCreateViewFolderChanged(bool value)
		{
			_CreateViewFolder = value;
			if (CreateViewFolderChanged != null)
				CreateViewFolderChanged(this, new EventArgs());
		}

		public bool PreviewRefreshed
		{
			get { return _previewRefreshed; }
		}

        public void RefreshSolutionPreview(IProjectModel activeModuleProject, IProjectItemModel activeProjectItem, bool isWpfView)
        {
            _previewRefreshed = true;
        }

		public void ShowValidationErrorMessage(string errorMessage)
		{
			ErrorMessageShowed = true;
		}
	}

    class MockCreateViewPageModel : ICreateViewPageModel
    {
        private bool _showDocumentation;
        private bool _createViewFolder;
        private string _viewName;
        private string _validateExistingFileExtension;
        private IProjectModel _moduleProject = null;
        private IProjectItemModel _projectItem;
        private string _language;
        private bool _testProjectExists;
        private bool _isWpfView = true;

		public bool ShowDocumentation
		{
			get { return _showDocumentation; }
			set { _showDocumentation = value; }
		}

		public string Language
		{
			get { return _language; }
			set { _language = value; }
		}

		public bool TestProjectExists
		{
			get { return _testProjectExists; }
			set { _testProjectExists = value; }
		}

		public bool CreateViewFolder
		{
			get { return _createViewFolder; }
			set { _createViewFolder = value; }
		}

		public string ViewName
		{
			get { return _viewName; }
			set { _viewName = value; }
		}

		public bool IsValid
		{
			get { return !String.IsNullOrEmpty(_viewName); }
		}

		public IProjectModel ModuleProject
		{
			get { return _moduleProject; }
		}

		public IProjectItemModel ProjectItem
		{
			get { return _projectItem; }
			set { _projectItem = value; }
		}

        public string ValidationErrorMessage
        {
            get 
            {
                return string.Empty;
            }
        }

        public bool IsWpfView
        {
            get
            {
                return _isWpfView;
            }
        }

        public string ValidateExistingFileExtension
        {
            get { return _validateExistingFileExtension; }
            set { _validateExistingFileExtension = value; }
        }
    }
}
