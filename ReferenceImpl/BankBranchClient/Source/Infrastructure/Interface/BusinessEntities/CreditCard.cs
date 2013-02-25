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
	public class CreditCard
	{
		private DateTime _lastPaymentDate;
		private DateTime _dateOpened;
		private float _paymentDue;
		private float _availableBalance;
		private float _cardCreditLimit;
		private string _customerId;
		private string _creditCardNumber;
		private float _interestRate;
		private float _fees;
		private float _maxCreditLimit;
		private string _creditCardTypeName;
		private int _creditCardTypeId;

		public float InterestRate
		{
			get { return _interestRate; }
			set { _interestRate = value; }
		}

		public float Fees
		{
			get { return _fees; }
			set { _fees = value; }
		}

		public float MaxCreditLimit
		{
			get { return _maxCreditLimit; }
			set { _maxCreditLimit = value; }
		}
		public string CreditCardTypeName
		{
			get { return _creditCardTypeName; }
			set { _creditCardTypeName = value; }
		}

		public int CreditCardTypeId
		{
			get { return _creditCardTypeId; }
			set { _creditCardTypeId = value; }
		}

		public DateTime LastPaymentDate
		{
			get { return _lastPaymentDate; }
			set { _lastPaymentDate = value; }
		}

		public DateTime DateOpened
		{
			get { return _dateOpened; }
			set { _dateOpened = value; }
		}

		public float PaymentDue
		{
			get { return _paymentDue; }
			set { _paymentDue = value; }
		}

		public float AvailableBalance
		{
			get { return _availableBalance; }
			set { _availableBalance = value; }
		}

		public float CardCreditLimit
		{
			get { return _cardCreditLimit; }
			set { _cardCreditLimit = value; }
		}

		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		public string CreditCardNumber
		{
			get { return _creditCardNumber; }
			set { _creditCardNumber = value; }
		}
	}
}
