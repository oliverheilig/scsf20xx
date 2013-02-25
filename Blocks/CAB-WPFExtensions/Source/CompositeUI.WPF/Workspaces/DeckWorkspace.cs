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
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms.Integration;
using System.Windows;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Microsoft.Practices.CompositeUI.WPF
{
	/// <summary>
	/// Implements a workspace which shows <see cref="Control"/> layered as in a deck.
	/// </summary>
	[DesignerCategory("Code")]
	public partial class DeckWorkspace : Control, IComposableWorkspace<Control, WPFSmartPartInfo>
	{
        private ElementHostWorkspaceComposer<Control, WPFSmartPartInfo> composer;
        private bool isDisposing = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="DeckWorkspace"/> class.
		/// </summary>
		public DeckWorkspace()
		{
            composer = new ElementHostWorkspaceComposer<Control, WPFSmartPartInfo>(this);
		}

		/// <summary>
		/// Gets the controls that the deck currently contains.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ReadOnlyCollection<object> SmartParts
		{
			get
			{
				object[] controls = new object[composer.SmartParts.Count];
				composer.SmartParts.CopyTo(controls, 0);
				return new ReadOnlyCollection<object>(controls);
			}
		}

		/// <summary>
		/// Gets the currently active smart part.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ActiveSmartPart
		{
			get { return composer.ActiveSmartPart; }
		}

		/// <summary>
		/// Dependency injection setter property to get the <see cref="WorkItem"/> where the 
		/// object is contained.
		/// </summary>
		[ServiceDependency]
		public WorkItem WorkItem
		{
			set { composer.WorkItem = value; }
		}

        /// <summary>
        /// Dependency injection setter property to get the <see cref="IWPFUIElementAdapter"/>
        /// </summary>
        [ServiceDependency]
        public IWPFUIElementAdapter WPFUIElementAdapter
        {
            set { composer.WPFUIElementAdapter = value; }
        }  

        /// <summary>
        /// Overriden to control when the workspace is being disposed to disable the control activation logic.
        /// </summary>
        /// <param name="disposing">A flag that indicates if <see cref="IDisposable.Dispose"/> was called.</param>
        protected override void Dispose(bool disposing)
        {
            isDisposing = disposing;
            base.Dispose(disposing);
        }


		#region Private

        private void ControlDisposed(object sender, EventArgs e)
        {
			Control control = sender as Control;
			if (this.isDisposing == false && control != null)
            {
				composer.ForceClose(control);
            }
        }

		private void FireSmartPartActivated(object smartPart)
		{
			if (this.SmartPartActivated != null)
			{
				this.SmartPartActivated(this, new WorkspaceEventArgs(smartPart));
			}
		}

		private void ActivateTopmost()
		{
			if (this.Controls.Count != 0)
			{
				Activate(this.Controls[0]);
			}
		}

		#endregion

		#region Protected virtual implementation

		/// <summary>
		/// Activates the smart part.
		/// </summary>
		protected virtual void OnActivate(Control smartPart)
		{
			//this.Controls.SetChildIndex(smartPart, this.Controls.Count - 1);
			smartPart.BringToFront();
			smartPart.Show();
		}

		/// <summary>
		/// Applies the smart part display information to the smart part.
		/// </summary>
		protected virtual void OnApplySmartPartInfo(Control smartPart, WPFSmartPartInfo smartPartInfo)
		{
			// No op. We do not use the SPI for anything actually.
		}

		/// <summary>
		/// Closes the smart part.
		/// </summary>
		protected virtual void OnClose(Control smartPart)
		{
			this.Controls.Remove(smartPart);

			smartPart.Disposed -= ControlDisposed;

			ActivateTopmost();
		}

		/// <summary>
		/// Hides the smart part.
		/// </summary>
		protected virtual void OnHide(Control smartPart)
		{
			smartPart.SendToBack();

			ActivateTopmost();
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		protected virtual void OnShow(Control smartPart, WPFSmartPartInfo smartPartInfo)
		{
			smartPart.Dock = DockStyle.Fill;

			this.Controls.Add(smartPart);

			smartPart.Disposed += ControlDisposed;
			Activate(smartPart);
		}

		/// <summary>
		/// Raises the <see cref="SmartPartActivated"/> event.
		/// </summary>
		protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
		{
			if (SmartPartActivated != null)
			{
				SmartPartActivated(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="SmartPartClosing"/> event.
		/// </summary>
		protected void OnSmartPartClosing(WorkspaceCancelEventArgs e)
		{
			if (SmartPartClosing != null)
			{
				SmartPartClosing(this, e);
			}
		}

		/// <summary>
		/// Converts a smart part information to a compatible one for the workspace.
		/// </summary>
		protected virtual WPFSmartPartInfo OnConvertFrom(ISmartPartInfo source)
		{
			return WPFSmartPartInfo.ConvertTo<WPFSmartPartInfo>(source);
		}

		#endregion

		#region IComposableWorkspace<Control,SmartPartInfo> Members

		void IComposableWorkspace<Control, WPFSmartPartInfo>.OnActivate(Control smartPart)
		{
			OnActivate(smartPart);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, WPFSmartPartInfo smartPartInfo)
		{
			OnApplySmartPartInfo(smartPart, smartPartInfo);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.OnClose(Control smartPart)
		{
			OnClose(smartPart);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.OnHide(Control smartPart)
		{
			OnHide(smartPart);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.OnShow(Control smartPart, WPFSmartPartInfo smartPartInfo)
		{
			OnShow(smartPart, smartPartInfo);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
		{
			OnSmartPartActivated(e);
		}

		void IComposableWorkspace<Control, WPFSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
		{
			OnSmartPartClosing(e);
		}

		WPFSmartPartInfo IComposableWorkspace<Control, WPFSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
		{
			return OnConvertFrom(source);
		}

		#endregion

		#region IWorkspace Members

		/// <summary>
		/// See <see cref="IWorkspace.SmartPartClosing"/>.
		/// </summary>
		public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

		/// <summary>
		/// See <see cref="IWorkspace.SmartPartActivated"/>.
		/// </summary>
		public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

		/// <summary>
		/// See <see cref="IWorkspace.SmartParts"/>.
		/// </summary>
		ReadOnlyCollection<object> IWorkspace.SmartParts
		{
			get { return composer.SmartParts; }
		}

		/// <summary>
		/// See <see cref="IWorkspace.ActiveSmartPart"/>.
		/// </summary>
		object IWorkspace.ActiveSmartPart
		{
			get { return composer.ActiveSmartPart; }
		}

		/// <summary>
		/// Shows the smart part in a new tab with the given information.
		/// </summary>
		public void Show(object smartPart, ISmartPartInfo smartPartInfo)
		{
			composer.Show(smartPart, smartPartInfo);
		}

		/// <summary>
		/// Shows the smart part in a new tab.
		/// </summary>
		public void Show(object smartPart)
		{
			composer.Show(smartPart);
		}

        /// <summary>
		/// Hides the smart part and its tab.
		/// </summary>
		public void Hide(object smartPart)
		{
			composer.Hide(smartPart);
		}

		/// <summary>
		/// Closes the smart part and removes its tab.
		/// </summary>
		public void Close(object smartPart)
		{
			composer.Close(smartPart);
		}

		/// <summary>
		/// Activates the tab the smart part is contained in.
		/// </summary>
		/// <param name="smartPart"></param>
		public void Activate(object smartPart)
		{
			composer.Activate(smartPart);
		}

		/// <summary>
		/// Applies new layout information on the tab of the smart part.
		/// </summary>
		public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
		{
			composer.ApplySmartPartInfo(smartPart, smartPartInfo);
		}

		#endregion
	}
}