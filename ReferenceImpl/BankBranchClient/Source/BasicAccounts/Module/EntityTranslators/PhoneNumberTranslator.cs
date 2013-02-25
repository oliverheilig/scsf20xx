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
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;

namespace GlobalBank.BasicAccounts.Module.EntityTranslators
{
	public class PhoneNumberTranslator : EntityMapperTranslator<PhoneNumber, ServiceProxies.PhoneNumber>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return base.CanTranslate(targetType, sourceType) ||
				sourceType == typeof(PhoneNumber);
		}

		protected override ServiceProxies.PhoneNumber BusinessToService(IEntityTranslatorService service, PhoneNumber value)
		{
			ServiceProxies.PhoneNumber result = new ServiceProxies.PhoneNumber();
			result.PhoneNumber1 = value.Number;
			result.PhoneType = value.PhoneType.ToString();
			return result;
		}

		protected override PhoneNumber ServiceToBusiness(IEntityTranslatorService service, ServiceProxies.PhoneNumber value)
		{
			PhoneNumber result = new PhoneNumber();
			result.Number = value.PhoneNumber1;
			result.PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), value.PhoneType);
			return result;
		}
		
	}

}
