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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;

namespace Microsoft.Practices.CompositeUI.Commands
{
	/// <summary>
	/// Declares a method as a <see cref="Command"/> handler.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
	public class CommandHandlerAttribute : Attribute
	{
		private string commandName;

		/// <summary>
		/// Declares a method as a handler for a <see cref="Command"/> with specified name.
		/// </summary>
		/// <param name="commandName">The name of the <see cref="Command"/> the method handles.</param>
		public CommandHandlerAttribute(string commandName)
		{
			this.commandName = commandName;
		}

		/// <summary>
		/// Gets the name of the <see cref="Command"/> that the decorated method handles.
		/// </summary>
		public string CommandName
		{
			get { return this.commandName; }
		}
	}
}