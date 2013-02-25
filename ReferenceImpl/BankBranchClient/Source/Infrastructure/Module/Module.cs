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
using GlobalBank.Infrastructure.Library.ActionConditions;
using GlobalBank.Infrastructure.Module.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.Infrastructure.Module
{
	public class Module : ModuleInit
	{
		private WorkItem _rootWorkItem;

		[InjectionConstructor]
		public Module
			(
			[ServiceDependency] WorkItem rootWorkItem
			)
		{
			_rootWorkItem = rootWorkItem;
		}

		public override void AddServices()
		{
			base.AddServices();
			_rootWorkItem.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();
			_rootWorkItem.Services.AddNew<EnterpriseLibraryCacheService, ICacheService>();
			IActionCatalogService catalog = _rootWorkItem.Services.Get<IActionCatalogService>();
			catalog.RegisterGeneralCondition(new EnterpriseLibraryAuthorizationActionCondition());
		}
	}
}