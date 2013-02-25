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
	public class AlertTranslator : EntityMapperTranslator<Infrastructure.Interface.BusinessEntities.Alert, Alert>
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return targetType == typeof (Infrastructure.Interface.BusinessEntities.Alert) && sourceType == typeof (Alert);
		}

		protected override Alert BusinessToService(IEntityTranslatorService service,
		                                           Infrastructure.Interface.BusinessEntities.Alert value)
		{
			throw new NotImplementedException();
		}

		protected override Infrastructure.Interface.BusinessEntities.Alert ServiceToBusiness(IEntityTranslatorService service,
		                                                                                     Alert value)
		{
			Infrastructure.Interface.BusinessEntities.Alert result = new Infrastructure.Interface.BusinessEntities.Alert();
			result.AlertId = value.AlertId;
			result.AlertType = value.AlertTypeReference.Type;
			result.CustomerId = value.CustomerId;
			result.ExpirationDate = value.ExpirationDate;
			result.StartDate = value.StartDate;
			return result;
		}
	}
}