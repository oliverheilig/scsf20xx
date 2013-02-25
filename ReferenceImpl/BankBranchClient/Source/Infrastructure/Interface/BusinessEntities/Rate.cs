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

namespace GlobalBank.Infrastructure.Interface.BusinessEntities
{
	[Serializable]
    public class Rate
	{
		private decimal _interestRate;
		private decimal _minimunAmount;
		private decimal _maximunAmount;
		private int _start;
		private int _end;

		public decimal InterestRate
		{
			get { return _interestRate; }
			set { _interestRate = value; }
		}

		public decimal MinimumAmount
		{
			get { return _minimunAmount; }
			set { _minimunAmount = value; }
		}

		public decimal MaximumAmount
		{
			get { return _maximunAmount; }
			set { _maximunAmount = value; }
		}

		public int Start
		{
			get { return _start; }
			set { _start = value; }
		}

		public int End
		{
			get { return _end; }
			set { _end = value; }
		}
	}

}
