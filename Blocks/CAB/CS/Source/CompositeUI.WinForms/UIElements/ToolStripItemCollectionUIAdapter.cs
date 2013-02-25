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
using Microsoft.Practices.CompositeUI.Utility;

namespace Microsoft.Practices.CompositeUI.WinForms.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="ToolStripItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class ToolStripItemCollectionUIAdapter : UIElementAdapter<ToolStripItem>
	{
		private ToolStripItemCollection collection;

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItemCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="collection"></param>
		public ToolStripItemCollectionUIAdapter(ToolStripItemCollection collection)
		{
			Guard.ArgumentNotNull(collection, "collection");
			this.collection = collection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override ToolStripItem Add(ToolStripItem uiElement)
		{
			if (collection == null)
				throw new InvalidOperationException();

			collection.Insert(GetInsertingIndex(uiElement), uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(ToolStripItem uiElement)
		{
			if (uiElement.Owner != null)
				uiElement.Owner.Items.Remove(uiElement);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the collection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return collection.Count;
		}

		/// <summary>
		/// Returns the internal collection mananged by the <see cref="ToolStripItemCollectionUIAdapter"/>
		/// </summary>
		protected ToolStripItemCollection InternalCollection
		{
			get { return collection; }
			set { collection = value; }
		}
	}
}
