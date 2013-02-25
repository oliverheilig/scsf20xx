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
	public class EmailAddressTranslator : EntityMapperTranslator<EmailAddress, ServiceProxies.EmailAddress>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return base.CanTranslate(targetType, sourceType) ||
				sourceType == typeof(EmailAddress);
		}

		protected override ServiceProxies.EmailAddress BusinessToService(IEntityTranslatorService service, EmailAddress value)
		{
			ServiceProxies.EmailAddress result = new ServiceProxies.EmailAddress();
			result.EmailAddress1 = value.Address;
			result.Type = value.Type;
			return result;
		}

        protected override EmailAddress ServiceToBusiness(IEntityTranslatorService service, ServiceProxies.EmailAddress value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
	}
}
