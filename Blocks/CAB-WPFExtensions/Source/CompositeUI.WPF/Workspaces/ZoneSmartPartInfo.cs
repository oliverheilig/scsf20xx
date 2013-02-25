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
using System.Drawing;

namespace Microsoft.Practices.CompositeUI.WPF
{
	/// <summary>
	/// Provides infomation to show smartparts in the <see cref="ZoneWorkspace"/>.
	/// </summary>
	public class ZoneSmartPartInfo : WPFSmartPartInfo
	{
		private string zoneName;
		private DockStyle? dock = null;

		/// <summary>
		/// Initializes the ZoneSmartPartInfo with no zone name.
		/// </summary>
		public ZoneSmartPartInfo()
		{
		}

		/// <summary>
		/// Creates the information for the smart part specifying the name of the zone.
		/// </summary>
		/// <param name="zoneName">Name of the zone assigned to a smart part.</param>
		public ZoneSmartPartInfo(string zoneName)
		{
			this.zoneName = zoneName;
		}

		/// <summary>
		/// Creates the information for the smart part specifying its title and the name of the zone.
		/// </summary>
		/// <param name="zoneName">Name of the zone assigned to a smart part.</param>
		/// <param name="title">Title of the smart part.</param>
		public ZoneSmartPartInfo(string title, string zoneName)
		{
			base.Title = title;
			this.zoneName = zoneName;
		}

		/// <summary>
		/// Name of the zone where the smart part should be shown.
		/// </summary>
		/// <remarks>
		/// If a zone with the given name does not exist in the <see cref="ZoneWorkspace"/> 
		/// where the smart part is being shown, an exception will be thrown.
		/// </remarks>
		public string ZoneName
		{
			get { return zoneName; }
			set { zoneName = value; }
		}

		/// <summary>
		/// Sets the dockstyle of the control to show in the zone.
		/// </summary>
		public DockStyle? Dock
		{
			get { return dock; }
			set { dock = value; }
		}
	}
}