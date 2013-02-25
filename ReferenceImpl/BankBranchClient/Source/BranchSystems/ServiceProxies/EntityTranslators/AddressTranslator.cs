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
	public class AddressTranslator : EntityMapperTranslator<Infrastructure.Interface.BusinessEntities.Address, Address>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return base.CanTranslate(targetType, sourceType) ||
			       sourceType == typeof (Infrastructure.Interface.BusinessEntities.Address) &&
			       targetType == typeof (AddressSearchCriteria);
		}

		public override object Translate(IEntityTranslatorService service, Type targetType, object source)
		{
			if (targetType == typeof (AddressSearchCriteria))
				return CreateSearchCriteria(service, (Infrastructure.Interface.BusinessEntities.Address) source);
			return base.Translate(service, targetType, source);
		}

		protected override Address BusinessToService(IEntityTranslatorService service,
		                                             Infrastructure.Interface.BusinessEntities.Address value)
		{
			Address result = new Address();
			result.Address1 = value.Address1;
			result.Address2 = value.Address2;
			result.AddressType = value.AddressType.ToString();
			result.City = value.City;
			result.CountryCode = value.CountryCode;
			result.PostalZipCode = value.PostalZipCode;
			result.StateProvince = value.StateProvince;
			return result;
		}

		protected override Infrastructure.Interface.BusinessEntities.Address ServiceToBusiness(
			IEntityTranslatorService service, Address value)
		{
			Infrastructure.Interface.BusinessEntities.Address result = new Infrastructure.Interface.BusinessEntities.Address();
			result.Address1 = value.Address1;
			result.Address2 = value.Address2;
			result.AddressType =
				(Infrastructure.Interface.BusinessEntities.AddressType)
				Enum.Parse(typeof (Infrastructure.Interface.BusinessEntities.AddressType), value.AddressType);
			result.City = value.City;
			result.CountryCode = value.CountryCode;
			result.PostalZipCode = value.PostalZipCode;
			result.StateProvince = value.StateProvince;
			return result;
		}

		private AddressSearchCriteria CreateSearchCriteria(IEntityTranslatorService service,
		                                                   Infrastructure.Interface.BusinessEntities.Address value)
		{
			AddressSearchCriteria result = new AddressSearchCriteria();
			result.Address1 = value.Address1;
			result.Address2 = value.Address2;
			result.City = value.City;
			result.CountryCode = value.CountryCode;
			result.PostalZipCode = value.PostalZipCode;
			result.StateProvince = value.StateProvince;
			return result;
		}
	}
}