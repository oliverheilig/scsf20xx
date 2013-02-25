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
using System.Globalization;

namespace GlobalBank.Infrastructure.Interface.BusinessEntities
{
	public class Account
	{
		private DateTime _lastTransactionDate;
		private DateTime _dateOpened;
		private float _balance;
		private float _interestRate;
		private AccountType _accountType;
		private string _customerId;
		private string _accountNumber;

		public DateTime LastTransactionDate
		{
			get { return _lastTransactionDate; }
			set { _lastTransactionDate = value; }
		}

		public DateTime DateOpened
		{
			get { return _dateOpened; }
			set { _dateOpened = value; }
		}

		public float Balance
		{
			get { return _balance; }
			set { _balance = value; }
		}

		public float InterestRate
		{
			get { return _interestRate; }
			set { _interestRate = value; }
		}

		public AccountType AccountType
		{
			get { return _accountType; }
			set { _accountType = value; }
		}

		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		public string AccountNumber
		{
			get { return _accountNumber; }
			set { _accountNumber = value; }
		}

		public string FriendlyName
		{
			get { return String.Format( CultureInfo.CurrentCulture, "{0}[{1}] $ {2}", this.AccountType, this.AccountNumber, this.Balance); }
		}
	}
}
