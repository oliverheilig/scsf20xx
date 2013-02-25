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

namespace GlobalBank.BranchSystems.ServiceProxies.EntityTranslators
{
	public class WalkinTranslator :
		EntityMapperTranslator<GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn, Walkin>
	{
		protected override Walkin BusinessToService(IEntityTranslatorService service,
		                                            GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn value)
		{
			Walkin result = new Walkin();
			result.Addresses = service.Translate<Address[]>(value.Addresses);
			result.CustomerLevel = value.CustomerLevel;
			result.EmailAddresses = service.Translate<EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			result.PhoneNumbers = service.Translate<PhoneNumber[]>(value.PhoneNumbers);
			result.SSNumber = value.SocialSecurityNumber;
			return result;
		}

		protected override GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn ServiceToBusiness(
			IEntityTranslatorService service, Walkin value)
		{
			GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn result =
				new GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn();
			result.Addresses = service.Translate<Infrastructure.Interface.BusinessEntities.Address[]>(value.Addresses);
			result.CustomerLevel = value.CustomerLevel;
			result.EmailAddresses =
				service.Translate<Infrastructure.Interface.BusinessEntities.EmailAddress[]>(value.EmailAddresses);
			result.FirstName = value.FirstName;
			result.LastName = value.LastName;
			result.MiddleInitial = value.MiddleInitial;
			result.MotherMaidenName = value.MotherMaidenName;
			result.PhoneNumbers = service.Translate<Infrastructure.Interface.BusinessEntities.PhoneNumber[]>(value.PhoneNumbers);
			result.SocialSecurityNumber = value.SSNumber;
			return result;
		}
	}
}