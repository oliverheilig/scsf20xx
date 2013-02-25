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
	internal static class NativeMethods
	{
		internal const int ERROR_SERVICE_NOT_ACTIVE = 1062;
		internal const int VER_PLATFORM_WIN32s = 0;
		internal const int VER_PLATFORM_WIN32_WINDOWS = 1;
		internal const int VER_PLATFORM_WIN32_NT = 2;
		internal const int VER_PLATFORM_WIN32_HH = 3;
		internal const int VER_PLATFORM_WIN32_CE = 3;

		internal const int VER_BUILDNUMBER = 0x0000004; // dwBuildNumber 
		internal const int VER_MAJORVERSION = 0x0000002; // dwMajorVersion
		// If you are testing the major version, you must also test the minor version and the service pack major and minor versions.
		internal const int VER_MINORVERSION = 0x0000001; // dwMinorVersion 
		internal const int VER_PLATFORMID = 0x0000008; // dwPlatformId 
		internal const int VER_SERVICEPACKMAJOR = 0x0000020; // wServicePackMajor 
		internal const int VER_SERVICEPACKMINOR = 0x0000010; // wServicePackMinor 
		internal const int VER_SUITENAME = 0x0000040; // wSuiteMask 
		internal const int VER_PRODUCT_TYPE = 0x0000080; // dwProductType

		internal const int VER_EQUAL = 1; // The current value must be equal to the specified value. 
		internal const int VER_GREATER = 2; // The current value must be greater than the specified value. 
		internal const int VER_GREATER_EQUAL = 3; // The current value must be greater than or equal to the specified value. 
		internal const int VER_LESS = 4; // The current value must be less than the specified value. 
		internal const int VER_LESS_EQUAL = 5; // The current value must be less than or equal to the specified value. 

		internal const int VER_AND = 6;
		                   // All product suites specified in the wSuiteMask member must be present in the current system. 

		internal const int VER_OR = 7; // At least one of the specified product suites must be present in the current system. 

		[StructLayout(LayoutKind.Sequential)]
		internal struct OSVERSIONINFOEX
		{
			public int OSVersionInfoSize;
			public int MajorVersion;
			public int MinorVersion;
			public int BuildNumber;
			public int PlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string CSDVersion;
			public UInt16 ServicePackMajor;
			public UInt16 ServicePackMinor;
			public UInt16 SuiteMask;
			public byte ProductType;
			public byte Reserved;
		}

		//		[DllImport("kernel32")]
		//		static extern bool GetVersionEx(ref OSVERSIONINFOEX versionInfo);

		[DllImport("kernel32")]
		[return : MarshalAs(UnmanagedType.Bool)]
		internal static extern bool VerifyVersionInfo(ref OSVERSIONINFOEX versionInfo,
		                                              int typeMask,
		                                              ulong conditionMask);

		[DllImport("kernel32")]
		internal static extern ulong VerSetConditionMask(ulong conditionMask, int typeBitMask, int operatorMask);

		#region Native WiFi API

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WLAN_INTERFACE_INFO_LIST
		{
			public WLAN_INTERFACE_INFO_LIST(IntPtr pList)
			{
				NumberOfItems = Marshal.ReadInt32(pList, 0);
				Index = Marshal.ReadInt32(pList, 4);
				InterfaceInfo = new WLAN_INTERFACE_INFO[NumberOfItems];

				for (int i = 0; i < NumberOfItems; i++)
				{
					IntPtr pItemList = new IntPtr(pList.ToInt32() + (i*284));
					WLAN_INTERFACE_INFO wii = new WLAN_INTERFACE_INFO();

					byte[] intGuid = new byte[16];
					for (int j = 0; j < 16; j++)
					{
						intGuid[j] = Marshal.ReadByte(pItemList, 8 + j);
					}
					wii.InterfaceGuid = new Guid(intGuid);
					//wii.InterfacePtr = new IntPtr(pItemList.ToInt32() + 8);
					wii.InterfaceDescription =
						Marshal.PtrToStringUni(new IntPtr(pItemList.ToInt32() + 24), 256).Replace("\0", "");
					wii.State = (WLAN_INTERFACE_STATE) Marshal.ReadInt32(pItemList, 280);

					InterfaceInfo[i] = wii;
				}
			}


			public int NumberOfItems; // Contains the number of items in the InterfaceInfo member.
			public int Index; // The index of the current item. The index of the first item is 0. 

			[MarshalAs(UnmanagedType.ByValArray)] public WLAN_INTERFACE_INFO[] InterfaceInfo;
			                                                                   // Pointer to an array of WLAN_INTERFACE_INFO structures containing interface information. 
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WLAN_INTERFACE_INFO
		{
			public Guid InterfaceGuid;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string InterfaceDescription;
			public WLAN_INTERFACE_STATE State;
		}

		internal enum WLAN_INTERFACE_STATE
		{
			wlan_interface_state_not_ready = 0,
			wlan_interface_state_connected = 1,
			wlan_interface_state_ad_hoc_network_formed = 2,
			wlan_interface_state_disconnecting = 3,
			wlan_interface_state_disconnected = 4,
			wlan_interface_state_associating = 5,
			wlan_interface_state_discovering = 6,
			wlan_interface_state_authenticating = 7
		}

		[DllImport("wlanapi", SetLastError = true)]
		internal static extern uint WlanOpenHandle(
			[In] uint dwClientVersion,
			[In, Out] IntPtr pReserved,
			[Out] out uint pdwNegotiatedVersion,
			[Out] out uint phClientHandle);

		[DllImport("wlanapi", SetLastError = true)]
		internal static extern uint WlanCloseHandle(
			[In] uint hClientHandle,
			[In] IntPtr pReserved);

		[DllImport("wlanapi", SetLastError = true)]
		internal static extern uint WlanEnumInterfaces(
			[In] uint hClientHandle,
			[In] IntPtr pReserved,
			[Out] out IntPtr ppInterfaceList);

		[DllImport("wlanapi", SetLastError = true)]
		internal static extern void WlanFreeMemory([In] IntPtr pMemory);

		#endregion

		#region Windows Zero Configuration API

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTFS_KEY_TABLE
		{
			public uint dwNumIntfs;
			[MarshalAs(UnmanagedType.ByValArray)] public uint[] pIntfs;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTF_KEY_ENTRY
		{
			public string wszGuid;
		}

		[DllImport("wzcsapi.dll", SetLastError = true)]
		internal static extern int WZCEnumInterfaces(
			[In] IntPtr pSvrAddr,
			[Out] out INTFS_KEY_TABLE pIntfs);

		#endregion
	}
}