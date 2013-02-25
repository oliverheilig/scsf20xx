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
	/// Status of a <see cref="Command"/> at any given time.
	/// </summary>
	public enum CommandStatus
	{
		/// <summary>
		/// <see cref="Command"/> will fire the <see cref="Command.ExecuteAction"/> event
		/// when it is executed (either directly through <see cref="Command.Execute"/> or 
		/// through an <see cref="CommandAdapter.ExecuteCommand"/> event in a registered 
		/// adapter. Adapters should show the appropriate UI elements.
		/// </summary>
		Enabled,

		/// <summary>
		/// <see cref="Command"/> will not fire the <see cref="Command.ExecuteAction"/> event
		/// even if it is executed (either directly through <see cref="Command.Execute"/> or 
		/// through an <see cref="CommandAdapter.ExecuteCommand"/> event in a registered 
		/// adapter. Adapters should disable UI elements (or equivalent 
		/// operation available for it), meaning that it will be 
		/// visible but not enabled, in the general case.
		/// </summary>
		/// <remarks>
		/// Trying to execute a disabled command does not perform any action and does 
		/// not raise any exceptions.
		/// </remarks>
		Disabled,

		/// <summary>
		/// <see cref="Command"/> will not fire the <see cref="Command.ExecuteAction"/> event
		/// even if it is executed (either directly through <see cref="Command.Execute"/> or 
		/// through an <see cref="CommandAdapter.ExecuteCommand"/> event in a registered 
		/// adapter. Adapters should hide UI elements (or equivalent 
		/// operation available for it), meaning that it will be 
		/// visible but not enabled, in the general case.
		/// </summary>
		/// <remarks>
		/// Trying to execute an unavailable command does not perform any action and does 
		/// not raise any exceptions.
		/// </remarks>
		Unavailable,
	}
}
