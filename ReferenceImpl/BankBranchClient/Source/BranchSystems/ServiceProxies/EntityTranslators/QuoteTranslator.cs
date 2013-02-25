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
	public class QuoteTranslator :
		EntityMapperTranslator<GlobalBank.Infrastructure.Interface.BusinessEntities.Quote, QuoteResponseType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return
				targetType == typeof (GlobalBank.Infrastructure.Interface.BusinessEntities.Quote) &&
				sourceType == typeof (QuoteResponseType);
		}

		protected override QuoteResponseType BusinessToService(IEntityTranslatorService service,
		                                                       GlobalBank.Infrastructure.Interface.BusinessEntities.Quote
		                                                       	value)
		{
			throw new NotImplementedException();
		}

		protected override GlobalBank.Infrastructure.Interface.BusinessEntities.Quote ServiceToBusiness(
			IEntityTranslatorService service, QuoteResponseType value)
		{
			GlobalBank.Infrastructure.Interface.BusinessEntities.Quote quote =
				new GlobalBank.Infrastructure.Interface.BusinessEntities.Quote();
			quote.Amount = value.amount;
			quote.Duration = value.durationInMonths;
			quote.Rate = value.rate;
			return quote;
		}
	}
}