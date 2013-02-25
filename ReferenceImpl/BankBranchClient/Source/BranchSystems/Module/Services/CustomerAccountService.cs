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
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;

namespace GlobalBank.BranchSystems.Module.Services
{
	[Service(typeof (ICustomerAccountService))]
	public class CustomerAccountService : ICustomerAccountService
	{
		private ICustomerProductsServiceProxy _productsServiceProxy;
		private IEntityTranslatorService _translator = null;

		public CustomerAccountService()
			: this(new CustomerProductsService(Settings.Default.ProductsWebServiceUrl))
		{
		}

		public CustomerAccountService
			(
			ICustomerProductsServiceProxy productsProxy
			)
		{
			_productsServiceProxy = productsProxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}


		public Account[] GetCustomerAccounts(Customer exampleCustomer)
		{
			getCustomerAccountsRequestType request = new getCustomerAccountsRequestType();
			request.customerId = exampleCustomer.CustomerId;
			getCustomerAccountsResponseType response = _productsServiceProxy.GetCustomerAccounts(request);
			return Translator.Translate<Account[]>(response.accounts);
		}

		public CreditCard[] GetCustomerCreditCards(Customer exampleCustomer)
		{
			getCustomerAccountsRequestType request = new getCustomerAccountsRequestType();
			request.customerId = exampleCustomer.CustomerId;
			getCustomerAccountsResponseType response = _productsServiceProxy.GetCustomerAccounts(request);
			return Translator.Translate<CreditCard[]>(response.creditCards);
		}
	}
}