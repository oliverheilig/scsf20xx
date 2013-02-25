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

namespace GlobalBank.BranchSystems.Interface.Services
{
	[Serializable]
	public class InsufficientFundsException : Exception
	{
		private decimal _required;
		private decimal _available;

		public InsufficientFundsException(decimal required, decimal available, string message)
			: base(message)
		{
			_available = available;
			_required = required;
		}

		public InsufficientFundsException() : base() { }
		public InsufficientFundsException(string message) : base(message) { }
		public InsufficientFundsException(string message, Exception innerException) : base(message, innerException) { }

		public decimal AvailableAmount
		{
			get { return _available; }
			set { _available = value; }
		}

		public decimal RequiredAmount
		{
			get { return _required; }
			set { _required = value; }
		}
	}
}
