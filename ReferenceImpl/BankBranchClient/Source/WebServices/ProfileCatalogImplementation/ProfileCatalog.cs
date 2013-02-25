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
using System.IO;

namespace ProfileCatalogServiceImplementation
{
    public class ProfileCatalog
    {
        private string _basePath;
        public ProfileCatalog( string basePath )
        {
            _basePath = basePath;
        }

        public string GetProfileCatalog(string[] roles)
        {
            string catalogFilePath;

            catalogFilePath = GetCatalogFilePath(roles);

            string result;

            try
            {
                result = File.ReadAllText(catalogFilePath);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string GetCatalogFilePath(string[] roles)
        {
            string catalogFilePath;
            if (Array.IndexOf(roles, "tester") > -1)
                catalogFilePath = "TestableProfileCatalog.xml";
            else
                catalogFilePath = "ProfileCatalog.xml";

            return Path.Combine( _basePath, catalogFilePath );
        }
    }
}
