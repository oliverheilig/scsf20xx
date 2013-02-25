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
using Microsoft.VisualStudio.TextTemplating;
using System.Reflection;
using Microsoft.Practices.SmartClientFactory.Converters;
using EnvDTE;
using System.Collections.ObjectModel;
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory
{
	public abstract class AgentTemplateHelper
	{
        public static MethodBehaviors BuildBehaviors(Type proxyType, MethodBehaviors behaviorOverrides, List<MethodInfo> methodsList)
		{
			if (behaviorOverrides == null) behaviorOverrides = new MethodBehaviors();
            List<MethodInfo> methods = ProxyMethodsConverter.GetMethods(proxyType, methodsList);
			
			// Copy received ones so that we don't modify original argument value.
			MethodBehaviors behaviors = new MethodBehaviors();
			if (behaviorOverrides != null)
			{
				foreach (MethodBehavior behavior in behaviorOverrides)
				{
					behaviors.Add(behavior);
                    MethodBehaviorHelper.RemoveMethod(methods,behavior.Method);
				}
			}

			// Add the remaining ones as not-overriden behaviors.
			foreach (MethodInfo method in methods)
			{
				behaviors.Add(new MethodBehavior(method, false));
			}

			return behaviors;
		}

		public static List<string> BuildImports(List<MethodInfo> methods)
		{
			// We may get up to one import for each return type and parameter.
			List<string> imports = new List<string>(methods.Count * 2);

			foreach (MethodInfo	method in methods)
			{
				if (!imports.Contains(method.ReturnType.Namespace))
				{
					imports.Add(method.ReturnType.Namespace);
				}
				foreach (ParameterInfo parameter in method.GetParameters())
				{
					if (!imports.Contains(parameter.ParameterType.Namespace))
					{
						imports.Add(parameter.ParameterType.Namespace);
					}
				}
			}

			return imports;
		}

		public static List<string> BuildImports(MethodBehaviors behaviors)
		{
			List<MethodInfo> methods = new List<MethodInfo>(behaviors.Count);

			foreach (MethodBehavior behavior in behaviors)
			{
				methods.Add(behavior.Method);
			}

			return BuildImports(methods);
		}

        public static bool HasOverloadedMethods(string methodName, MethodBehaviors methodBehaviors)
        {
            List<MethodBehavior> filteredMethods = methodBehaviors.FindAll(delegate(MethodBehavior innerBehavior)
                                        {
                                            return innerBehavior.Method.Name == methodName;
                                        });
            return filteredMethods.Count > 1;
        }

        public static int GetOverloadedMethodCount(string methodName, Dictionary<string, int> overloadedMethods)
        {
            if (!overloadedMethods.ContainsKey(methodName))
            {
                overloadedMethods.Add(methodName, 0);
            }
            overloadedMethods[methodName] += 1;
            return overloadedMethods[methodName];
        }
	}
}
