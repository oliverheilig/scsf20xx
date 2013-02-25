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
	public class EmailAddressTranslator : EntityMapperTranslator<Infrastructure.Interface.BusinessEntities.EmailAddress, EmailAddress>
	{
		protected override EmailAddress BusinessToService(IEntityTranslatorService service, Infrastructure.Interface.BusinessEntities.EmailAddress value)
		{
			EmailAddress result = new EmailAddress();
			result.EmailAddress1 = value.Address;
			result.Type = value.Type;
			return result;
		}

		protected override Infrastructure.Interface.BusinessEntities.EmailAddress ServiceToBusiness(IEntityTranslatorService service, EmailAddress value)
		{
			Infrastructure.Interface.BusinessEntities.EmailAddress result = new Infrastructure.Interface.BusinessEntities.EmailAddress();
			result.Address = value.EmailAddress1;
			result.Type = value.Type;
			return result;
		}
	}
}
