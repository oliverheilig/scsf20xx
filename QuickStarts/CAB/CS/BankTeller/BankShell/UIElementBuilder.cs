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
using System.Configuration;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;

namespace BankShell
{
	/// <summary>
	/// This is a temporary implementation that will be replaced with something
	/// richer when we move it into the framework.
	/// </summary>
	public static class UIElementBuilder
	{
		// Loads the menu items from App.config and put them into the menu strip, hooking
		// up the menu URIs for command dispatching.
		public static void LoadFromConfig(WorkItem workItem)
		{
			ShellItemsSection section = (ShellItemsSection)ConfigurationManager.GetSection("shellitems");

			foreach (MenuItemElement menuItem in section.MenuItems)
			{
				ToolStripMenuItem uiMenuItem = menuItem.ToMenuItem();

				workItem.UIExtensionSites[menuItem.Site].Add(uiMenuItem);

				if (menuItem.Register == true)
					workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, uiMenuItem.DropDownItems);

				if (!String.IsNullOrEmpty(menuItem.CommandName))
					workItem.Commands[menuItem.CommandName].AddInvoker(uiMenuItem, "Click");
			}
		}
	}
}
