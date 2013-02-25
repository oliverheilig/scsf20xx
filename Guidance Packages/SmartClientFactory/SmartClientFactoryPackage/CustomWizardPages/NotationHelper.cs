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
using System.Text.RegularExpressions;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public static class NotationHelper
    {
        public static string ParseClrNotationToGenericName(string genericTypeFullName)
        {
            Type genericType = Type.GetType(genericTypeFullName);
            if (genericType!=null && genericType.IsGenericType)
            {
                Type[] genericParameterTypesArray = genericType.GetGenericArguments();
                string[] genericParameterNamesArray;
                genericParameterNamesArray = Array.ConvertAll<Type, string>(genericParameterTypesArray, new Converter<Type, string>(SelectTypeName));

                string genericParametersNamesCommaSeparated = String.Join(",", genericParameterNamesArray);
                string genericParametersExpression = string.Format("<{0}>", genericParametersNamesCommaSeparated);

                Regex genericCLRNotationToReplace = new Regex(@"\`\d+");
                string eventArgsGenericFormat = genericCLRNotationToReplace.Replace(genericTypeFullName, "{0}", 1);

                genericTypeFullName = string.Format(eventArgsGenericFormat, genericParametersExpression);
            }
            return genericTypeFullName;
        }

        private static string SelectTypeName(Type type)
        {
            return type.Name;
        }

        public static string ParseGenericNameToCLRNotation(string genericClass)
        {
            string CLRNotationClass;
            int argumentBegin = genericClass.IndexOf("<");
            if (argumentBegin != -1)
            {
                int argumentLength = genericClass.LastIndexOf(">") - argumentBegin + 1;

                string genericArguments = genericClass.Substring(argumentBegin + 1, argumentLength - 2);
                Regex genericArgumentsReplace = new Regex(@"\<\S*\>");
                string genericArgumentsCountable = genericArgumentsReplace.Replace(genericArguments, string.Empty);

                int genericCount = genericArgumentsCountable.Split(',').Length;

                CLRNotationClass = genericClass.Remove(argumentBegin, argumentLength);
                CLRNotationClass = CLRNotationClass.Insert(argumentBegin, string.Format("`{0}", genericCount));
            }
            else
            {
                CLRNotationClass = genericClass;
            }
            return CLRNotationClass;
        }

        public static string RemoveTrailingCLRChars(string clrNotation)
        {
            Regex genericCLRNotationToReplace = new Regex(@"\`\d+");
            string genericNotation=genericCLRNotationToReplace.Replace(clrNotation, string.Empty);
            return genericNotation;
        }
    }
}
