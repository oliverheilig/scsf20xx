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
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.Infrastructure.Library.Services
{
    public class SimpleRoleService : IRoleService
    {
        #region IRoleService Members

        public string[] GetRolesForUser(string userName)
        {
            if (userName == "Jerry")
                return new string[] { "Greeter" };
            else if( userName == "Tom" )
                return new string[] { "Officer" };
            else if( userName == "Spike" )
                return new string[] { "BranchManager" };

            return null;
        }

        #endregion
    }
}
