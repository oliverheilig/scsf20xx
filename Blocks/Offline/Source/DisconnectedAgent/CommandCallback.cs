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
using System.Reflection;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An object representing a command callback
	/// </summary>
	public class CommandCallback
	{
		private string targetMethodName;
		private Type targetType;

		/// <summary>
		/// Constructor which creates a new command callback object for the 
		/// target type and method name.
		/// It's used as wrapper for callbacks in the dispatching process of requests.
		/// </summary>
		/// <param name="targetType">Type of the target object.</param>
		/// <param name="targetMethodName">Method name to be invoked.</param>
		public CommandCallback(Type targetType, string targetMethodName)
		{
			this.targetType = targetType;
			this.targetMethodName = targetMethodName;
		}

		/// <summary>
		/// Gets the target type.
		/// This is the type for the object to be created.
		/// </summary>
		public Type TargetType
		{
			get { return targetType; }
		}

		/// <summary>
		/// Gets the method name.
		/// This is the name of the method to be invoked.
		/// </summary>
		public string TargetMethodName
		{
			get { return targetMethodName; }
		}

		/// <summary>
		/// Creates a new instance of the TargetType and invokes the method name
		/// with the given parameters.
		/// </summary>
		/// <param name="args">List of arguments for the method.</param>
		/// <returns>Result of the invoked method.</returns>
		public virtual object Invoke(params object[] args)
		{
			object targetInstance = Activator.CreateInstance(targetType);
			MethodInfo method = targetType.GetMethod(targetMethodName);
			return method.Invoke(targetInstance, args);
		}
	}
}