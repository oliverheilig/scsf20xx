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
using System.Globalization;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Properties;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
    /// <summary>
    /// Implements the <see cref="IWifiAdapterEnumerator"/> using the Native WiFi API
    /// available in Windows Vista
    /// </summary>
    public class VistaWirelessEnumerator : IWifiAdapterEnumerator
    {
        /// <summary>
        /// Enumerates an returns a list of available wireless adapters on the system.
        /// </summary>
        /// <returns>A list os srtings with the GUID ids of the wireless adapters.</returns>
        public List<string> EnumerateWirelessAdapters()
        {
            List<string> result = new List<string>();

            uint negotiatedVersion = 0;
            uint clientHandle = 0;
            IntPtr pList = IntPtr.Zero;
            uint hreturn = 0;
            try
            {
                hreturn = NativeMethods.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out clientHandle);
                // The service is not active in Vista if there are no wirelessa adapters.
                if (hreturn == NativeMethods.ERROR_SERVICE_NOT_ACTIVE)
                {
                    return result;
                }

                CheckError(hreturn);
            }
            catch (DllNotFoundException dllNotFoundException)
            {
                string fullMessage = string.Format("{0}. {1}", dllNotFoundException.Message, Resources.WlanapiNotFound);
                throw new DllNotFoundException(fullMessage);
            }
            

            try
            {

                hreturn = NativeMethods.WlanEnumInterfaces(clientHandle, IntPtr.Zero, out pList);
                CheckError(hreturn);

                NativeMethods.WLAN_INTERFACE_INFO_LIST list = new NativeMethods.WLAN_INTERFACE_INFO_LIST(pList);

                if (list.NumberOfItems > 0)
                {
                    foreach (NativeMethods.WLAN_INTERFACE_INFO info in list.InterfaceInfo)
                    {
                        result.Add(info.InterfaceGuid.ToString("B").ToUpper(CultureInfo.CurrentCulture));
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                if (pList != IntPtr.Zero)
                {
                    NativeMethods.WlanFreeMemory(pList);
                }
                NativeMethods.WlanCloseHandle(clientHandle, IntPtr.Zero);
            }
            return result;
        }

        private static void CheckError(uint error)
        {
            if (error != 0)
                throw new ConnectionMonitorException(Resources.ErrorEnumeratingWirelessAdapters);
        }
    }
}