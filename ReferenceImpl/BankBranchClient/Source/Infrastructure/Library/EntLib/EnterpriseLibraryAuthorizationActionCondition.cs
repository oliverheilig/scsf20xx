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
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.EnterpriseLibrary.Security;

namespace GlobalBank.Infrastructure.Library.ActionConditions
{
	public class EnterpriseLibraryAuthorizationActionCondition : IActionCondition
	{
		private IAuthorizationProvider _authzProvider;

		public EnterpriseLibraryAuthorizationActionCondition()
		{
			_authzProvider = AuthorizationFactory.GetAuthorizationProvider();
		}

		public EnterpriseLibraryAuthorizationActionCondition(string module)
		{
			_authzProvider = AuthorizationFactory.GetAuthorizationProvider(module);
		}

		public bool CanExecute(string action, WorkItem context, object caller, object target)
		{
			try
			{
				return _authzProvider.Authorize(Thread.CurrentPrincipal, action);
			}
			catch (InvalidOperationException)
			{
				// rule (action) not found
				return true;
			}
		}
	}
}