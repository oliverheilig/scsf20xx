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
using System.Configuration;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Configuration;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Properties;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	/// Factory class to create and initialize a <see cref="ConnectionMonitor"/> instance from configuration.
	/// </summary>
	public class ConnectionMonitorFactory
	{
		private static ConnectionMonitor _instance;
		private static object _syncObject = new object();

		/// <summary>
		/// This factory method initializes the ConnectionMonitor single instance adding networks acording to the 
		/// native Connection Manager API and connections from the deafault configuration section
		/// "ConnectionMonitor".
		/// </summary>
		/// <returns>The ConnectionMonitor instance.</returns>
		public static ConnectionMonitor CreateFromConfiguration()
		{
			return CreateFromConfiguration("ConnectionMonitor");
		}

		/// <summary>
		///	This factory method initializes the ConnectionMonitor single instance adding networks acording to the 
		///	native Connection Manager API and connections from the supplied configuration section
		///	name.
		/// </summary>
		/// <param name="sectionName">
		///	Name of the section in the configuration file (App.config), wich contains the different
		///	types of connection
		/// </param>
		/// <returns>The <see cref="ConnectionMonitor"/> instance.</returns>
		public static ConnectionMonitor CreateFromConfiguration(string sectionName)
		{
			lock (_syncObject)
			{
				if (_instance == null)
				{
					ConnectionSettingsSection configuration = ConfigurationManager.GetSection(sectionName) as ConnectionSettingsSection;

					NetworkCollection networks = null;

					if (configuration != null)
					{
						// Create the strategy for network detection
						if (!String.IsNullOrEmpty(configuration.Networks.StrategyType))
						{
							Type strategyType = Type.GetType(configuration.Networks.StrategyType);
							INetworkStatusStrategy statusService = (INetworkStatusStrategy) Activator.CreateInstance(strategyType);
							networks = new NetworkCollection(statusService);
						}
						else
						{
							networks = new NetworkCollection();
						}


						foreach (NetworkElement networkConfig in configuration.Networks)
						{
							networks.Add(new Network(networkConfig.Name, networkConfig.Address));
						}

						ConnectionCollection connections = new ConnectionCollection();

						foreach (ConnectionItemElement itemConnection in configuration.Connections)
						{
							connections.Add(ConnectionFactory.CreateConnection(itemConnection.Type, itemConnection.Price));
						}

						_instance = new ConnectionMonitor(connections, networks);
					}
					else
					{
						_instance = new ConnectionMonitor();
					}

					if (_instance.Connections.Count == 0)
					{
						_instance.Connections.Add(new NicConnection("NicConnection", 0));
					}
				}
			}
			return _instance;
		}

		/// <summary>
		/// Provides access to the single instance of the ConnectionMonitor.
		/// </summary>
		public static ConnectionMonitor Instance
		{
			get
			{
				lock (_syncObject)
				{
					if (_instance == null)
					{
						throw new InvalidOperationException(Resources.InstanceNotInitialized);
					}
					return _instance;
				}
			}
		}

		/// <summary>
		/// Provides support for testability.
		/// </summary>
		protected virtual ConnectionMonitor ConnectionMonitor
		{
			get { return _instance; }
			set { _instance = value; }
		}
	}
}