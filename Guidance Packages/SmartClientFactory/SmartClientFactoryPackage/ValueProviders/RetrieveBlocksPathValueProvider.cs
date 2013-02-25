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
using System.Linq;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using System.Reflection;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class RetrieveBlocksPathValueProvider : ValueProvider
    {
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {            
            newValue = string.Empty;
            
            if (string.IsNullOrEmpty(currentValue as string))
            {
                newValue = GuidancePackageConfiguration.GetLatestSelectedApplicationBlocksPath();                
                return true;
            }

            newValue = currentValue;
            return false;           
        }
    }

    public class GuidancePackageConfiguration
    {
        public static string GetLatestSelectedApplicationBlocksPath()
        {
            string newValue = string.Empty;

            string baseDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Blocks");            
            if (string.IsNullOrEmpty(newValue.ToString()))
            {
                newValue = baseDirectory;
            }

            return newValue;
        }
    }
}
