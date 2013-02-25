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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace GlobalBank.Infrastructure.UI
{
	public partial class IconTabWorkspace : UserControl, IWorkspace
	{
		private static readonly IconSmartPartInfo nullSmartPartInfo = new IconSmartPartInfo(
			Properties.Resources.NullSmartPartInfo,
			Properties.Resources.NullSmartPartInfo,
			Properties.Resources.NullSmartPartInfoIcon);

		Dictionary<object, ToolStripButton> _smartParts;

		public IconTabWorkspace()
		{
			_smartParts = new Dictionary<object, ToolStripButton>();
			InitializeComponent();
		}

		void _deckWorkspace_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
		{
			if (_smartParts.ContainsKey(e.SmartPart))
			{
				_toolStrip.Items.Remove(_smartParts[e.SmartPart]);
			}
		}

		public void Activate(object smartPart)
		{
			_deckWorkspace.Activate(smartPart);
			OnSmartPartActivated(smartPart);
		}

		public object ActiveSmartPart
		{
			get { return _deckWorkspace.ActiveSmartPart; }
		}

		public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
		{
			_deckWorkspace.ApplySmartPartInfo(smartPart, smartPartInfo);
		}

		public void Close(object smartPart)
		{
			if (_smartParts.ContainsKey(smartPart))
			{
				_toolStrip.Items.Remove(_smartParts[smartPart]);
			}
			_deckWorkspace.Close(smartPart);
			OnSmartPartActivated(_deckWorkspace.ActiveSmartPart);
		}

		public void Hide(object smartPart)
		{
			_deckWorkspace.Hide(smartPart);
		}

		public void Show(object smartPart)
		{
			ISmartPartInfoProvider provider = smartPart as ISmartPartInfoProvider;
			if (provider != null)
			{
				ISmartPartInfo info = provider.GetSmartPartInfo(typeof(IconSmartPartInfo));
				if (info != null)
				{
					Show(smartPart, info);
				}
			}
			else
			{
				_deckWorkspace.Show(smartPart);
			}
			OnSmartPartActivated(smartPart);
		}

		public void Show(object smartPart, ISmartPartInfo smartPartInfo)
		{
			Control ctrl = smartPart as Control;
			if (ctrl != null && _deckWorkspace.SmartParts.Contains(ctrl) == false)
			{
				IconSmartPartInfo info = smartPartInfo as IconSmartPartInfo;
				if (info == null)
				{
					info = nullSmartPartInfo;
				}
				ToolStripButton item = new ToolStripButton(info.Title, info.Icon.ToBitmap());
				item.ToolTipText = info.Description;
				item.Tag = smartPart;
				_toolStrip.Items.Add(item);
				_smartParts.Add(smartPart, item);
			}
			_deckWorkspace.Show(smartPart, smartPartInfo);
			OnSmartPartActivated(smartPart);
		}

		public event EventHandler<WorkspaceEventArgs> SmartPartActivated
		{
			add
			{
				_deckWorkspace.SmartPartActivated += value;
			}
			remove
			{
				_deckWorkspace.SmartPartActivated -= value;
			}
		}

		public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing
		{
			add
			{
				_deckWorkspace.SmartPartClosing += value;
			}
			remove
			{
				_deckWorkspace.SmartPartClosing -= value;
			}
		}

		public ReadOnlyCollection<object> SmartParts
		{
			get
			{
				List<object> result = new List<object>();
				foreach (Control ctrl in _deckWorkspace.SmartParts) result.Add(ctrl);
				return result.AsReadOnly();
			}
		}

		private void _toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			_deckWorkspace.Show(e.ClickedItem.Tag);
			OnSmartPartActivated(e.ClickedItem.Tag);
		}

		private void OnSmartPartActivated(object smartPart)
		{
			foreach (KeyValuePair<object, ToolStripButton> pair in _smartParts)
			{
				pair.Value.Checked = pair.Key == smartPart;
			}
		}
	}
}
