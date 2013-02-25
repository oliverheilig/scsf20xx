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
using System.Threading;
using System.Windows.Forms;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.BuilderStrategies;
using GlobalBank.Infrastructure.Library.Services;
using GlobalBank.Infrastructure.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.SmartClient.Library;

namespace GlobalBank.Infrastructure.Library
{
	public abstract class SmartClientApplication<TWorkItem, TShell> : FormShellApplication<TWorkItem, TShell>
		where TWorkItem : WorkItem, new()
		where TShell : Form
	{
		protected override void AddBuilderStrategies(Builder builder)
		{
			base.AddBuilderStrategies(builder);
			builder.Strategies.AddNew<ActionStrategy>(BuilderStage.Initialization);
		}

		protected override void AddServices()
		{
			base.AddServices();

			// Authentication services
			RootWorkItem.Services.AddNew<UserSelectorService, IUserSelectorService>();
			RootWorkItem.Services.Remove<IAuthenticationService>();
			RootWorkItem.Services.AddNew<SimpleWinFormAuthenticationService, IAuthenticationService>();
			RootWorkItem.Services.AddNew<SimpleRoleService, IRoleService>();

			// Profile catalog services
			RootWorkItem.Services.AddNew<ProfileCatalogService, IProfileCatalogService>();
			//Add the InfoStore service we want to use
			//RootWorkItem.Services.AddNew<ProfileCatalogModuleInfoStore, IModuleInfoStore>();
			RootWorkItem.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();
			RootWorkItem.Services.Remove<IModuleEnumerator>();
			RootWorkItem.Services.Remove<IModuleLoaderService>();
			RootWorkItem.Services.AddNew<XmlStreamDependentModuleEnumerator, IModuleEnumerator>();
			RootWorkItem.Services.AddNew<DependentModuleLoaderService, IModuleLoaderService>();

			RootWorkItem.Services.AddNew<ActionCatalogService, IActionCatalogService>();
			IUIElementAdapterFactoryCatalog catalog = RootWorkItem.Services.Get<IUIElementAdapterFactoryCatalog>();
			catalog.RegisterFactory(new LinkLabelPanelUIAdapterFactory());
			RootWorkItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			RootWorkItem.Services.AddNew<EntityTranslatorService, IEntityTranslatorService>();
		}

		protected override void BeforeShellCreated()
		{
			base.BeforeShellCreated();
			InitializeWebServiceCatalogModuleInfoStore();
		}

		private void InitializeWebServiceCatalogModuleInfoStore()
		{
			IRoleService roleService = RootWorkItem.Services.Get<IRoleService>();
			WebServiceCatalogModuleInfoStore store =
				RootWorkItem.Services.Get<IModuleInfoStore>() as WebServiceCatalogModuleInfoStore;
			store.Roles = roleService.GetRolesForUser(Thread.CurrentPrincipal.Identity.Name);
		}

		protected override void AfterShellCreated()
		{
			base.AfterShellCreated();

			ShellNotificationService notificationService = new ShellNotificationService(this.Shell);
			RootWorkItem.Services.Add(typeof (IMessageBoxService), notificationService);
		}
	}
}