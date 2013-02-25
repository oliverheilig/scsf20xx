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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class DataProtectionCryptographyServiceFixture
		<TestMethod()> _
		Public Sub DataProtectionCryptographyServiceIsAvailable()
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub EncryptFailsIfNullData()
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.EncryptSymmetric(Nothing)
		End Sub


		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub DecryptFailsIfNullData()
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.DecryptSymmetric(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub CanEncryptData()
			Dim userData As Byte() = {0, 1, 2, 3, 4, 1, 2, 3, 4}
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			Dim result As Byte() = svc.EncryptSymmetric(userData)

			Dim restored As Byte() = ProtectedData.Unprotect(result, Nothing, DataProtectionScope.CurrentUser)

			Assert.AreEqual(9, restored.Length, "Restored data is not correct.")
			Dim i As Integer = 0
			Do While i < userData.Length
				Assert.AreEqual(userData(i), restored(i), "The encryption failed.")
				i += 1
			Loop
		End Sub

		<TestMethod()> _
		Public Sub CanDecryptData()
			Dim userData As Byte() = {0, 1, 2, 3, 4, 1, 2, 3, 4}
			Dim cipherData As Byte() = ProtectedData.Protect(userData, Nothing, DataProtectionScope.CurrentUser)

			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			Dim restored As Byte() = svc.DecryptSymmetric(cipherData)

			Assert.AreEqual(9, restored.Length, "Restored data is not correct.")
			Dim i As Integer = 0
			Do While i < userData.Length
				Assert.AreEqual(userData(i), restored(i), "The decryption failed.")
				i += 1
			Loop
		End Sub

		<TestMethod()> _
		Public Sub ServiceCanBeConfigured()
			Dim settings As NameValueCollection = New NameValueCollection()
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.Configure(settings)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfConfiguredWithNullSettings()
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.Configure(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub EncryptsUsingEntropy()
			Dim userData As Byte() = {0, 1, 2, 3, 4, 1, 2, 3, 4}
			Dim entropy As Byte() = {1, 2, 3, 4}
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("Entropy") = Convert.ToBase64String(entropy)
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.Configure(settings)

			Dim cipherData As Byte() = svc.EncryptSymmetric(userData)

			Dim recovered As Byte() = ProtectedData.Unprotect(cipherData, entropy, DataProtectionScope.CurrentUser)
			Assert.AreEqual(Convert.ToBase64String(userData), Convert.ToBase64String(recovered))
		End Sub

		<TestMethod()> _
		Public Sub DecryptUsingEntropy()
			Dim userData As Byte() = {0, 1, 2, 3, 4, 1, 2, 3, 4}
			Dim entropy As Byte() = {1, 2, 3, 4}
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("Entropy") = Convert.ToBase64String(entropy)
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.Configure(settings)
			Dim cipherData As Byte() = ProtectedData.Protect(userData, entropy, DataProtectionScope.CurrentUser)

			Dim recovered As Byte() = svc.DecryptSymmetric(cipherData)

			Assert.AreEqual(Convert.ToBase64String(userData), Convert.ToBase64String(recovered))
		End Sub

		<TestMethod()> _
		Public Sub ServiceClearsSettingsAfterConfigured()
			Dim entropy As Byte() = {1, 2, 3, 4}
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("Entropy") = Convert.ToBase64String(entropy)
			Dim svc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			svc.Configure(settings)

			Assert.AreEqual(0, settings.Count)
		End Sub
	End Class
End Namespace
