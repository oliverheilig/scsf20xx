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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using System.Windows.Forms;

namespace Microsoft.Practices.CompositeUI.WinForms.UIElements
{
	/// <summary>
	/// A <see cref="IUIElementAdapterFactory"/> that produces adapters for ToolStrip-related UI Elements.
	/// </summary>
	public class ToolStripUIAdapterFactory : IUIElementAdapterFactory
	{
		/// <summary>
		/// See <see cref="IUIElementAdapterFactory.GetAdapter"/> for more information.
		/// </summary>
		public IUIElementAdapter GetAdapter(object uiElement)
		{
			if (uiElement is ToolStrip)
				return new ToolStripItemCollectionUIAdapter(((ToolStrip)uiElement).Items);

			if (uiElement is ToolStripItem)
				return new ToolStripItemOwnerCollectionUIAdapter((ToolStripItem)uiElement);

			if (uiElement is ToolStripItemCollection)
				return new ToolStripItemCollectionUIAdapter((ToolStripItemCollection)uiElement);

			throw new ArgumentException("uiElement");
		}

		/// <summary>
		/// See <see cref="IUIElementAdapterFactory.Supports"/> for more information.
		/// </summary>
		public bool Supports(object uiElement)
		{
			return (uiElement is ToolStrip) || (uiElement is ToolStripItem) || (uiElement is ToolStripItemCollection);
		}
	}
}
