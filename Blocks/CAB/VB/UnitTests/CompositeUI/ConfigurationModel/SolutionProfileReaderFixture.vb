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
Imports Microsoft.Practices.CompositeUI.Configuration.Xsd
Imports System.Reflection

Namespace ConfigurationModel
	<TestClass()> _
	Public Class SolutionProfileReaderFixture
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
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader()
			Assert.AreEqual(SolutionProfileReader.DefaultCatalogFile, slnReader.CatalogFilePath)
		End Sub

		<TestMethod()> _
		Public Sub CanCreateSpecifyingFile()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader("myCatalog.xml")
			Assert.IsTrue(slnReader.CatalogFilePath.EndsWith("myCatalog.xml"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfNullFile()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowsIfEmptyFile()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader(String.Empty)
		End Sub

		<TestMethod(), ExpectedException(GetType(SolutionProfileReaderException))> _
		Public Sub ThrowsIfFileIsNotUnderApplication()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader("\\somemachine\someshare\somefile.xml")
		End Sub

		<TestMethod()> _
		Public Sub CurrentDirectoryNotUsedToCheckFile()
			Dim current As String = Environment.CurrentDirectory
			Try
				Environment.CurrentDirectory = "c:\"
				Dim slnReader As SolutionProfileReader = New SolutionProfileReader("somefile.xml")
			Finally
				Environment.CurrentDirectory = current
			End Try
		End Sub

		<TestMethod(), ExpectedException(GetType(SolutionProfileReaderException))> _
		Public Sub ThrowsIfFileNotFound()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader("NonExistingFile.xml")
			slnReader.ReadProfile()
		End Sub

		<TestMethod(), ExpectedException(GetType(SolutionProfileReaderException))> _
		Public Sub ThrowsIfBadFormedFile()
			Dim filename As String = fileHelper.CreateTempFile()
			File.WriteAllText(filename, "<blah />")
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader(filename)
			slnReader.ReadProfile()
		End Sub

		<TestMethod()> _
		Public Sub UnAuthenticatedUserCanLoadModulesWithoutRoles()
			Dim originalPrincipal As IPrincipal = Nothing

			Using resFile1 As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureWithRoles.xml")
				Try
					originalPrincipal = Thread.CurrentPrincipal
					If Thread.CurrentPrincipal.Identity.IsAuthenticated Then
						Thread.CurrentPrincipal = New GenericPrincipal(WindowsIdentity.GetCurrent(), Nothing)
					End If
					Dim slnReader As SolutionProfileReader = New SolutionProfileReader("FileCatalogReaderServiceFixtureWithRoles.xml")
					Dim profile As SolutionProfileElement = slnReader.ReadProfile()

					Assert.AreEqual(1, profile.Modules.Length, "The solution profile does not contains 1 module.")
					Assert.AreEqual("MyAssembly1.dll", profile.Modules(0).AssemblyFile, "The 1st module is not MyAssembly1.dll")
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

					Dim slnReader As SolutionProfileReader = New SolutionProfileReader("FileCatalogReaderServiceFixtureWithRoles.xml")

					Dim profile As SolutionProfileElement = slnReader.ReadProfile()

					Assert.AreEqual(3, profile.Modules.Length, "The solution profile does not contains 3 modules.")
					Assert.AreEqual("MyAssembly1.dll", profile.Modules(0).AssemblyFile, "The 1st module is not MyAssembly1.dll")
					Assert.AreEqual("MyAssembly2.dll", profile.Modules(1).AssemblyFile, "The 2nd module is not MyAssembly2.dll")
					Assert.AreEqual("MyAssembly4.dll", profile.Modules(2).AssemblyFile, "The 3rd module is not MyAssembly4.dll")
				Finally
					Thread.CurrentPrincipal = cachedPrincipal
				End Try
			End Using
		End Sub

		<TestMethod()> _
		Public Sub CanLoadProfileContainingModuleWithoutUpdateLocation()
			Using resFile As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Dim slnReader As SolutionProfileReader = New SolutionProfileReader("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Dim profile As SolutionProfileElement = slnReader.ReadProfile()
			End Using
		End Sub

		<TestMethod()> _
		Public Sub CatalogExistsReturnsFalseOnInexistentFile()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader("NonExistingFile.xml")
			Assert.IsFalse(File.Exists(slnReader.CatalogFilePath))
		End Sub

		<TestMethod()> _
		Public Sub CatalogExistsReturnsTrueOnExistentFile()
			Using resFile As TestResourceFile = New TestResourceFile("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Dim slnReader As SolutionProfileReader = New SolutionProfileReader("FileCatalogReaderServiceFixtureModuleWithoutUpdateLocation.xml")
				Assert.IsTrue(File.Exists(slnReader.CatalogFilePath))
			End Using
		End Sub

		<TestMethod()> _
		Public Sub DoesNothingIfNoCatalogIsProvidedAndDefaultOneNotExists()
			Dim slnReader As SolutionProfileReader = New SolutionProfileReader()
			Dim profile As SolutionProfileElement = slnReader.ReadProfile()

			'An empty solutionprofile is returned.
			Assert.IsNotNull(profile)
			Assert.IsNotNull(profile.Modules)
			Assert.AreEqual(0, profile.Modules.Length)
		End Sub

		Private Class TempFileHelper
			Implements IDisposable

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
