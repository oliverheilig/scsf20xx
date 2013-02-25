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
using System.IO;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Services;
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Practices.ComponentModel;
using EnvDTE;
using EnvDTE80;
using Microsoft.Win32;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class VBInstallationCheck : ValueProvider
    {
        const string vbInstallRegistryPathForVS2010 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\10.0\InstalledProducts\Microsoft Visual Basic";

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = EvaluateVBInstalled();
            return true;
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            newValue = EvaluateVBInstalled();
            return true;
        }

        private bool EvaluateVBInstalled()
        {
            object VBPresent = Registry.GetValue(vbInstallRegistryPathForVS2010, "Package", null);

            if (VBPresent != null)
            {
                return true;
            }

            return false;
        }
    }
}
