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
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.EntityTranslators;

namespace GlobalBank.BranchSystems.ServiceProxies.EntityTranslators
{
	public class CreditCardTranslator :
		EntityMapperTranslator<GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard, creditCardType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return
				targetType == typeof (GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard) &&
				sourceType == typeof (creditCardType);
		}

		protected override creditCardType BusinessToService(IEntityTranslatorService service,
		                                                    GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard
		                                                    	value)
		{
			throw new NotImplementedException();
		}

		protected override GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard ServiceToBusiness(
			IEntityTranslatorService service, creditCardType value)
		{
			GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard result =
				new GlobalBank.Infrastructure.Interface.BusinessEntities.CreditCard();
			result.AvailableBalance = (float) value.availableBalance;
			result.CardCreditLimit = (float) value.cardCreditLimit;
			result.CreditCardNumber = value.accountNumber;
			result.CreditCardTypeId = value.accountType.id;
			result.CreditCardTypeName = value.accountType.type;
			result.CustomerId = value.customerId;
			result.DateOpened = value.dateOpened;
			result.Fees = value.accountType.fees;
			result.InterestRate = value.accountType.interestRate;
			result.LastPaymentDate = value.lastPaymentDue;
			result.MaxCreditLimit = (float) value.accountType.maxCreditLimit;
			result.PaymentDue = (float) value.paymentDue;
			return result;
		}
	}
}