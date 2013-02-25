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
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using GlobalBank.Infrastructure.Library.Properties;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class SimpleWinFormAuthenticationService : IAuthenticationService
	{
		private IUserSelectorService _userSelector;
		
		[InjectionConstructor]
		public SimpleWinFormAuthenticationService( [ServiceDependency] IUserSelectorService userSelector)
		{
			_userSelector = userSelector;
		}

		public void Authenticate()
		{
			IUserData user = _userSelector.SelectUser();
			if (user != null)
			{
				GenericIdentity identity = new GenericIdentity(user.Name);
				GenericPrincipal principal = new GenericPrincipal(identity, user.Roles);
				Thread.CurrentPrincipal = principal;
			}
			else
			{
				throw new AuthenticationException(Resources.NoUserProvidedForAuthentication);
			}
		}
	}
}
