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
using System.Reflection;
using System.Globalization;
using Microsoft.Practices.SmartClientFactory.DSACompatibleTypes;

namespace Microsoft.Practices.SmartClientFactory.Helpers
{
    public sealed class MethodBehaviorHelper
    {
        private const string DefaultBehaviorMethodNameFormat = "Get{0}DefaultBehavior";

        public static MethodBehaviors GetMethodsBehavior(Type dsaType, Type proxyType)
        {
            MethodBehaviors behaviors = new MethodBehaviors();
            List<MethodInfo> methods = GetAgentMethods(dsaType, proxyType);
            OfflineBehavior defaultBehavior = GetDefaultBehavior(dsaType);

            foreach (MethodInfo method in methods)
            {
                OfflineBehavior methodOfflineBehavior = GetMethodBehavior(dsaType, method.Name);
                MethodBehavior behavior = GenerateMethodBehavior(method, methodOfflineBehavior, defaultBehavior); 

                behaviors.Add(behavior);
            }

            return behaviors;
        }

        private static MethodBehavior GenerateMethodBehavior(MethodInfo method, OfflineBehavior methodOffBehavior, OfflineBehavior defaultOffBehavior)
        {
            MethodBehavior behavior = new MethodBehavior(method, false);
            behavior.Expiration = null;
            behavior.MaxRetries = null;
            behavior.Stamps = null;
            behavior.Tag = null;

            if (methodOffBehavior != null)
            {
                if (defaultOffBehavior == null)
                {
                    behavior.Expiration = methodOffBehavior.Expiration - DateTime.Now;
                    behavior.MaxRetries = methodOffBehavior.MaxRetries;
                    behavior.Stamps = methodOffBehavior.Stamps;
                    behavior.Tag = methodOffBehavior.Tag;
                }
                else
                {
                    TimeSpan methoBehaviorExpiration=new TimeSpan();
                    TimeSpan defaultBehaviorExpiration = new TimeSpan();

                    if (methodOffBehavior.Expiration.HasValue)
                    {
                        methoBehaviorExpiration = methodOffBehavior.Expiration.Value - DateTime.Now;
                        methoBehaviorExpiration = new TimeSpan(methoBehaviorExpiration.Days,
                                                                    methoBehaviorExpiration.Hours,
                                                                    methoBehaviorExpiration.Minutes,
                                                                    methoBehaviorExpiration.Seconds);
                    }
                    if(defaultOffBehavior.Expiration.HasValue)
                    {
                        defaultBehaviorExpiration = defaultOffBehavior.Expiration.Value - DateTime.Now;
                        defaultBehaviorExpiration = new TimeSpan(defaultBehaviorExpiration.Days,
                                                                    defaultBehaviorExpiration.Hours,
                                                                    defaultBehaviorExpiration.Minutes,
                                                                    defaultBehaviorExpiration.Seconds);
                    }

                    if (methoBehaviorExpiration!=null && defaultBehaviorExpiration!=null
                        && !(methoBehaviorExpiration.Equals(defaultBehaviorExpiration)))
                    {
                        behavior.Expiration = methoBehaviorExpiration;
                        behavior.IsOverride = true;
                    }
                    if ( !(methodOffBehavior.MaxRetries.Equals(defaultOffBehavior.MaxRetries)))
                    {
                        behavior.MaxRetries = methodOffBehavior.MaxRetries;
                        behavior.IsOverride = true;
                    }
                    if (!(methodOffBehavior.Stamps.Equals(defaultOffBehavior.Stamps)))
                    {
                        behavior.Stamps = methodOffBehavior.Stamps;
                        behavior.IsOverride = true;
                    }
                    if (methodOffBehavior.Tag!=null
                        && !(methodOffBehavior.Tag.Equals(defaultOffBehavior.Tag)))
                    {
                        behavior.Tag = methodOffBehavior.Tag;
                        behavior.IsOverride = true;
                    }
                }
            }
            return behavior;
        }

