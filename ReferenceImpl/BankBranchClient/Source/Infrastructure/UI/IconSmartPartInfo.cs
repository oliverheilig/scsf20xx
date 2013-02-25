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
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace GlobalBank.Infrastructure.UI
{
	public class IconSmartPartInfo : SmartPartInfo
	{
		private Icon _icon;

		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		public IconSmartPartInfo(string title, string description, Icon icon)
		{
			this.Title = title;
			this.Description = description;
			_icon = icon;
		}
	}
}
