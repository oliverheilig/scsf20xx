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
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	///	Concrete <see cref="Connection"/> implementation for Network Interface. 
	///	Use this type of connection to detect any connection availability.
	/// </summary>
	public class NicConnection : Connection, IDisposable
	{
		private bool disposed;
		private NetworkInterface adapter;

		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="connectionTypeName">The type name to use for this connection.</param>
		/// <param name="price">The associated price for using this connection.</param>
		public NicConnection(string connectionTypeName, int price)
			: base(connectionTypeName, price)
		{
			NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
			NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
			UpdateConnectedAdapter();
		}

		/// <summary>
		/// Detects a connected <see cref="NetworkInterface"/> adapter. Override this method to 
		/// change the detection strategy.
		/// </summary>
		/// <returns>The first connected <see cref="NetworkInterface"/> found, or <see langworg="null"/>.</returns>
		protected virtual NetworkInterface DetectConnectedAdapter()
		{
			foreach (NetworkInterface theAdapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (theAdapter.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
					theAdapter.OperationalStatus == OperationalStatus.Up)
				{
					return theAdapter;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the current connected <see cref="NetworkInterface"/> adapter.
		/// </summary>
		protected NetworkInterface ConnectedAdapter
		{
			get { return adapter; }
		}

		/// <summary>
		/// Forces the update of the current connected <see cref="NetworkInterface"/> adapter.
		/// </summary>
		protected virtual void UpdateConnectedAdapter()
		{
			adapter = DetectConnectedAdapter();
		}


		private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
		{
			UpdateConnectedAdapter();
			base.RaiseStateChanged(new StateChangedEventArgs(IsConnected));
		}

		private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			UpdateConnectedAdapter();
			base.RaiseStateChanged(new StateChangedEventArgs(IsConnected));
		}

		/// <summary>
		/// Returns <see langword="true"/> if any wireless adapter is able to transmit data; otherwise <see langword="false"/>.
		/// </summary>
		public override bool IsConnected
		{
			get { return ConnectedAdapter == null ? false : ConnectedAdapter.OperationalStatus == OperationalStatus.Up; }
		}

		/// <summary>
		/// Intance destuctor.
		/// </summary>
		~NicConnection()
		{
			Dispose(false);
		}

		/// <summary>
		/// Handles the instance disposal behavior.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
					NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
				}
			}
			disposed = true;
		}


		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or
		///  resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Returns a string containing detailed information about the connection 
		/// </summary>
		public override string GetDetailedInfoString()
		{
			NetworkInterface theAdapter = ConnectedAdapter;
			if (theAdapter != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("Name: {0}\n", theAdapter.Name);
				sb.AppendLine(theAdapter.Description);
				sb.AppendLine(String.Empty.PadLeft(theAdapter.Description.Length, '='));
				sb.AppendFormat("  Interface Type ................................. : {0}\n", theAdapter.NetworkInterfaceType);
				sb.AppendFormat("  Operational Status ............................. : {0}\n", theAdapter.OperationalStatus);
				string versions = String.Empty;
				if (theAdapter.Supports(NetworkInterfaceComponent.IPv4))
				{
					versions = "IPv4";
				}
				if (theAdapter.Supports(NetworkInterfaceComponent.IPv6))
				{
					if (versions.Length > 0)
					{
						versions += " ";
					}
					versions += "IPv6";
				}
				sb.AppendFormat("  IP version ..................................... : {0}\n", versions);

				GetIPAddressesInfoString(theAdapter.GetIPProperties(), sb);

				return sb.ToString();
			}

			return "There's no active connection at this moment.";
		}

		private static void GetIPAddressesInfoString(IPInterfaceProperties adapterProperties, StringBuilder sb)
		{
			IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
			if (dnsServers != null)
			{
				foreach (IPAddress dns in dnsServers)
				{
					sb.AppendFormat("  DNS Servers ............................. : {0}\n",
					                dns.ToString()
						);
				}
			}
			IPAddressInformationCollection anyCast = adapterProperties.AnycastAddresses;
			if (anyCast != null)
			{
				foreach (IPAddressInformation any in anyCast)
				{
					sb.AppendFormat("  Anycast Address .......................... : {0} {1} {2}\n",
					                any.Address,
					                any.IsTransient ? "Transient" : "",
					                any.IsDnsEligible ? "DNS Eligible" : ""
						);
				}
				sb.AppendLine();
			}

			MulticastIPAddressInformationCollection multiCast = adapterProperties.MulticastAddresses;
			if (multiCast != null)
			{
				foreach (IPAddressInformation multi in multiCast)
				{
					sb.AppendFormat("  Multicast Address ....................... : {0} {1} {2}\n",
					                multi.Address,
					                multi.IsTransient ? "Transient" : "",
					                multi.IsDnsEligible ? "DNS Eligible" : ""
						);
				}
				sb.AppendLine();
			}

			UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;
			if (uniCast != null)
			{
				string lifeTimeFormat = "dddd, MMMM dd, yyyy  hh:mm:ss tt";
				foreach (UnicastIPAddressInformation uni in uniCast)
				{
					DateTime when;

					sb.AppendFormat("  Unicast Address ......................... : {0}\n", uni.Address);
					sb.AppendFormat("     Prefix Origin ........................ : {0}\n", uni.PrefixOrigin);
					sb.AppendFormat("     Suffix Origin ........................ : {0}\n", uni.SuffixOrigin);
					sb.AppendFormat("     Duplicate Address Detection .......... : {0}\n",
					                uni.DuplicateAddressDetectionState);

					// Calculate the date and time at the end of the lifetimes.    
					when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressValidLifetime);
					when = when.ToLocalTime();
					sb.AppendFormat("     Valid Life Time ...................... : {0}\n",
					                when.ToString(lifeTimeFormat, CultureInfo.CurrentCulture)
						);
					when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressPreferredLifetime);
					when = when.ToLocalTime();
					sb.AppendFormat("     Preferred life time .................. : {0}\n",
					                when.ToString(lifeTimeFormat, CultureInfo.CurrentCulture)
						);

					when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.DhcpLeaseLifetime);
					when = when.ToLocalTime();
					sb.AppendFormat("     DHCP Leased Life Time ................ : {0}\n",
					                when.ToString(lifeTimeFormat, CultureInfo.CurrentCulture)
						);
				}
				sb.AppendLine();
			}
		}
	}
}