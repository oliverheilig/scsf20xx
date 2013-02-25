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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.SmartClient.ConnectionMonitor;
using Microsoft.Practices.SmartClient.EndpointCatalog;
using Microsoft.Practices.SmartClient.EnterpriseLibrary;
using Microsoft.Practices.SmartClient.DisconnectedAgent;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary
{
	/// <summary>
	/// A static class assists in initializing <see cref="RequestManager"/> objects.
	/// </summary>
	public static class DatabaseRequestManagerIntializer
	{
		/// <summary>
		/// Will initialize an instance of a <see cref="RequestManager"/> using the configure default
		/// database and the endpoint declared in the app.config file.
		/// </summary>
		/// <returns></returns>
		public static RequestManager Initialize()
		{
            SmartClientDatabase db = (SmartClientDatabase)DatabaseFactory.CreateDatabase();
			return InitializeManager(db);
		}


		/// <summary>
		/// Will initialize an instance of a <see cref="RequestManager"/> using the configured database settings 
		/// and endpoints in the app.config file.
		/// </summary>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		public static RequestManager Initialize(string databaseName)
		{
			Guard.ArgumentNotNullOrEmptyString(databaseName, "databaseName");

			SmartClientDatabase db = (SmartClientDatabase) DatabaseFactory.CreateDatabase(databaseName);
			return InitializeManager(db);
		}

		private static RequestManager InitializeManager(SmartClientDatabase db)
		{
			DatabaseRequestQueue requestQueue = new DatabaseRequestQueue(db, "Requests");
			DatabaseRequestQueue deadLetterQueue = new DatabaseRequestQueue(db, "DeadLetter");

			ConnectionMonitor.ConnectionMonitor monitor = ConnectionMonitorFactory.CreateFromConfiguration();
			ConnectionMonitorAdapter monitorAdapter = new ConnectionMonitorAdapter(monitor);
			IEndpointCatalog endpointCatalog = new EndpointCatalogFactory("Endpoints").CreateCatalog();
			// nothing is defined in config.  Create an empty catalog
			if (endpointCatalog == null)
			{
				endpointCatalog = new EndpointCatalog.EndpointCatalog();
			}

			RequestManager manager = RequestManager.Instance;
			manager.Initialize(requestQueue, deadLetterQueue, monitorAdapter, endpointCatalog);

			return manager;
		}
	}
}