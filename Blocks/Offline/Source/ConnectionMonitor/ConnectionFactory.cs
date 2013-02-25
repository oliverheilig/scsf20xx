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
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	/// A simple factory that can create <see cref="Connection"/> objects.
	/// </summary>
	public class ConnectionFactory
	{
		/// <summary>
		/// DesktopConnection
		/// </summary>
		public const string DesktopConnection = "DesktopConnection";

		/// <summary>
		/// NicConnection
		/// </summary>
		public const string NicConnection = "NicConnection";

		/// <summary>
		/// WirelessConnection
		/// </summary>
		public const string WirelessConnection = "WirelessConnection";

		/// <summary>
		/// WiredConnection
		/// </summary>
		public const string WiredConnection = "WiredConnection";

		/// <summary>
		/// Creates a <see cref="Connection"/> object.
		/// </summary>
		/// <param name="connectionType">The type of the connection to create.</param>
		/// <param name="price">The price of the <see cref="Connection"/>.</param>
		/// <returns>A <see cref="Connection"/> object.</returns>
		/// <exception cref="ConnectionMonitorException">Thrown when an invalid type is requested.</exception>
		/// <remarks>
		/// For the built-in <see cref="Connection"/> types 
		/// (i.e. DesktopConnection, NicConnection, WirelessConnection, WiredConnection), 
		/// only the class name is required.  For user created types, the fully qualified 
		/// class name is required.
		/// </remarks>
		public static Connection CreateConnection(string connectionType, int price)
		{
			Guard.StringNotNullOrEmpty(connectionType, "connectionType");
			
			if (price < 0)
			{
				throw new ArgumentOutOfRangeException("price");
			}

			Connection connection;
			switch (connectionType)
			{
				case DesktopConnection:
					connection = new DesktopConnection(DesktopConnection, price);
					break;
				case NicConnection:
					connection = new NicConnection(NicConnection, price);
					break;
				case WirelessConnection:
					connection = new WirelessConnection(WirelessConnection, price);
					break;
				case WiredConnection:
					connection = new WiredConnection(WiredConnection, price);
					break;
				default:
					try
					{
						Type connectionTypeToCreate = Type.GetType(connectionType, true);
						Object createdObject = Activator.CreateInstance(connectionTypeToCreate, connectionTypeToCreate.Name, price);
						connection = createdObject as Connection;
					}
					catch(TypeLoadException ex)
					{
						throw new ConnectionMonitorException("Unsupported connection type.", ex);
					}
					
					if (connection == null)
					{
						throw new ConnectionMonitorException("Unsupported connection type.");
					}
					break;
			}
			return connection;
		}

	}
}
