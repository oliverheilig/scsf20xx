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
using System.Drawing;

namespace Microsoft.Practices.CompositeUI.WPF
{
	/// <summary>
	/// Provides information to show smartparts in the <see cref="WindowWorkspace"/>.
	/// </summary>
	public class WindowSmartPartInfo : WPFSmartPartInfo
	{
		private bool showModal = false;
		private bool controlBox = true;
		private bool maximizeButton = true;
		private bool minimizeButton = true;
		private int height = 0;
		private int width = 0;
		private Point location = default(Point);
		private Icon icon = null;

		/// <summary>
		/// Gets or sets the location of the window.
		/// </summary>
		public Point Location
		{
			get { return location; }
			set { location = value; }
		}

		/// <summary>
        /// Gets or sets the Icon that will appear on the window.
		/// </summary>
		public Icon Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		/// <summary>
        /// Gets or sets the Width of the window.
		/// </summary>
		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		/// <summary>
        /// Gets or sets the Height of the window.
		/// </summary>
		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		/// <summary>
		/// Make minimize button visible.
		/// </summary>
		public bool MinimizeBox
		{
			get { return minimizeButton; }
			set { minimizeButton = value; }
		}

		/// <summary>
		/// Make maximize button visible.
		/// </summary>
		public bool MaximizeBox
		{
			get { return maximizeButton; }
			set { maximizeButton = value; }
		}

		/// <summary>
		/// Whether the controlbox will be visible.
		/// </summary>
		public bool ControlBox
		{
			get { return controlBox; }
			set { controlBox = value; }
		}

		/// <summary>
		/// Whether the form should be shown as modal.
		/// </summary>
		public bool Modal
		{
			get { return showModal; }
			set { showModal = value; }
		}

	}
}