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
using System.Security.Principal;
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.Infrastructure.Module.Services
{
    public class GenericPrincipalImpersonationContext : IImpersonationContext
    {
        private IPrincipal _oldPrincipal;

        public object State
        {
            get
            {
                return _oldPrincipal;
            }
            set
            {
                _oldPrincipal = value as IPrincipal;
            }
        }        

        public void Undo()
        {
            Thread.CurrentPrincipal = _oldPrincipal;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Undo();
            }
        }        
    }
}
