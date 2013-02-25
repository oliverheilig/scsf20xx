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
namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// A <see cref="Connection"/> implementation to use for local services.
	/// In addtion this connection type supports the manual connect and disconnect behavior.
	/// </summary>
	public class DesktopConnection : Connection
	{
		private bool isConnected = true;

		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="connectionTypeName">The type name to use for this connection.</param>
		/// <param name="price">The associated price to use this connection.</param>
		public DesktopConnection(string connectionTypeName, int price)
			: base(connectionTypeName, price)
		{
		}

		/// <summary>
		/// Gets the connected status.
		/// </summary>
		public override bool IsConnected
		{
			get { return isConnected; }
		}

		/// <summary>
		/// Returns a string containing detailed information about the connection 
		/// </summary>
		public override string GetDetailedInfoString()
		{
			return "Desktop connection";
		}

		/// <summary>
		/// Sets the connected status to <see langword="true"/>.
		/// </summary>
		public void Connect()
		{
			isConnected = true;
			RaiseStateChanged(new StateChangedEventArgs(isConnected));
		}

		/// <summary>
		/// Sets the connected status to <see langword="false"/>.
		/// </summary>
		public void Disconnect()
		{
			isConnected = false;
			RaiseStateChanged(new StateChangedEventArgs(isConnected));
		}
	}
}