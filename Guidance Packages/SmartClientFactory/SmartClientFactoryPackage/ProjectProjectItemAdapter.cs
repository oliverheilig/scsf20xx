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
using EnvDTE;

//TODO SW: Replace this recipe by CreateFolder + AddItemFromStringAction
namespace Microsoft.Practices.SmartClientFactory
{
	internal class ProjectProjectItemAdapter : ProjectItem
	{
		Project project;

		public ProjectProjectItemAdapter(Project project)
		{
			this.project = project;
		}

		#region ProjectItem Members

		public ProjectItems Collection
		{
			get { throw new NotImplementedException(); }
		}

		public ConfigurationManager ConfigurationManager
		{
			get { return project.ConfigurationManager; }
		}

		public Project ContainingProject
		{
			get { return project; }
		}

		public DTE DTE
		{
			get { return project.DTE; }
		}

		public void Delete()
		{
			project.Delete();
		}

		public Document Document
		{
			get { throw new NotImplementedException(); }
		}

		public void ExpandView()
		{
			throw new NotImplementedException();
		}

		public string ExtenderCATID
		{
			get { return project.ExtenderCATID; }
		}

		public object ExtenderNames
		{
			get { return project.ExtenderNames; }
		}

		public FileCodeModel FileCodeModel
		{
			get { throw new NotImplementedException(); }
		}

		public short FileCount
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsDirty
		{
			get
			{
				return project.IsDirty;
			}
			set
			{
				project.IsDirty = value;
			}
		}

		public string Kind
		{
			get { return project.Kind; }
		}

		public string Name
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public object Object
		{
			get { return project.Object; }
		}

		public Window Open(string ViewKind)
		{
			throw new NotImplementedException();
		}

		public ProjectItems ProjectItems
		{
			get { return project.ProjectItems; }
		}

		public EnvDTE.Properties Properties
		{
			get { return project.Properties; }
		}

		public void Remove()
		{
			throw new NotImplementedException();
		}

		public void Save(string FileName)
		{
			project.Save(FileName);
		}

		public bool SaveAs(string NewFileName)
		{
			project.SaveAs(NewFileName);
			return true;
		}

		public bool Saved
		{
			get
			{
				return project.Saved;
			}
			set
			{
				project.Saved = value;
			}
		}

		public Project SubProject
		{
			get { throw new NotImplementedException(); }
		}

		public object get_Extender(string ExtenderName)
		{
			return project.get_Extender(ExtenderName);
		}

		public string get_FileNames(short index)
		{
			if (index == 0)
				return project.FileName;
			else
				throw new IndexOutOfRangeException();
		}

		public bool get_IsOpen(string ViewKind)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
