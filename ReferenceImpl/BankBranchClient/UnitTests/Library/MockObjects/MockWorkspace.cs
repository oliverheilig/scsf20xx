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
using Microsoft.Practices.CompositeUI.SmartParts;

namespace GlobalBank.UnitTest.Library.MockObjects
{
	public class MockWorkspace : IWorkspace
	{
		List<object> _smartParts = new List<object>();
		public object ClosedSmartPart = null;

		public void Activate(object smartPart)
		{
		}

		public object ActiveSmartPart
		{
			get
			{
				return null;
			}
		}

		public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
		{
		}

		public void Close(object smartPart)
		{
			_smartParts.Remove(smartPart);
			ClosedSmartPart = smartPart;
		}

		public void Hide(object smartPart)
		{
		}

		public void Show(object smartPart)
		{
			_smartParts.Add(smartPart);
		}

		public void Show(object smartPart, ISmartPartInfo smartPartInfo)
		{
			_smartParts.Add(smartPart);
		}

		public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

		public void FireSmartPartActivated(object smartPart)
		{
			if ( SmartPartActivated!=null)
			{
				SmartPartActivated(this, new WorkspaceEventArgs(smartPart));
			}
		}
		
		public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

		public void FireSmartPartClosing(object smartPart)
		{
			if (SmartPartClosing != null)
			{
				SmartPartClosing(this, new WorkspaceCancelEventArgs(smartPart));
			}
		}
		
		public ReadOnlyCollection<object> SmartParts
		{
			get { return new ReadOnlyCollection<object>(_smartParts); }
		}
	}
}
