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
namespace GlobalBank.Infrastructure.Interface.BusinessEntities
{
	public class Quote
	{
		private int _duration;
		private decimal _rate;
		private decimal _amount;

		public decimal Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}
	
		public decimal Rate
		{
			get { return _rate; }
			set { _rate = value; }
		}
	
		public int Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}
	
	}
}
