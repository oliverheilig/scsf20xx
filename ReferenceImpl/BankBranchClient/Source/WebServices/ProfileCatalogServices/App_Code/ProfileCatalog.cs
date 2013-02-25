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
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;

namespace ProfileCatalogServices
{
    [WebService(Namespace = "http://schemas.microsoft.com/pag/cab-profile-service/1.0")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProfileCatalog : System.Web.Services.WebService
    {
        // TODO: should we send an XmlNode a IXmlSerializable dictionary ?
        [WebMethod]
        public string GetProfileCatalog( string[] roles )
        {
            ProfileCatalogServiceImplementation.ProfileCatalog impl = new ProfileCatalogServiceImplementation.ProfileCatalog( Server.MapPath( "./" ) );
            return impl.GetProfileCatalog( roles );
        }        

    }

}