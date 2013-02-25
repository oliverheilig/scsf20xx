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
using System.Diagnostics;
using EnvDTE;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
	/// <summary>
	/// ValueProvider that returns the first selected project
	/// in the solution explorer
	/// </summary>
	[ServiceDependency(typeof(DTE))]
	public class FirstSelectedProject : ValueProvider
	{
		/// <summary>
		/// Sets the newValue to the first selected project
		/// </summary>
		/// <param name="currentValue"></param>
		/// <param name="newValue"></param>
		/// <returns></returns>
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			newValue = currentValue;
			DTE vs = (DTE)GetService(typeof(DTE));
			object[] activeProjects = (object[])vs.ActiveSolutionProjects;
			if (activeProjects != null && activeProjects.Length > 0)
			{
				newValue = activeProjects[0] as Project;
			}
			if (newValue != null)
			{
				return true;
			}
			return false;
		}
	}
}