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
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.BranchSystems.Properties;
using GlobalBank.BranchSystems.ServiceProxies;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;

namespace GlobalBank.BranchSystems.Module.Services
{
	[Service(typeof (ICustomerFinderService))]
	public class CustomerFinderService : ICustomerFinderService
	{
		private ICustomerFinderServiceProxy _customerFinderProxy;
		private IEntityTranslatorService _translator;

		public CustomerFinderService()
			: this(new ServiceProxies.CustomerFinderService(Settings.Default.CustomerFinderWebServiceUrl))
		{
		}

		public CustomerFinderService(ICustomerFinderServiceProxy proxy)
		{
			_customerFinderProxy = proxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}

		public Customer[] FindCustomer(Customer exampleCustomer)
		{
			CustomerSearchCriteria criteria = Translator.Translate<CustomerSearchCriteria>(exampleCustomer);
			FindCustomerResponse response = _customerFinderProxy.FindCustomer(criteria);
			return Translator.Translate<Customer[]>(response.Customers);
		}
	}
}