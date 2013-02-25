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
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartClientFactoryPackage.Tests.Mocks;

namespace SmartClientFactoryPackage.Tests
{
	[TestClass]
	public class SolutionPropertiesPagePresenterTestFixture
	{
		private IDictionaryService dictionary;
		private MockSolutionPropertiesPage view;
		private SolutionPropertiesPagePresenter presenter;
		private SolutionPropertiesModel model;

		[TestInitialize]
		public void SetUp()
		{
			dictionary = new MockDictionaryService();
			view = new MockSolutionPropertiesPage();
			model = new SolutionPropertiesModel(dictionary);
			presenter = new SolutionPropertiesPagePresenter(view, model);
		}

		[TestMethod]
		public void ShowDocumentationChanges()
		{
			view.FireShowDocumentationChanging(true);
			Assert.AreEqual(model.ShowDocumentation, view.ShowDocumentation);
		}

		[TestMethod]
		public void CreateShellLayoutModuleChanges()
		{
			view.FireCreateShellLayoutModuleChanging(true);
			Assert.AreEqual(model.CreateShellLayoutModule, view.CreateShellLayoutModule);
		}

		[TestMethod]
		public void ShowShowDocumentationOnViewReady()
		{
			dictionary.SetValue("RootNamespace", "Some.Namespace");

			presenter.OnViewReady();

			Assert.AreEqual("Some.Namespace", view.RootNamespace);
		}

		[TestMethod]
		public void ShowRootNamespaceOnViewReady()
		{
			dictionary.SetValue("ShowDocumentation", false);

			presenter.OnViewReady();

			Assert.AreEqual(false, view.ShowDocumentation);
		}

		[TestMethod]
		public void ShowSupportLibrariesPathOnViewReady()
		{
			dictionary.SetValue("SupportLibrariesPath", @"c:\some path");

			presenter.OnViewReady();

			Assert.AreEqual(@"c:\some path", view.SupportLibrariesPath);
		}

