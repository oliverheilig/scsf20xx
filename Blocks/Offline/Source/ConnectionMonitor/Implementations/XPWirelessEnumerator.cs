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
using System.Runtime.InteropServices;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// Implements the <see cref="IWifiAdapterEnumerator"/> using the WZCSAPI
	/// available in Windows XP/SP2
	/// </summary>
	public class XPWirelessEnumerator : IWifiAdapterEnumerator
	{
		/// <summary>
		/// Enumerates an returns a list of available wireless adapters on the system.
		/// </summary>
		/// <returns>A list os srtings with the GUID ids of the wireless adapters.</returns>
		public List<string> EnumerateWirelessAdapters()
		{
			NativeMethods.INTFS_KEY_TABLE pIntfs = new NativeMethods.INTFS_KEY_TABLE();
			int result = NativeMethods.WZCEnumInterfaces(IntPtr.Zero, out pIntfs);

			List<string> adapters = new List<string>();

			if (result == 0 && pIntfs.dwNumIntfs > 0)
			{
				foreach (uint pValue in pIntfs.pIntfs)
				{
					IntPtr pPointer = new IntPtr(pValue);
					NativeMethods.INTF_KEY_ENTRY pIntfsEntry =
						(NativeMethods.INTF_KEY_ENTRY) Marshal.PtrToStructure(pPointer, typeof (NativeMethods.INTF_KEY_ENTRY));
					adapters.Add(pIntfsEntry.wszGuid);
				}
			}
			return adapters;
		}
	}
}