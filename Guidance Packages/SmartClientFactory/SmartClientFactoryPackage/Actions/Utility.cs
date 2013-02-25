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
using System.ComponentModel;
using Microsoft.Practices.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	class Utility
	{
		private const uint ROOT = 0xFFFFFFFE;
		
		internal static Guid GetProjectGuid(IServiceProvider serviceProvider, Project project)
		{
			if (project != null)
			{
				IVsHierarchy vsHier = Microsoft.Practices.RecipeFramework.Library.DteHelper.GetVsHierarchy(serviceProvider, project);
				Guid projectGuid = Guid.Empty;
				vsHier.GetGuidProperty(ROOT, (int)__VSHPROPID.VSHPROPID_ProjectIDGuid, out projectGuid);
				return projectGuid;
			}
			return Guid.Empty;
		}


		internal static Project GetProjectFromGuid(DTE dte, IServiceProvider provider, Guid guid)
		{
			return DteHelperEx.FindProject(dte, delegate(Project match)
			{
				return guid == GetProjectGuid(provider, match);
			});
		}

		internal static bool IsSealedOrStatic(CodeClass codeClass)
		{
			EditPoint start = codeClass.StartPoint.CreateEditPoint();
			EditPoint cursor = codeClass.StartPoint.CreateEditPoint();
			cursor.WordRight(1);
			string currentWord = String.Empty;
			while (!cursor.AtEndOfDocument)
			{
				currentWord = start.GetText(cursor);
				if (currentWord.StartsWith("class", true, CultureInfo.InvariantCulture)) return false;
				if (currentWord.StartsWith("sealed", true, CultureInfo.InvariantCulture)) return true;
				if (currentWord.StartsWith("static", true, CultureInfo.InvariantCulture)) return true;
                if (currentWord.StartsWith("notinheritable", true, CultureInfo.InvariantCulture)) return true;
				start.WordRight(1);
				cursor.WordRight(1);
			}
			return false;
		}
	}
}
