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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.UIElements;

namespace GlobalBank.Infrastructure.UI
{
	public class LinkLabelPanelUIAdapter : UIElementAdapter<LinkLabel>
	{
		private LinkLabelPanel _panel;

		public LinkLabelPanelUIAdapter(LinkLabelPanel panel)
		{
			_panel = panel;
		}

		protected override LinkLabel Add(LinkLabel uiElement)
		{
			_panel.Add(uiElement);
			return uiElement;
		}

		protected override void Remove(LinkLabel uiElement)
		{
			_panel.Remove(uiElement);
		}
	}
}
