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

namespace Microsoft.Practices.CompositeUI.Services
{
	/// <summary>
	/// Service that exposes cryptography methods to encrypt and decrypt data.
	/// </summary>
	public interface ICryptographyService
	{
		/// <summary>
		/// Encrypts data using a specified symmetric cryptography provider.
		/// </summary>
		/// <param name="plainText">The input data to encrypt.</param>
		/// <returns>The resulting cipher text.</returns>
		byte[] EncryptSymmetric(byte[] plainText);

		/// <summary>
		/// Decrypts a cipher text using a specified symmetric cryptography provider.
		/// </summary>
		/// <param name="cipherText">The cipher text for which you want to decrypt.</param>
		/// <returns>The resulting plain text.</returns>
		byte[] DecryptSymmetric(byte[] cipherText);

	}
}