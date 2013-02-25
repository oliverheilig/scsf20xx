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
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{

	public class TernaryOperationValueProvider : ValueProvider, IAttributesConfigurable
	{
		private string _trueValueExpression;
		private string _falseValueExpression;
		private string _conditionValueExpression;

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			object trueValue = null, falseValue =null;
			bool conditionValue = false;

            trueValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            _trueValueExpression);
            falseValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            _falseValueExpression);
            conditionValue = (bool)ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            _conditionValueExpression);

			newValue = conditionValue ? trueValue : falseValue;
			return !object.Equals(currentValue,newValue);
		}

		public void Configure(StringDictionary attributes)
		{
			_trueValueExpression = attributes["TrueValue"];
			_falseValueExpression = attributes["FalseValue"];
			_conditionValueExpression = attributes["ConditionValue"];
		}
	}
}
