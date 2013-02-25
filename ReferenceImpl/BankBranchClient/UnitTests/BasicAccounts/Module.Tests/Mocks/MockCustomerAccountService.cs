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
using GlobalBank.Infrastructure.Interface.BusinessEntities;

namespace GlobalBank.BasicAccounts.Module.Tests.Mocks
{
	class MockCustomerAccountService : ICustomerAccountService
	{
		public bool GetCustomerAccountsCalled;
		public bool GetCustomerCreditCardsCalled;

		public Account[] Accounts = new Account[0];
		public CreditCard[] CreditCards = new CreditCard[0];

		public Account[] GetCustomerAccounts(Customer customer)
		{
			GetCustomerAccountsCalled = true;

			return Accounts;
		}

		public CreditCard[] GetCustomerCreditCards(Customer customer)
		{
			GetCustomerCreditCardsCalled = true;

			return CreditCards;
		}
		
	}
}
