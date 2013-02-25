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
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;

namespace GlobalBank.BasicAccounts.Module.Tests.Mocks
{
	public class MockActionCatalog : IActionCatalogService
	{
		public bool CanExecute(string action, WorkItem context, object caller, object target)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool CanExecute(string action)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Execute(string action, WorkItem context, object caller, object target)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RegisterSpecificCondition(string action, IActionCondition actionCondition)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RegisterGeneralCondition(IActionCondition actionCondition)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveSpecificCondition(string action, IActionCondition actionCondition)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveGeneralCondition(IActionCondition actionCondition)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveActionImplementation(string action)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public List<string> ActionNames = new List<string>();

		public void RegisterActionImplementation(string action, ActionDelegate actionDelegate)
		{
			ActionNames.Add(action);
		}
	}
}
