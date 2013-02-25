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
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.Common;
using System.Collections.Specialized;
using System.ComponentModel.Design;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{

    public class ConditionalValueProvider : ValueProvider, IAttributesConfigurable
    {
        private string _trueValue;
        private string _falseValue;
        private string _conditionValue;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            object trueValue = null, falseValue = null;
            bool conditionValue = false;

            IDictionaryService ds = GetService<IDictionaryService>();
            trueValue = ds.GetValue(_trueValue);
            falseValue = ds.GetValue(_falseValue);
            conditionValue = (bool)ds.GetValue(_conditionValue);

            newValue = conditionValue ? trueValue : falseValue;
            return true;
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            return OnBeginRecipe(currentValue, out newValue);
        }

        public void Configure(StringDictionary attributes)
        {
            _trueValue = attributes["TrueValueArgument"];
            _falseValue = attributes["FalseValueArgument"];
            _conditionValue = attributes["ConditionValueArgument"];
        }
    }
}
