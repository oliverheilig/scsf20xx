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
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.Infrastructure.Module.Services
{
    public class GenericPrincipalImpersonationService : IImpersonationService
    {
        private IAuthenticationService _authService = null;
        [InjectionConstructor]
        public GenericPrincipalImpersonationService(
                                    [ServiceDependency] IAuthenticationService authService
                                    )
        {
            _authService = authService;
        }

		public IAuthenticationService AuthenticationService
        {
            get { return _authService; }
        }       

        public IImpersonationContext Impersonate()
        {
            IImpersonationContext context = new GenericPrincipalImpersonationContext();
            context.State = Thread.CurrentPrincipal;
            
            // the AuthenticationService sets the new principal
            _authService.Authenticate();

            return context;
        }
    }
}
