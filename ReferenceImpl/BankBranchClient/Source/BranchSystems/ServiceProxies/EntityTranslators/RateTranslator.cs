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
	public class RateTranslator :
		EntityMapperTranslator<GlobalBank.Infrastructure.Interface.BusinessEntities.Rate, RateTableEntryType>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return
				targetType == typeof (GlobalBank.Infrastructure.Interface.BusinessEntities.Rate) &&
				sourceType == typeof (RateTableEntryType);
		}

		protected override RateTableEntryType BusinessToService(IEntityTranslatorService service,
		                                                        GlobalBank.Infrastructure.Interface.BusinessEntities.Rate
		                                                        	value)
		{
			throw new NotImplementedException();
		}

		protected override GlobalBank.Infrastructure.Interface.BusinessEntities.Rate ServiceToBusiness(
			IEntityTranslatorService service, RateTableEntryType value)
		{
			GlobalBank.Infrastructure.Interface.BusinessEntities.Rate result =
				new GlobalBank.Infrastructure.Interface.BusinessEntities.Rate();
			result.InterestRate = value.rate;
			result.MinimumAmount = value.minimumAmount;
			result.MaximumAmount = value.maximumAmount;
			result.Start = value.start;
			result.End = value.end;
			return result;
		}
	}
}