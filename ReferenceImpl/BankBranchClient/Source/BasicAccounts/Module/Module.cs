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
using GlobalBank.BasicAccounts.ServiceProxies.EntityTranslators;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.BasicAccounts.Module
{
	public class Module : ModuleInit
	{
		private WorkItem _rootWorkItem;
		private IEntityTranslatorService _translator;

		[InjectionConstructor]
		public Module
			(
			[ServiceDependency] WorkItem rootWorkItem,
			[ServiceDependency] IEntityTranslatorService translator
			)
		{
			_rootWorkItem = rootWorkItem;
			_translator = translator;
		}

		public override void Load()
		{
			base.Load();

			ControlledWorkItem<ModuleController> workItem =
				_rootWorkItem.WorkItems.AddNew<ControlledWorkItem<ModuleController>>();
			workItem.Controller.Run();
			RegisterTranslators();
		}

		private void RegisterTranslators()
		{
			_translator.RegisterEntityTranslator(new AccountTranslator());
			_translator.RegisterEntityTranslator(new AddressTranslator());
			_translator.RegisterEntityTranslator(new CustomerTranslator());
			_translator.RegisterEntityTranslator(new EmailAddressTranslator());
			_translator.RegisterEntityTranslator(new PhoneNumberTranslator());
		}
	}
}