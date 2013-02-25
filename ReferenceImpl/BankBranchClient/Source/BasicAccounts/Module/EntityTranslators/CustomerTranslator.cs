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
using Address=GlobalBank.BasicAccounts.ServiceProxies.Address;
using EmailAddress=GlobalBank.BasicAccounts.ServiceProxies.EmailAddress;
using PhoneNumber=GlobalBank.BasicAccounts.ServiceProxies.PhoneNumber;

namespace GlobalBank.BasicAccounts.Module.EntityTranslators
{
	public class CustomerTranslator : EntityMapperTranslator<Customer, ServiceProxies.Customer>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return base.CanTranslate(targetType, sourceType) ||
				sourceType == typeof(Customer);
		}

		protected override ServiceProxies.Customer BusinessToService(IEntityTranslatorService service, Customer value)
		{
			ServiceProxies.Customer result = new ServiceProxies.Customer();
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

        protected override Customer ServiceToBusiness(IEntityTranslatorService service, ServiceProxies.Customer value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
	}
}
