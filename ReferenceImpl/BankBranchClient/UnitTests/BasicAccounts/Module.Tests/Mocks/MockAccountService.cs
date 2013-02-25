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
using GlobalBank.BasicAccounts.Interface.Services;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;

namespace GlobalBank.BasicAccounts.Module.Tests.Mocks
{
    public class MockAccountService : IAccountService
    {

        public Account CDAccount;

        public void PurchaseCertificateOfDeposit(Customer customer, Account source, decimal amount, int duration, decimal interestRate)
        {
            if (Convert.ToDecimal(source.Balance) < amount)
                throw new InsufficientFundsException("Isufficient funds");

            CDAccount = new Account();
            CDAccount.AccountType = AccountType.CD;
            CDAccount.InterestRate = Convert.ToSingle(interestRate);
            CDAccount.Balance = Convert.ToSingle(amount);
            CDAccount.CustomerId = customer.CustomerId;
        }
    }
}
