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
namespace GlobalBankServices
{
	public class CustomerProductsService : ICustomerProductsServiceSoap
	{
		public getCustomerAccountsResponseType GetCustomerAccounts(getCustomerAccountsRequestType getCustomerAccountsRequest)
		{
			GlobalBankDataServices db = new GlobalBankDataServices();
			getCustomerAccountsResponseType response = new getCustomerAccountsResponseType();
			response.customerId = getCustomerAccountsRequest.customerId;
			response.accounts = db.GetCustomerAccounts(getCustomerAccountsRequest.customerId);
			response.creditCards = db.GetCustomerCreditCards(getCustomerAccountsRequest.customerId);
			return response;
		}
	}
}