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
using System.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.Practices.Common;
using System.Reflection;
using System.Collections.Generic;
using EnvDTE;
using System.Diagnostics;
using System.Web.Services.Description;
using System.Collections.ObjectModel;

namespace Microsoft.Practices.SmartClientFactory.Converters
{
	public class ProxyMethodsConverter : StringConverter
	{
		/// <summary>
		/// This is hardcoded and needs to match the argument in the recipe, because 
		/// we cannot pass dynamic values to the converter via the TypeConverterAttribute.
		/// </summary>
        public const string ProxyArgumentName = "ProxyType";
        public const string MethodsArgumentName = "ServiceAgentMethods";

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				Type proxyType = GetProxyType(context);
				return proxyType.GetMethod((string)value);
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is MethodInfo)
			{
				MethodInfo method = (MethodInfo)value;
				return method.ToString();
			}
			else
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (value is string)
			{
				Type proxyType = GetProxyType(context);
				return proxyType.GetMethod((string)value) != null;
			}
			else
			{
				return base.IsValid(context, value);
			}
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
            Type proxyType = GetProxyType(context);
            List<MethodInfo> methodsList = GetMethodsList(context);

			List<MethodInfo> methods = GetMethods(proxyType,methodsList);

			return new StandardValuesCollection(methods);
		}

        public static List<MethodInfo> GetMethods(Type proxyType, List<MethodInfo> operationMethods)
		{
			List<MethodInfo> methods = new List<MethodInfo>();

			foreach (MethodInfo method in proxyType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance))
			{
				if (!method.IsSpecialName && operationMethods.Contains(method))
				{
					methods.Add(method);
				}
			}

			return methods;
		}

        private List<MethodInfo> GetMethodsList(ITypeDescriptorContext context)
        {
            IDictionaryService dictionary = VsHelper.GetService<IDictionaryService>(context, true);
            List<MethodInfo> methodsList = (List<MethodInfo>)dictionary.GetValue(MethodsArgumentName);
            return methodsList;
        }
        private Type GetProxyType(ITypeDescriptorContext context)
		{
			IDictionaryService dictionary = VsHelper.GetService<IDictionaryService>(context, true);
			Type proxyType = (Type)dictionary.GetValue(ProxyArgumentName);
			return proxyType;
		}

    //TODO Remove this method as is only used by Mobile
    //    private static bool IsAsyncMethod(MethodInfo method)
    //    {
    //        if (method.ReturnType == typeof(IAsyncResult))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            ParameterInfo[] parameters = method.GetParameters();
    //            return parameters.Length > 0 &&
    //                parameters[0].ParameterType == typeof(IAsyncResult);
    //        }
    //    }
    }
}