        private static OfflineBehavior GetMethodBehavior(Type dsaType, string methodName)
        {
            string agentGetBehaviorMethodName = string.Format(CultureInfo.InvariantCulture,
                                                    DefaultBehaviorMethodNameFormat,
                                                    methodName);
            MethodInfo method = dsaType.GetMethod(agentGetBehaviorMethodName);
            object returnValue = null;
            if (method != null
                && method.IsStatic
                && method.GetParameters().Length == 0)
            {
                returnValue = method.Invoke(null, null);
            }
            if (returnValue != null)
            {
                return TranslateToOfflineBehavior(returnValue);
            }
            return null;
        }
        public static OfflineBehavior TranslateToOfflineBehavior(object offlineBehavior)
        {
            OfflineBehavior properBehavior=new OfflineBehavior();
            Type offlineBehaviorType = offlineBehavior.GetType();

            PropertyInfo expirationProperty = offlineBehaviorType.GetProperty("Expiration");
            if (expirationProperty != null)
            {
                properBehavior.Expiration = (DateTime?)expirationProperty.GetValue(offlineBehavior, null);
            }
            PropertyInfo stampsProperty = offlineBehaviorType.GetProperty("Stamps");
            if (stampsProperty != null)
            {
                properBehavior.Stamps = (int)stampsProperty.GetValue(offlineBehavior, null);
            }
            PropertyInfo maxRetriesProperty = offlineBehaviorType.GetProperty("MaxRetries");
            if (maxRetriesProperty != null)
            {
                properBehavior.MaxRetries = (int)maxRetriesProperty.GetValue(offlineBehavior, null);
            }
            PropertyInfo tagProperty = offlineBehaviorType.GetProperty("Tag");
            if (tagProperty != null)
            {
                properBehavior.Tag = (string)tagProperty.GetValue(offlineBehavior, null);
            }

            return properBehavior;
        }

        private static OfflineBehavior GetDefaultBehavior(Type dsaType)
        {
            return GetMethodBehavior(dsaType,"Agent");
        }

        public static List<MethodInfo> GetProxyMethods(Type proxyType)
        {
            List<MethodInfo> methods = new List<MethodInfo>(proxyType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            methods.RemoveAll(delegate(MethodInfo innerMethod)
                                    {
                                        return innerMethod.IsSpecialName;
                                    });
            return methods;
        }

        public static List<MethodInfo> GetAgentMethods(Type proxyAgentType, Type proxyType)
        {
            List<MethodInfo> agentMethods = new List<MethodInfo>(proxyAgentType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance));
            List<MethodInfo> proxyMethods = GetProxyMethods(proxyType);

            return GetEqualMethods(proxyMethods, agentMethods);
        }

        public static List<string> GetMethodNames(List<MethodInfo> methods)
        {
            return methods.ConvertAll<string>(delegate(MethodInfo innerMethod)
                                                {
                                                    return innerMethod.Name;
                                                });
        }

        public static List<MethodInfo> GetEqualMethods(List<MethodInfo> methods1, List<MethodInfo> methods2)
        {
            List<MethodInfo> filteredMethods=new List<MethodInfo>();
            foreach (MethodInfo method1 in methods1)
            {
                foreach (MethodInfo method2 in methods2)
                {
                    if (EqualMethods(method1,method2))
                    {
                        filteredMethods.Add(method1);
                    }
                }
            }
            return filteredMethods;
        }

        public static bool RemoveMethod(List<MethodInfo> methodList, MethodInfo targetMethod)
        {
            foreach (MethodInfo method in methodList)
            {
                if (EqualMethods(method, targetMethod))
                {
                    return methodList.Remove(method);
                }
            }
            return false;
        }

        public static bool EqualMethods(MethodInfo method1, MethodInfo method2)
        {
            return method1!=null && method2!=null
                && method1.Name == method2.Name
                && HasSameParameterTypes(method1, method2);
        }

        public static bool HasSameParameterTypes(MethodInfo method1, MethodInfo method2)
        {
            ParameterInfo[] parameters1 = method1.GetParameters();
            ParameterInfo[] parameters2 = method2.GetParameters();

            if (parameters1.Length != parameters2.Length)
            {
                return false;
            }
            else
            {
                foreach (ParameterInfo param1 in parameters1)
                {
                    foreach (ParameterInfo param2 in parameters2)
                    {
                        if (param1.Position == param2.Position
                            && param1.ParameterType != param2.ParameterType)
                        {
                            return false;
                        }
                    }
                }
            }
            
            return true;
        }
    }
}
