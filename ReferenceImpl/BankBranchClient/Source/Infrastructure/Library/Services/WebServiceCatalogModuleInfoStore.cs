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
using GlobalBank.Infrastructure.Library.Properties;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class WebServiceCatalogModuleInfoStore : IModuleInfoStore
	{
		private string _catalogUrl;
		private string[] _roles;
		private IProfileCatalogService _catalogService;

		[InjectionConstructor]
		public WebServiceCatalogModuleInfoStore([ServiceDependency] IProfileCatalogService catalogService)
		{
			_catalogService = catalogService;
			_catalogUrl = Settings.Default.ProfileCatalogWebServiceUrl;
		}

		public string CatalogUrl
		{
			get { return _catalogUrl; }
			set
			{
				Guard.ArgumentNotNullOrEmptyString(value, "Catalog Url");

				_catalogUrl = value;
			}
		}

		public string[] Roles
		{
			get { return _roles; }
			set
			{
				Guard.ArgumentNotNull(value, "Roles");

				_roles = value;
			}
		}

		#region IModuleInfoStore Members

		public string GetModuleListXml()
		{
			_catalogService.Url = _catalogUrl;
			return _catalogService.GetProfileCatalog(_roles);
		}

		#endregion
	}
}