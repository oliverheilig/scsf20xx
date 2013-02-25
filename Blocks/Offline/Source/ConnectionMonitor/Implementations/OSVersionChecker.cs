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
using System.Runtime.InteropServices;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// Helper class to verify the OS version information
	/// </summary>
	public static class OSVersionChecker
	{
		private static bool VerifyWindowsVesion(int major, int minor, UInt16 spMajor, UInt16 spMinor)
		{
			NativeMethods.OSVERSIONINFOEX osvi = new NativeMethods.OSVERSIONINFOEX();
			osvi.OSVersionInfoSize = Marshal.SizeOf(osvi);

			osvi.MajorVersion = major;
			osvi.MinorVersion = minor;
			osvi.ServicePackMajor = spMajor;
			osvi.ServicePackMinor = spMinor;

			int typeMask = NativeMethods.VER_MAJORVERSION | NativeMethods.VER_MINORVERSION | NativeMethods.VER_SERVICEPACKMAJOR |
			               NativeMethods.VER_SERVICEPACKMINOR;

			ulong conditionMask = 0;
			conditionMask =
				NativeMethods.VerSetConditionMask(conditionMask, NativeMethods.VER_MAJORVERSION, NativeMethods.VER_GREATER_EQUAL);
			conditionMask =
				NativeMethods.VerSetConditionMask(conditionMask, NativeMethods.VER_MINORVERSION, NativeMethods.VER_GREATER_EQUAL);
			conditionMask =
				NativeMethods.VerSetConditionMask(conditionMask, NativeMethods.VER_SERVICEPACKMAJOR, NativeMethods.VER_GREATER_EQUAL);
			conditionMask =
				NativeMethods.VerSetConditionMask(conditionMask, NativeMethods.VER_SERVICEPACKMINOR, NativeMethods.VER_GREATER_EQUAL);

			bool result = NativeMethods.VerifyVersionInfo(ref osvi, typeMask, conditionMask);
			return result;
		}

		/// <summary>
		/// Returns <see langword="true"/> if the operating system is Windows XP with SP2 or later.
		/// </summary>
		public static bool IsWindowsXPSP2()
		{
			return VerifyWindowsVesion(5, 1, 2, 0);
		}

		/// <summary>
		/// Returns <see langword="true"/> is the operating system is Windows Vista or later.
		/// </summary>
		/// <returns></returns>
		public static bool IsWindowsVista()
		{
			return VerifyWindowsVesion(6, 0, 0, 0);
		}
	}
}