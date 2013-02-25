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
using System.Web.Services.Protocols;
using GlobalBank.BasicAccounts.Interface.Services;
using GlobalBank.BasicAccounts.Properties;
using GlobalBank.BasicAccounts.ServiceProxies;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;

namespace GlobalBank.BasicAccounts.Module.Services
{
	[Service(typeof (IAccountService))]
	public class AccountService : IAccountService
	{
		private IEntityTranslatorService _translator = null;
		private IAccountServiceProxy _accountServiceproxy;

		public AccountService()
			: this(new ServiceProxies.AccountService(Settings.Default.AccountsWebServiceUrl))
		{
		}

		public AccountService
			(
			IAccountServiceProxy accountsProxy
			)
		{
			_accountServiceproxy = accountsProxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}

		public void PurchaseCertificateOfDeposit(Customer customer, Account sourceAccount, decimal amount, int duration, decimal interestRate)
		{
			try
			{
				PurchaseCertidicateOfDepositRequest request = new PurchaseCertidicateOfDepositRequest();
				request.Amount = (float) amount;
				request.Customer = Translator.Translate<ServiceProxies.Customer>(customer);
				request.DurationInDays = duration;
				request.InterestRate = (float) interestRate;
				request.SourceAccount = Translator.Translate<accountType>(sourceAccount);
				_accountServiceproxy.PurchaseCertificateOfDeposit(request);
			}
			catch (SoapException ex)
			{
				if (ex.Detail.FirstChild.LocalName == typeof (InsufficientFundsException).Name)
					throw new InsufficientFundsException();

				throw;
			}
		}
	}
}