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
using System.IO;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Library;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public class AddItemFromStringToProjectItemsAction  : ConfigurableAction
	{
		private ProjectItems _targetCollection;
		private ProjectItems _createdProjectItems;
		private ProjectItem _createdItem;
		private string _content;
		private bool _open;
		private string _targetFileName;

		public AddItemFromStringToProjectItemsAction()
		{
			_open = true;
		}

		public override void Execute()
		{
			DTE vs=base.GetService<DTE>(true);
			string tempFilename = Path.GetTempFileName();
			using (StreamWriter sw = new StreamWriter(tempFilename, false))
			{
				sw.WriteLine(_content);
			}

            ProjectItem existingItem = DteHelper.FindItemByName(_targetCollection, _targetFileName, false);
            if (existingItem != null)
            {
                if (overwrite)
                {
                    OverwriteFile(vs, existingItem.get_FileNames(1), _content);
                    existingItem.Delete();
                }
            }

            _createdItem = DteHelper.FindItemByName(_targetCollection, _targetFileName, false);
            if (_createdItem == null)
            {
                _createdItem = _targetCollection.AddFromTemplate(tempFilename, _targetFileName);
            }

			_createdProjectItems = _createdItem.ProjectItems;
			if (_open)
			{
				Window window = _createdItem.Open("{00000000-0000-0000-0000-000000000000}");
				window.Visible = true;
				window.Activate();
			}
			File.Delete(tempFilename);
		}

        private void OverwriteFile(DTE vs, string fullPath, string content)
        {
            VsHelper.EnsureWriteable(vs, fullPath);
        }

		public override void Undo()
		{
			if (_createdItem != null)
			{
				_createdItem.Delete();
			}
		}

		[Input(Required = true)]
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		[Input]
		public bool Open
		{
			get { return _open; }
			set { _open = value; }
		}

		[Input(Required = true)]
		public ProjectItems TargetCollection
		{
			get { return _targetCollection; }
			set { _targetCollection = value; }
		}
	
		[Input(Required = true)]
		public string TargetFileName
		{
			get { return _targetFileName; }
			set { _targetFileName = value; }
		}

		[Output]
		public ProjectItems CreatedProjectItems
		{
			get { return _createdProjectItems; }
			set { _createdProjectItems = value; }
		}

        private bool overwrite=false;

        [Input(Required = false)]
        public bool Overwrite
        {
            get { return overwrite; }
            set { overwrite = value; }
        }
	}
}