		[TestMethod]
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		public void ViewFireSupportLibrariesUpdatesModel()
		{
			view.FireSupportingLibrariesPathChanging((new DirectoryInfo(".\\Support")).FullName);

			Assert.AreEqual(view.SupportLibrariesPath, model.SupportLibrariesPath);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullRootNamespace()
		{
			model.RootNamespace = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptyRootNamespace()
		{
			model.RootNamespace = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnInvalidRootNamespace()
		{
			model.RootNamespace = "Root.Name Space";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullSupportLibrariesPath()
		{
			model.SupportLibrariesPath = null;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnEmptySupportLibrariesPath()
		{
			model.SupportLibrariesPath = string.Empty;

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnNotExistingSupportLibrariesPath()
		{
			model.SupportLibrariesPath = "c:\\some path that should not exists";

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ModelIsNotValidOnNullCABDlls()
		{
			dictionary.SetValue("CompositeUIDlls", null);

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		[DeploymentItem(@"Support\EntLib.dll.txt", "Support")]
		[DeploymentItem(@"Support\CAB\Dummy.txt", @"Support\CAB")]
		[DeploymentItem(@"Support\EntLib\Dummy.txt", @"Support\EntLib")]
		[DeploymentItem(@"Support\Offline\Dummy.txt", @"Support\Offline")]
		public void ModelIsValidWithValidArguments()
		{
			dictionary.SetValue("RecipeLanguage", "C#");
			dictionary.SetValue("CompositeUIDlls", "CompositeUI.dll.txt");
			dictionary.SetValue("EnterpriseLibraryDlls", "EntLib.dll.txt");

			model.SupportLibrariesPath = (new DirectoryInfo(".\\Support")).FullName;
			model.RootNamespace = @"SomeNamespace";

			Assert.IsTrue(model.IsValid);
		}

		[TestMethod]
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		[DeploymentItem(@"Support\EntLib.dll.txt", "Support")]
		[DeploymentItem(@"Support\CAB\Dummy.txt", @"Support\CAB")]
		[DeploymentItem(@"Support\EntLib\Dummy.txt", @"Support\EntLib")]
		[DeploymentItem(@"Support\Offline\Dummy.txt", @"Support\Offline")]
		public void ModelIsValidWithValidArgumentsForVB()
		{
			dictionary.SetValue("RecipeLanguage", "VB");
			dictionary.SetValue("CompositeUIDlls", "CompositeUI.dll.txt");
			dictionary.SetValue("EnterpriseLibraryDlls", "EntLib.dll.txt");

			model.SupportLibrariesPath = (new DirectoryInfo(".\\Support")).FullName;
			model.RootNamespace = @"SomeNamespace";

			Assert.IsTrue(model.IsValid);
		}

		[TestMethod]
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		public void ModelIsNotValidWhenSupportingLibrariesFolderIncorrect()
		{
			view.FireSupportingLibrariesPathChanging(@"not exists");

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		public void ShouldNotValidateWhenFolderNotContainsCompositeDlls()
		{
			// simulate a missing dll by looking for an unexisting one
			dictionary.SetValue("CompositeUIDlls", "NonExists.dll");
			view.FireSupportingLibrariesPathChanging((new DirectoryInfo(".\\Support")).FullName);

			Assert.IsFalse(model.IsValid);
		}

		[TestMethod]
		public void ShowSupportingLibrariesOnViewReady()
		{
			dictionary.SetValue("CompositeUIDlls",
			                    "Microsoft.Practices.CompositeUI.dll;Microsoft.Practices.CompositeUI.WinForms.dll;Microsoft.Practices.ObjectBuilder.dll");
			dictionary.SetValue("EnterpriseLibraryDlls",
			                    "Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.dll;Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.dll;Microsoft.Practices.EnterpriseLibrary.Logging.dll;Microsoft.Practices.EnterpriseLibrary.Common.dll");

			presenter.OnViewReady();

			Assert.IsTrue(view.SupportLibraries.Length > 0);
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
		[DeploymentItem(@"Support\CompositeUI.dll.txt", "Support")]
		[DeploymentItem(@"Support\EntLib.dll.txt", "Support")]
		public void WhenSolutionSupportingLibrariesPathChangesUpdateMissingLibraries()
		{
			dictionary.SetValue("CompositeUIDlls", "CompositeUI.dll.txt;MissingCompositeLibrary.dll");
			dictionary.SetValue("EnterpriseLibraryDlls", "EntLib.dll.txt;MissingEntlibLibrary.dll");
			view.FireSupportingLibrariesPathChanging((new DirectoryInfo(".\\Support")).FullName);

			Assert.AreEqual(4, view.SupportLibraries.Length);
			Assert.AreEqual(4, view.MissingLibraries.Length);
			Assert.AreEqual("CompositeUI.dll.txt", view.SupportLibraries[0]);
			Assert.IsFalse(view.MissingLibraries[0]);
			Assert.AreEqual("MissingCompositeLibrary.dll", view.SupportLibraries[1]);
			Assert.IsTrue(view.MissingLibraries[1]);
			Assert.AreEqual("EntLib.dll.txt", view.SupportLibraries[2]);
			Assert.IsFalse(view.MissingLibraries[2]);
			Assert.AreEqual("MissingEntlibLibrary.dll", view.SupportLibraries[3]);
			Assert.IsTrue(view.MissingLibraries[3]);
		}

		[TestMethod]
		public void OnViewReadyShowsolutionPreview()
		{
			presenter.OnViewReady();

			Assert.IsTrue(view.PreviewRefreshed);
		}

		[TestMethod]
		public void ShouldDefaultToNotSupportingWPFViews()
		{
			presenter.OnViewReady();
			view.FireSupportWPFViewsChanging(false);
			Assert.IsFalse((bool) dictionary.GetValue("SupportWPFViews"));
			Assert.AreEqual("WinForms", (string) dictionary.GetValue("WorkspaceTechnology"));
		}

		[TestMethod]
		public void SettingSupportWPFViewSetsTechnologyToWPF()
		{
			presenter.OnViewReady();
			view.FireSupportWPFViewsChanging(true);
			Assert.IsTrue((bool) dictionary.GetValue("SupportWPFViews"));
			Assert.AreEqual("WPF", (string) dictionary.GetValue("WorkspaceTechnology"));
		}

		[TestMethod]
		public void CreateShellLayoutModuleAndUseSimpleShellStayOpposite()
		{
			presenter.OnViewReady();
			view.FireCreateShellLayoutModuleChanging(false);
			Assert.IsFalse((bool) dictionary.GetValue("CreateShellLayoutModule"));
			Assert.IsTrue((bool) dictionary.GetValue("UseSimpleShell"));
		}

		[TestMethod]
		public void CreateShellLayoutTrueMeansUseSimpleShellIsFalse()
		{
			presenter.OnViewReady();
			view.FireCreateShellLayoutModuleChanging(true);
			Assert.IsTrue((bool) dictionary.GetValue("CreateShellLayoutModule"));
			Assert.IsFalse((bool) dictionary.GetValue("UseSimpleShell"));
		}
	}

	internal class MockSolutionPropertiesPage : ISolutionPropertiesPage
	{
		public event EventHandler<EventArgs> SupportLibrariesPathChanging;
		public event EventHandler<EventArgs> ShowDocumentationChanging;
		public event EventHandler<EventArgs> CreateShellLayoutModuleChanging;
		public event EventHandler<EventArgs> RootNamespaceChanging;
		public event EventHandler<EventArgs<bool>> RequestingValidation;

		public event EventHandler<EventArgs> SupportWPFViewsChanging;


		public string[] SupportLibraries;
		public bool[] MissingLibraries;
		private bool _showDocumentation;
		private bool _createShellLayoutModule;
		private bool _supportWPFViews;
		private string _rootNamespace;
		private string _supportingLibrariesPath;
		private string _language;
		private bool _previewRefreshed;

		public bool ShowDocumentation
		{
			get { return _showDocumentation; }
		}

		public bool CreateShellLayoutModule
		{
			get { return _createShellLayoutModule; }
		}

		public bool SupportWPFViews
		{
			get { return _supportWPFViews; }
		}

		public string RootNamespace
		{
			get { return _rootNamespace; }
			set { _rootNamespace = value; }
		}

		public void ShowRootNamespace(string rootNamespace)
		{
			_rootNamespace = rootNamespace;
		}

		public string SupportLibrariesPath
		{
			get { return _supportingLibrariesPath; }
			set { _supportingLibrariesPath = value; }
		}

		public string Language
		{
			get { return _language; }
		}

		public void SetLanguage(string language)
		{
			_language = language;
		}

		public void ShowSupportLibrariesPath(string path)
		{
			_supportingLibrariesPath = path;
		}

		public void ShowShowDocumentation(bool showDocumentation)
		{
			_showDocumentation = showDocumentation;
		}

		public void FireShowDocumentationChanging(bool value)
		{
			_showDocumentation = value;
			if (ShowDocumentationChanging != null)
				ShowDocumentationChanging(this, new EventArgs());
		}


		public void FireRootNamespaceChanging(string value)
		{
			_rootNamespace = value;
			if (RootNamespaceChanging != null)
				RootNamespaceChanging(this, new EventArgs());
		}

		public void FireCreateShellLayoutModuleChanging(bool value)
		{
			_createShellLayoutModule = value;
			if (CreateShellLayoutModuleChanging != null)
				CreateShellLayoutModuleChanging(this, new EventArgs());
		}

		public void FireSupportWPFViewsChanging(bool value)
		{
			_supportWPFViews = value;
			if (SupportWPFViewsChanging != null)
			{
				SupportWPFViewsChanging(this, EventArgs.Empty);
			}
		}

		public void FireSupportingLibrariesPathChanging(string value)
		{
			_supportingLibrariesPath = value;
			if (SupportLibrariesPathChanging != null)
				SupportLibrariesPathChanging(this, new EventArgs());
		}

		internal void FireRequestingValidation()
		{
			if (RequestingValidation != null)
				RequestingValidation(this, new EventArgs<bool>());
		}

		public void ShowSupportLibraries(string[] libraries, bool[] missing)
		{
			SupportLibraries = libraries;
			MissingLibraries = missing;
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