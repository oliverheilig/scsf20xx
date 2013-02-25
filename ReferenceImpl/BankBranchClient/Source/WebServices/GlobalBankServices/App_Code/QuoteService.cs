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
using System.Collections.Generic;

namespace GlobalBankServices
{
	public class QuoteService : IBasicHttpBinding_IQuoteService
	{
		List<RateTableEntryType> _ratesTable = new List<RateTableEntryType>();

		public QuoteService()
		{
			_ratesTable.Add(CreateRate(91, 149, 0, 9999, 1.99));
			_ratesTable.Add(CreateRate(91, 149, 10000, 24999, 3.21));
			_ratesTable.Add(CreateRate(91, 149, 25000, 49999, 3.21));
			_ratesTable.Add(CreateRate(91, 149, 50000, 99999, 3.21));

			_ratesTable.Add(CreateRate(150, 181, 0, 9999, 1.99));
			_ratesTable.Add(CreateRate(150, 181, 10000, 24999, 3.21));
			_ratesTable.Add(CreateRate(150, 181, 25000, 49999, 3.21));
			_ratesTable.Add(CreateRate(150, 181, 50000, 99999, 3.21));

			_ratesTable.Add(CreateRate(182, 239, 0, 9999, 2.14));
			_ratesTable.Add(CreateRate(182, 239, 10000, 24999, 3.38));
			_ratesTable.Add(CreateRate(182, 239, 25000, 49999, 3.38));
			_ratesTable.Add(CreateRate(182, 239, 50000, 99999, 3.38));
			
			_ratesTable.Add(CreateRate(240, 299, 0, 9999, 2.24));
			_ratesTable.Add(CreateRate(240, 299, 10000, 24999, 3.58));
			_ratesTable.Add(CreateRate(240, 299, 25000, 49999, 3.58));
			_ratesTable.Add(CreateRate(240, 299, 50000, 99999, 3.58));

			_ratesTable.Add(CreateRate(300, 599, 0, 9999, 2.96));
			_ratesTable.Add(CreateRate(300, 599, 10000, 24999, 3.63));
			_ratesTable.Add(CreateRate(300, 599, 25000, 49999, 3.63));
			_ratesTable.Add(CreateRate(300, 599, 50000, 99999, 3.63));

			_ratesTable.Add(CreateRate(600, int.MaxValue, 0, 9999, 2.96));
			_ratesTable.Add(CreateRate(600, int.MaxValue, 10000, 24999, 3.73));
			_ratesTable.Add(CreateRate(600, int.MaxValue, 25000, 49999, 3.73));
			_ratesTable.Add(CreateRate(600, int.MaxValue, 50000, 99999, 3.73));
		}

		private RateTableEntryType CreateRate(int start, int end, int minimum, int maximum, double interest)
		{
			RateTableEntryType rate = new RateTableEntryType();
			rate.minimumAmount= minimum;
			rate.maximumAmount = maximum;
			rate.rate = Convert.ToDecimal(interest);
			rate.start = start;
			rate.end = end;
			return rate;
		}

		public QuoteResponseType GetRate(QuoteRequestType quoteRequest)
		{
			// This method is defined on the QuoteService contract but we're not using it,
			// so will not be implemented in this mock web service.
			throw new NotImplementedException();
		}

		RateTableEntryType[] IBasicHttpBinding_IQuoteService.GetRates()
		{
			return _ratesTable.ToArray();
		}
	}

}

