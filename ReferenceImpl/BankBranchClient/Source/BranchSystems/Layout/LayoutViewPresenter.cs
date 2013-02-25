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
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Constants;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace GlobalBank.BranchSystems.Layout
{
	public class LayoutViewPresenter : Presenter<LayoutView>
	{
		protected override void OnViewSet()
		{
			WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainMenu, View.MainMenuStrip);
			WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, View.MainStatusStrip);

            IRoleService roleService = WorkItem.Services.Get<IRoleService>();
            string userName = Thread.CurrentPrincipal.Identity.Name;
            View.SetUserPrincipalStatusLabel(userName, roleService.GetRolesForUser(userName));
		}

        public override void OnViewReady()
        {
            base.OnViewReady();

            IRoleService roleService = WorkItem.Services.Get<IRoleService>();
            string userName = Thread.CurrentPrincipal.Identity.Name;
            View.SetUserPrincipalStatusLabel( userName, roleService.GetRolesForUser( userName ));
        }

		/// <summary>
		/// Status update handler. Updates the status strip on the main form.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		[EventSubscription(EventTopicNames.StatusUpdate, ThreadOption.UserInterface)]
		public void StatusUpdateHandler(object sender, EventArgs<string> e)
		{
			View.SetStatusLabel(e.Data);
		}

		/// <summary>
		/// Called when the user asks to exit the application.
		/// </summary>
		public void OnFileExit()
		{
			View.ParentForm.Close();
		}
	}
}
