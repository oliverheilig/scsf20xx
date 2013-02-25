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
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;

namespace Microsoft.Practices.CompositeUI.WPF
{
	/// <summary>
	/// Implements a Workspace which shows the smarparts in MDI forms.
	/// </summary>
    /// <remarks>
    /// It includes support for <see cref="System.Windows.UIElement"/> objects.
    /// </remarks>
	public class MdiWorkspace : WindowWorkspace
	{
		private Form parentMdiForm;

		/// <summary>
		/// Constructor specifying the parent form of the MDI child.
		/// </summary>
		public MdiWorkspace(Form parentForm)
			: base()
		{
			this.parentMdiForm = parentForm;
			this.parentMdiForm.IsMdiContainer = true;
		}

		/// <summary>
		/// Gets the parent MDI form.
		/// </summary>
		public Form ParentMdiForm
		{
			get { return parentMdiForm; }
		}

		/// <summary>
		/// Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
		/// </summary>
		/// <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
		/// <param name="smartPartInfo">The information to use to show the smart part.</param>
		protected override void OnShow(Control smartPart, WindowSmartPartInfo smartPartInfo)
		{
			Form mdiChild = this.GetOrCreateForm(smartPart);
			mdiChild.MdiParent = parentMdiForm;

			this.SetWindowProperties(mdiChild, smartPartInfo);
			mdiChild.Show();
			this.SetWindowLocation(mdiChild, smartPartInfo);
			mdiChild.BringToFront();
		}
	}
}