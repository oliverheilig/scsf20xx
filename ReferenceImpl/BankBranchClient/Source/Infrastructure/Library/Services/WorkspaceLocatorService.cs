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
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Microsoft.Practices.SmartClient.Library
{
	public class WorkspaceLocatorService : IWorkspaceLocatorService
	{
		public IWorkspace FindContainingWorkspace(WorkItem workItem, object smartPart)
		{
			while (workItem != null)
			{
				foreach (KeyValuePair<string, IWorkspace> namedWorkspace in workItem.Workspaces)
				{
					if (namedWorkspace.Value.SmartParts.Contains(smartPart))
						return namedWorkspace.Value;
				}
				workItem = workItem.Parent;
			}
			return null;
		}
	}
}