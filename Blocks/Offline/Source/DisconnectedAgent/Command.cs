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
using System.Reflection;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// A command object
	/// </summary>
	public class Command
	{
		private object[] args;
		private object targetObject;
		private string commandName;

		/// <summary>
		/// Command constructor which creates a new command for the corresponding object, method name and
		/// with the given arguments.
		/// It's used from the RequestManager class to implement a command queue.
		/// </summary>
		/// <param name="target">Target object for the command.</param>
		/// <param name="commandName">Method name to be invoked.</param>
		/// <param name="args">Array of parameters to be used during the invoke.</param>
		public Command(object target, string commandName, params object[] args)
		{
			targetObject = target;
			this.commandName = commandName;
			this.args = args;
		}

		/// <summary>
		/// Getter for the command method name to invoke.
		/// </summary>
		public string CommandName
		{
			get { return commandName; }
		}

		/// <summary>
		/// This method invokes the method with the command name using the parameteres.
		/// </summary>
		/// <returns>The result value of the method.</returns>
		public object Execute()
		{
			MethodInfo commandMethod = targetObject.GetType().GetMethod(commandName);
			return commandMethod.Invoke(targetObject, args);
		}
	}
}