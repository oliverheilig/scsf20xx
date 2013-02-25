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
using GlobalBank.Infrastructure.Library.SolutionProfileV1;
using Microsoft.Practices.CompositeUI.Configuration;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class SolutionProfileV1Parser : ISolutionProfileParser
	{
		public const string Namespace = "http://schemas.microsoft.com/pag/cab-profile";

		public IModuleInfo[] Parse(string xml)
		{
			SolutionProfileElement solution = XmlValidationHelper.DeserializeXml<SolutionProfileV1.SolutionProfileElement>(xml,
			                                                                                                               "SolutionProfileV1.xsd",
			                                                                                                               Namespace);

			List<ModuleInfo> mis = new List<ModuleInfo>();

			if (solution.Modules != null)
			{
				foreach (ModuleInfoElement moduleInfo in solution.Modules)
				{
					ModuleInfo mi = new ModuleInfo(moduleInfo.AssemblyFile);
					SetModuleRoles(moduleInfo, mi);
					mis.Add(mi);
				}
			}

			return mis.ToArray();
		}

		private static void SetModuleRoles(ModuleInfoElement moduleInfo, ModuleInfo mi)
		{
			if (moduleInfo.Roles != null && moduleInfo.Roles.Length > 0)
				foreach (RoleElement role in moduleInfo.Roles)
					mi.AddRoles(role.Allow);
		}
	}
}