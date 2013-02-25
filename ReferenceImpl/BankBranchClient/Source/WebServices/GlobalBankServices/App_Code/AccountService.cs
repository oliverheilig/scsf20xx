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
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace GlobalBankServices
{
	public class AccountService : IBasicHttpBinding_IAccountService
	{
		public void PurchaseCertificateOfDeposit(PurchaseCertidicateOfDepositRequest request)
		{
			try
			{
				new GlobalBankDataServices().PurchaseCertificateOfDeposit(
					request.Customer.CustomerId, 
					request.SourceAccount.accountNumber, 
					(decimal)request.Amount, 
					request.DurationInDays, 
					(decimal)request.InterestRate);
			}
			catch (Exception ex)
			{
				throw SoapExceptionFactory.CreateSoapClientException(ex);
			}
		}
	}

	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/05/PurchaseCertidicateOfDepositRequest")]
	public class PurchaseCertidicateOfDepositRequest
	{
		private Customer _customer;
		private accountType _sourceAccount;
		private float _amount;
		private float _interestRate;
		private int _durationInDays;

		public int DurationInDays
		{
			get { return _durationInDays; }
			set { _durationInDays = value; }
		}
	
		public float InterestRate
		{
			get { return _interestRate; }
			set { _interestRate = value; }
		}
	
		public float Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

		[XmlElement(IsNullable = true)]
		public accountType SourceAccount
		{
			get { return _sourceAccount; }
			set { _sourceAccount = value; }
		}

		[XmlElement(IsNullable = true)]
		public Customer Customer
		{
			get { return _customer; }
			set { _customer = value; }
		}
	}
}
