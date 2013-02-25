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
Imports System.Collections.Generic
Imports System.IO
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class FileCatalogModuleEnumeratorFixture
		Private Shared fileHelper As TempFileHelper

		<TestInitialize()> _
		Public Sub FixtureSetup()
			fileHelper = New TempFileHelper()
		End Sub

		<TestCleanup()> _
		Public Sub FixtureTearDown()
			Try
				fileHelper.Dispose()
			Catch
			End Try
		End Sub

		<TestMethod()> _
		Public Sub CanCreate()
			Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator()
			Assert.AreEqual(SolutionProfileReader.DefaultCatalogFile, fcEnumerator.CatalogFilePath)
		End Sub

		<TestMethod()> _
		Public Sub CanCreateSpecifyingFile()
			Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator("myCatalog.xml")
			Assert.AreEqual("myCatalog.xml", fcEnumerator.CatalogFilePath)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfNullFile()
			Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowsIfEmptyFile()
			Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator(String.Empty)
		End Sub

		<TestMethod()> _
		Public Sub UnAuthenticatedUserCanLoadModulesWithoutRoles()
			Dim originalPrincipal As IPrincipal = Nothing

			Using resFile As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureWithRoles.xml")
				Try
					originalPrincipal = Thread.CurrentPrincipal
					If Thread.CurrentPrincipal.Identity.IsAuthenticated Then
						Thread.CurrentPrincipal = New GenericPrincipal(WindowsIdentity.GetCurrent(), Nothing)
					End If
					Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator("FileCatalogReaderServiceFixtureWithRoles.xml")
					Dim mInfos As IModuleInfo() = fcEnumerator.EnumerateModules()

					Assert.AreEqual(1, mInfos.Length, "The solution profile does not contains 1 module.")
					Assert.AreEqual("MyAssembly1.dll", mInfos(0).AssemblyFile, "The 1st module is not MyAssembly1.dll")
				Finally
					Thread.CurrentPrincipal = originalPrincipal
				End Try
			End Using
		End Sub

		<TestMethod()> _
		Public Sub AuthenticateUserCanLoadModulesAccordingToHisRoles()
			Dim cachedPrincipal As IPrincipal = Thread.CurrentPrincipal

			Using resFile As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureWithRoles.xml")
				Try
					Dim identity As GenericIdentity = New GenericIdentity("Me")
					Dim principal As GenericPrincipal = New GenericPrincipal(identity, New String() {"Users"})
					Thread.CurrentPrincipal = principal

					Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator("FileCatalogReaderServiceFixtureWithRoles.xml")

					Dim mInfos As IModuleInfo() = fcEnumerator.EnumerateModules()

					Assert.AreEqual(3, mInfos.Length, "The solution profile does not contains 3 modules.")
					Assert.AreEqual("MyAssembly1.dll", mInfos(0).AssemblyFile, "The 1st module is not MyAssembly1.dll")
					Assert.AreEqual("MyAssembly2.dll", mInfos(1).AssemblyFile, "The 2nd module is not MyAssembly2.dll")
					Assert.AreEqual("MyAssembly4.dll", mInfos(2).AssemblyFile, "The 3rd module is not MyAssembly4.dll")
				Finally
					Thread.CurrentPrincipal = cachedPrincipal
				End Try
			End Using
		End Sub

		<TestMethod()> _
		Public Sub CanLoadProfileContainingModuleWithoutUpdateLocation()
			Using resFile As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Dim fcEnumerator As FileCatalogModuleEnumerator = New FileCatalogModuleEnumerator("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Dim mInfos As IModuleInfo() = fcEnumerator.EnumerateModules()
			End Using
		End Sub

		Private Class TempFileHelper : Implements IDisposable
			Private createdFiles As List(Of String) = New List(Of String)()

			Public Function CreateTempFile() As String
				Dim filename As String = Path.GetFileName(Path.GetTempFileName())
				createdFiles.Add(filename)
				Return filename
			End Function

			Public Sub Dispose() Implements IDisposable.Dispose
				Dispose(True)
				GC.SuppressFinalize(Me)
			End Sub

			Protected Sub Dispose(ByVal disposing As Boolean)
				If disposing Then
					For Each filename As String In createdFiles
						If File.Exists(filename) Then
							File.Delete(filename)
						End If
					Next filename
				End If
			End Sub
		End Class
	End Class
End Namespace
