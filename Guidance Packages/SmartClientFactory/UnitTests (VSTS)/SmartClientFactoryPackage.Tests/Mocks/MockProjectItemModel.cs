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
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;

namespace SmartClientFactoryPackage.Tests.Mocks
{
	/// <summary>
	/// Mock class for testing things that need the IProjectItemModel interface
	/// </summary>
	internal class MockProjectItemModel : IProjectItemModel
	{
		private string _itemPath;
		private string _name;
		private object _projectItem;

		public MockProjectItemModel(object projectItem, string name, string itemPath)
		{
			_projectItem = projectItem;
			_name = name;
			_itemPath = itemPath;
		}

		public object ProjectItem
		{
			get { return _projectItem; }
		}

		public string Name
		{
			get { return _name; }
		}

		public string ItemPath
		{
			get { return _itemPath; }
		}
	}
}