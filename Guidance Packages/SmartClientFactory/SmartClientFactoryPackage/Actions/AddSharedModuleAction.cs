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
using System.Xml;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public sealed class AddSharedModuleAction : Microsoft.Practices.RecipeFramework.Action
	{
		#region Input properties

		[Input(Required = true)]
		public XmlDocument XmlDoc
		{
			get { return xmlDoc; }
			set { xmlDoc = value; }
		} XmlDocument xmlDoc;

		[Input(Required = true)]
		public string ModuleName
		{
			get { return moduleName; }
			set { moduleName = value; }
		} string moduleName;

		[Input(Required = true)]
		public string SectionName
		{
			get { return sectionName; }
			set { sectionName = value; }
		} string sectionName;

		#endregion

		#region Overrides

		public override void Execute()
		{
			XmlNamespaceManager nsManager = new XmlNamespaceManager(this.XmlDoc.NameTable);
			nsManager.AddNamespace("cp1", "http://schemas.microsoft.com/pag/cab-profile");
			nsManager.AddNamespace("cp2", "http://schemas.microsoft.com/pag/cab-profile/2.0");
			XmlNode moduleList = this.XmlDoc.SelectSingleNode("//cp1:Modules", nsManager);
			XmlNode newModule = null;
			if (moduleList == null && !string.IsNullOrEmpty(SectionName))
			{
				moduleList = this.XmlDoc.SelectSingleNode("//cp2:Section[@Name='" + SectionName + "']/cp2:Modules", nsManager);
				newModule = XmlDoc.CreateElement("ModuleInfo", "http://schemas.microsoft.com/pag/cab-profile/2.0");
			}
			else
			{
				newModule = XmlDoc.CreateElement("ModuleInfo", "http://schemas.microsoft.com/pag/cab-profile");
			}
			XmlAttribute xmlAttrib = XmlDoc.CreateAttribute("AssemblyFile");
			xmlAttrib.Value = this.ModuleName + ".dll";
			newModule.Attributes.Append(xmlAttrib);
			moduleList.AppendChild(newModule);
		}

		public override void Undo()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
