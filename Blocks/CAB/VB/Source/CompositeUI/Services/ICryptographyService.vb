'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright © Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================


Imports Microsoft.VisualBasic
Imports System
Namespace Services
	''' <summary>
	''' Service that exposes cryptography methods to encrypt and decrypt data.
	''' </summary>
	Public Interface ICryptographyService
		''' <summary>
		''' Encrypts data using a specified symmetric cryptography provider.
		''' </summary>
		''' <param name="plainText">The input data to encrypt.</param>
		''' <returns>The resulting cipher text.</returns>
		Function EncryptSymmetric(ByVal plainText As Byte()) As Byte()

		''' <summary>
		''' Decrypts a cipher text using a specified symmetric cryptography provider.
		''' </summary>
		''' <param name="cipherText">The cipher text for which you want to decrypt.</param>
		''' <returns>The resulting plain text.</returns>
		Function DecryptSymmetric(ByVal cipherText As Byte()) As Byte()

	End Interface
End Namespace