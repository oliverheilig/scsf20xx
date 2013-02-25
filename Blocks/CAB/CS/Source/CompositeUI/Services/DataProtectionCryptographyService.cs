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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Microsoft.Practices.CompositeUI.Utility;

namespace Microsoft.Practices.CompositeUI.Services
{
	/// <summary>
	/// Provides Cryptography services using the Data Protection API.
	/// </summary>
	public class DataProtectionCryptographyService : ICryptographyService, IConfigurable
	{
		private byte[] entropy = null;

		private DataProtectionScope scope = DataProtectionScope.CurrentUser;

		/// <summary>
		/// Encrypts data using the Data Protection API.
		/// </summary>
		/// <param name="plainText">The input data to be protected by using encryption.</param>
		/// <returns>The resulting cipher text.</returns>
		public byte[] EncryptSymmetric(byte[] plainText)
		{
			return ProtectedData.Protect(plainText, entropy, scope);
		}

		/// <summary>
		/// Decrypts a cipher text using the Data Protection API.
		/// </summary>
		/// <param name="cipherText">The cipher text for which you want to decrypt.</param>
		/// <returns>The resulting plain text.</returns>
		public byte[] DecryptSymmetric(byte[] cipherText)
		{
			return ProtectedData.Unprotect(cipherText, entropy, scope);
		}

		#region IConfigurable Members

		/// <summary>
		/// Configures the <see cref="DataProtectionCryptographyService"/> using the
		/// specified settings collection.
		/// </summary>
		/// <param name="settings">A <see cref="NameValueCollection"/> with the setting to use to configure the service.</param>
		public void Configure(NameValueCollection settings)
		{
			Guard.ArgumentNotNull(settings, "settings"); 

			if (!String.IsNullOrEmpty(settings["Entropy"]))
			{
				entropy = Convert.FromBase64String(settings["Entropy"]);
			}
			if (!String.IsNullOrEmpty(settings["Scope"]))
			{
				scope = (DataProtectionScope) Enum.Parse(typeof (DataProtectionScope), settings["Scope"]);
			}

			// Remove values from setting for security.
			settings.Clear();
		}

		#endregion
	}
}