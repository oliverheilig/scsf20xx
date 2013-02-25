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
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Services
	''' <summary>
	''' Provides Cryptography services using the Data Protection API.
	''' </summary>
	Public Class DataProtectionCryptographyService
		Implements ICryptographyService, IConfigurable

		Private entropy As Byte() = Nothing

		Private scope As DataProtectionScope = DataProtectionScope.CurrentUser

		''' <summary>
		''' Encrypts data using the Data Protection API.
		''' </summary>
		''' <param name="plainText">The input data to be protected by using encryption.</param>
		''' <returns>The resulting cipher text.</returns>
		Public Function EncryptSymmetric(ByVal plainText As Byte()) As Byte() Implements ICryptographyService.EncryptSymmetric
			Return ProtectedData.Protect(plainText, entropy, scope)
		End Function

		''' <summary>
		''' Decrypts a cipher text using the Data Protection API.
		''' </summary>
		''' <param name="cipherText">The cipher text for which you want to decrypt.</param>
		''' <returns>The resulting plain text.</returns>
		Public Function DecryptSymmetric(ByVal cipherText As Byte()) As Byte() Implements ICryptographyService.DecryptSymmetric
			Return ProtectedData.Unprotect(cipherText, entropy, scope)
		End Function

#Region "IConfigurable Members"

		''' <summary>
		''' Configures the <see cref="DataProtectionCryptographyService"/> using the
		''' specified settings collection.
		''' </summary>
		''' <param name="settings">A <see cref="NameValueCollection"/> with the setting to use to configure the service.</param>
		Public Sub Configure(ByVal settings As NameValueCollection) Implements IConfigurable.Configure
			Guard.ArgumentNotNull(settings, "settings")

			If (Not String.IsNullOrEmpty(settings("Entropy"))) Then
				entropy = Convert.FromBase64String(settings("Entropy"))
			End If
			If (Not String.IsNullOrEmpty(settings("Scope"))) Then
				scope = CType(System.Enum.Parse(GetType(DataProtectionScope), settings("Scope")), DataProtectionScope)
			End If

			' Remove values from setting for security.
			settings.Clear()
		End Sub

#End Region
	End Class
End Namespace