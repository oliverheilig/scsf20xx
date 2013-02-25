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
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;

namespace GlobalBank.BasicAccounts.Module.Tests.Mocks
{
	public class MockQuoteService : IQuoteService
	{
		public Rate[] Rates = new Rate[0];
		public decimal InterestRate = 10;

		public MockQuoteService()
		{
			Rates = new Rate[2];
			Rates[0] = new Rate();
			Rates[0].InterestRate = 10;
			Rates[0].MinimumAmount = 1;
			Rates[0].MaximumAmount = 1000;
			Rates[0].Start = 1;
			Rates[0].End = 30;

			Rates[1] = new Rate();
			Rates[1].InterestRate = 12;
			Rates[1].MinimumAmount = 1001;
			Rates[1].MaximumAmount = 12000;
			Rates[1].Start = 1;
			Rates[1].End = 30;

		}
		public Rate[] GetRates()
		{
			return Rates;
		}

		public decimal GetInterestRate(decimal amount, int duration)
		{
			if (amount > 0 && amount <= 1000)
				return Rates[0].InterestRate;

			if (amount > 1000 && amount <= 10000)
				return Rates[1].InterestRate;

			return -100;	// uh?
		}


		public Quote GetRate(decimal amount, int duration)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
