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
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.EntityTranslators;

namespace GlobalBank.BasicAccounts.ServiceProxies.EntityTranslators
{
	public class CustomerTranslator : EntityMapperTranslator<Infrastructure.Interface.BusinessEntities.Customer, Customer>
	{
		protected override Customer BusinessToService(IEntityTranslatorService service, Infrastructure.Interface.BusinessEntities.Customer value)
		{
			Customer result = new Customer();
			if (value.Addresses != null) result.Addresses = service.Translate<Address[]>(value.Addresses);
			result.CustomerId = value.CustomerId;
			result.CustomerLevel = value.CustomerLevel;
			if (value.EmailAddresses != null) result.EmailAddresses = service.Translate<EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			if (value.PhoneNumbers != null) result.PhoneNumbers = service.Translate<PhoneNumber[]>(value.PhoneNumbers);
			result.SSNumber = value.SocialSecurityNumber;
			return result;
		}

		protected override Infrastructure.Interface.BusinessEntities.Customer ServiceToBusiness(IEntityTranslatorService service, Customer value)
		{
			Infrastructure.Interface.BusinessEntities.Customer result = new Infrastructure.Interface.BusinessEntities.Customer();
			if (value.Addresses != null) result.Addresses = service.Translate<Infrastructure.Interface.BusinessEntities.Address[]>(value.Addresses);
			result.CustomerId = value.CustomerId;
			result.CustomerLevel = value.CustomerLevel;
			if (value.EmailAddresses != null) result.EmailAddresses = service.Translate<Infrastructure.Interface.BusinessEntities.EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			if (value.PhoneNumbers != null) result.PhoneNumbers = service.Translate<Infrastructure.Interface.BusinessEntities.PhoneNumber[]>(value.PhoneNumbers);
			result.SocialSecurityNumber = value.SSNumber;
			return result;
		}
	}
}
