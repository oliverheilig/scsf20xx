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
using Alert=GlobalBank.Infrastructure.Interface.BusinessEntities.Alert;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;

namespace GlobalBank.BranchSystems.Module.Services
{
	[Service(typeof (ICustomerAlertService))]
	public class CustomerAlertService : ICustomerAlertService
	{
		private IAlertsServiceProxy _alertServiceProxy;
		private IEntityTranslatorService _translator;

		public CustomerAlertService()
			: this(new AlertsService(Settings.Default.AlertsWebServiceUrl))
		{
		}

		public CustomerAlertService(IAlertsServiceProxy proxy)
		{
			_alertServiceProxy = proxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}

		public Alert[] GetCustomerAlerts(Customer exampleCustomer)
		{
			ServiceProxies.Customer svcCustomer = Translator.Translate<ServiceProxies.Customer>(exampleCustomer);
			ServiceProxies.Alert[] result = _alertServiceProxy.GetCustomerAlerts(svcCustomer);
			if (result == null) return null;
			return Translator.Translate<Alert[]>(result);
		}
	}
}