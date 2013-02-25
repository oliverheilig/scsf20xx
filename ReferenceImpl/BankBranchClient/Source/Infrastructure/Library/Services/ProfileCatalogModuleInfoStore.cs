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
using System.IO;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class ProfileCatalogModuleInfoStore : IModuleInfoStore
	{
		private string _catalogFilePath;

		public ProfileCatalogModuleInfoStore()
		{
			_catalogFilePath = "ProfileCatalog.xml";
		}

		public string CatalogFilePath
		{
			get { return _catalogFilePath; }
			set
			{
				Guard.ArgumentNotNullOrEmptyString(value, "Catalog File Path");

				_catalogFilePath = value;
			}
		}

		public string GetModuleListXml()
		{
			string result;

			try
			{
				result = File.ReadAllText(_catalogFilePath);
			}
			catch
			{
				result = null;
			}

			return result;
		}
	}
}
