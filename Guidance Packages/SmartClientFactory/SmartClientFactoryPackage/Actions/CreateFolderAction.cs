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
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Library;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public class CreateFolderAction : ConfigurableAction
	{
		private bool _createFolder;
		private string _folderName;
		private ProjectItems _targetCollection;
		private ProjectItems _folderCollection;
		private ProjectItem _createdItem;

		public CreateFolderAction()
		{
			_createFolder = false;
		}

		[Input()]
		public bool CreateFolder
		{
			get { return _createFolder; }
			set { _createFolder = value; }
		}

		[Input(Required=true)]
		public string FolderName
		{
			get { return _folderName; }
			set { _folderName = value; }
		}

		[Input(Required = true)]
		public ProjectItems TargetCollection
		{
			get { return _targetCollection; }
			set { _targetCollection = value; }
		}

		[Output()]
		public ProjectItems FolderCollection
		{
			get { return _folderCollection; }
			set { _folderCollection = value; }
		}

		public override void Execute()
		{
			if (_createFolder)
			{
                _createdItem = DteHelper.FindItemByName(_targetCollection, _folderName, false);
                if (_createdItem == null)
                {
                    _createdItem = _targetCollection.AddFolder(_folderName, "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
                }
				_folderCollection = _createdItem.ProjectItems;

			}
			else
			{
				_folderCollection = _targetCollection;
			}
		}

		public override void Undo()
		{
			if (_createdItem != null)
				_createdItem.Delete();
		}
	}
}
