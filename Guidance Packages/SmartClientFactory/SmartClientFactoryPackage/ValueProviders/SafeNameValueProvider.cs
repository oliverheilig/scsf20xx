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
using System.Text;
using Microsoft.Practices.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.Practices.Common;
using System.Text.RegularExpressions;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(IDictionaryService))]
    public class SafeNameValueProvider : ValueProvider, IAttributesConfigurable
    {
        private string _input;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {

            if (string.IsNullOrEmpty(currentValue as string))
            {
                IDictionaryService dictservice = (IDictionaryService)
                    ServiceHelper.GetService(this, typeof(IDictionaryService));

                string input = (string)dictservice.GetValue(_input);
                newValue = Regex.Replace(input, @"\.", "");
                return true;
            }
            return base.OnBeginRecipe(currentValue, out newValue);
        }

        public void Configure(System.Collections.Specialized.StringDictionary attributes)
        {
            _input = attributes["Input"];
        }
    }
}
