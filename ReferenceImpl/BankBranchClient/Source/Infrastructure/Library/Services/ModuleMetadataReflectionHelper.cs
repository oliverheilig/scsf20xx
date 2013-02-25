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
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.CompositeUI;
using System.IO;

namespace GlobalBank.Infrastructure.Library.Services
{
	public static class ModuleMetadataReflectionHelper
	{
		public static string GetModuleName(string assemblyFilename)
		{
			try
			{
				string assemblyFullPath = Path.GetFullPath(assemblyFilename);
				return GetModuleName(Assembly.LoadFile(assemblyFullPath));
			}
			catch
			{
				return null;
			}
		}

		public static string GetModuleName(Assembly assembly)
		{
			foreach (ModuleAttribute attrib in assembly.GetCustomAttributes(typeof (ModuleAttribute), false))
				return attrib.Name;

			return null;
		}

		public static IList<string> GetModuleDependencies(string assemblyFilename)
		{
			try
			{
				return GetModuleDependencies(Assembly.LoadFile(assemblyFilename));
			}
			catch
			{
				return new List<string>();
			}
		}

		public static IList<string> GetModuleDependencies(Assembly assembly)
		{
			List<string> results = new List<string>();

			foreach (ModuleDependencyAttribute attrib in assembly.GetCustomAttributes(typeof (ModuleDependencyAttribute), false))
				results.Add(attrib.Name);

			return results;
		}
	}
}