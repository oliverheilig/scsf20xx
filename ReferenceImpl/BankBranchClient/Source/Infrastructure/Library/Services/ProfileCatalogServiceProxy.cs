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
using GlobalBank.Infrastructure.Library.ProfileCatalogServiceProxy;
using GlobalBank.Infrastructure.Library.Properties;
using GlobalBank.Infrastructure.Library.Services;

namespace GlobalBank.Infrastructure.Library
{
	public class ProfileCatalogService : IProfileCatalogService, IDisposable
	{
		private ProfileCatalog _proxy = new ProfileCatalog();

		public ProfileCatalogService()
		{
			_proxy = new ProfileCatalog();
			_proxy.Url = Settings.Default.ProfileCatalogWebServiceUrl;
		}

		public string GetProfileCatalog(string[] roles)
		{
			return _proxy.GetProfileCatalog(roles);
		}

		public string Url
		{
			get { return _proxy.Url; }
			set { _proxy.Url = value; }
		}

		~ProfileCatalogService()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_proxy.Dispose();
			}
		}
	}
}