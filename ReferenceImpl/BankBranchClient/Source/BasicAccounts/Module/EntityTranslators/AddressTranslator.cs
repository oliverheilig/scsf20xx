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
	public class AddressTranslator : EntityMapperTranslator<Address, ServiceProxies.Address>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return base.CanTranslate(targetType, sourceType) ||
				sourceType == typeof(Address);
		}

		public override object Translate(IEntityTranslatorService service, Type targetType, object source)
		{
			return base.Translate(service, targetType, source);
		}

		protected override ServiceProxies.Address BusinessToService(IEntityTranslatorService service, Address value)
		{
			ServiceProxies.Address result = new ServiceProxies.Address();
			result.Address1 = value.Address1;
			result.Address2 = value.Address2;
			result.AddressType = value.AddressType.ToString();
			result.City = value.City;
			result.CountryCode = value.CountryCode;
			result.PostalZipCode = value.PostalZipCode;
			result.StateProvince = value.StateProvince;
			return result;
		}

        protected override Address ServiceToBusiness(IEntityTranslatorService service, ServiceProxies.Address value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

	}
}
