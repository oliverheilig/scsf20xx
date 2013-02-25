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
using System.IO;
using System.Net;
using Microsoft.Practices.SmartClient.EndpointCatalog;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks
{
	public class MockEndpointCatalog : IEndpointCatalog
	{
		public Dictionary<string, MockEndpoint> Endpoints = new Dictionary<string, MockEndpoint>();

		public bool AddressExistsForEndpoint(string endpointName, string networkName)
		{
			if (Endpoints.ContainsKey(endpointName))
			{
				return Endpoints[endpointName].ContainsConfigForNetwork(networkName);
			}
			return false;
		}

		public NetworkCredential GetCredentialForEndpoint(string endpointName, string networkName)
		{
			if (Endpoints.ContainsKey(endpointName))
			{
				return Endpoints[endpointName].GetCredentialForNetwork(networkName);
			}
			else
				throw new KeyNotFoundException("Endpoint doesn't exist in the catalog.");
		}

		public string GetAddressForEndpoint(string endpointName, string networkName)
		{
			if (Endpoints.ContainsKey(endpointName))
			{
				return Endpoints[endpointName].GetAddressForNetwork(networkName);
			}
			else
				throw new KeyNotFoundException("Endpoint doesn't exist in the catalog.");
		}

		public int Count
		{
			get { return Endpoints.Count; }
		}

		public void Save(TextWriter textWriter)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Load(TextReader textReader)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetEndpoint(Endpoint endpoint)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveEndpoint(string endpointName)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool EndpointExists(string endpointName)
		{
			return (endpointName != "MissingEndpoint");
		}

		public void RemoveCredential(string endpointName, string networkName)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetCredentialForEndpoint(string endpointName, string networkName, NetworkCredential credential)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}

	public class MockEndpoint
	{
		private MockEndpointConfig defaultConfig;
		private string name;

		public MockEndpointConfig Default
		{
			get { return defaultConfig; }
			set { defaultConfig = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		public MockEndpoint(string name)
		{
			this.name = name;
		}

		public bool ContainsConfigForNetwork(string networkName)
		{
			if (networkName == "NotForThisNetwork")
				return false;
			else
				return (Default != null);
		}

		public NetworkCredential GetCredentialForNetwork(string networkName)
		{
			if (networkName == "NotForThisNetwork")
				return null;
			else
				return Default.Credential;
		}

		public string GetAddressForNetwork(string networkName)
		{
			if (networkName == "NotForThisNetwork")
			{
				throw new KeyNotFoundException("There is not address for this Endpoint/Network");
			}
			else
				return Default.Address;
		}
	}

	public class MockEndpointConfig
	{
		public MockEndpointConfig(string adress)
		{
			address = adress;
		}

		private NetworkCredential credential;
		private string address;

		public string Address
		{
			get { return address; }
			set { address = value; }
		}


		public NetworkCredential Credential
		{
			get { return credential; }
			set { credential = value; }
		}
	}
}