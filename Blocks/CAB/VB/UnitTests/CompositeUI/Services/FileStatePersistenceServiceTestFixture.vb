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
Imports System.Globalization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography
Imports Microsoft.Practices.CompositeUI.Services

<TestClass()> _
 Public Class FileStatePersistenceServiceTestFixture
	Private stateID As String = "{0DBF9C49-5D03-4917-B49F-44EB31551E4F}"
	Private filename As String
	Private Shared container As WorkItem

    Public Sub New()
        AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory)

        filename = stateID & ".state"
    End Sub

	<TestInitialize()> _
	Public Sub Setup()
		container = New TestableRootWorkItem()
	End Sub

	<TestCleanup()> _
	Public Sub TearDown()
		If File.Exists(filename) Then
			File.Delete(filename)
		End If
	End Sub

	<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
	Public Sub InvalidCryptographySettingThrowsStatePersistenceException()
		Dim service As FileStatePersistenceService = New FileStatePersistenceService()
		Dim settings As NameValueCollection = New NameValueCollection()
		settings("UseCryptography") = "abc"

		service.Configure(settings)
	End Sub


	<TestMethod()> _
	Public Sub StateIsSerializable()
		Dim state As State = New State(stateID)
		Dim fmt As BinaryFormatter = New BinaryFormatter()
		Using write As FileStream = File.OpenWrite(filename)
			fmt.Serialize(write, state)
		End Using
		Assert.IsTrue(File.Exists(filename))

		Dim restored As State = Nothing
		Using read As FileStream = File.OpenRead(filename)
			restored = CType(fmt.Deserialize(read), State)
		End Using
		Assert.AreEqual(state.ID, restored.ID)
	End Sub


	<TestMethod()> _
	Public Sub CanSaveState()
		Dim state As State = New State(stateID)
		state("somekey") = "somevalue"
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		svc.Save(state)
		Assert.IsTrue(File.Exists(filename))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub CannotSaveNull()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = Nothing
		svc.Save(state)
	End Sub

	<TestMethod()> _
	Public Sub CanLoadState()
		CanSaveState()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = svc.Load(stateID)
		Assert.AreEqual(stateID, state.ID)
		Assert.AreEqual("somevalue", state("somekey"))
	End Sub

	<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
	Public Sub LoadingInexistentState()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = svc.Load("junk")
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveState()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = New State(stateID)
		svc.Save(state)

		svc.Remove(stateID)
		Assert.IsFalse(File.Exists(filename))
	End Sub

	<TestMethod()> _
	Public Sub StateContainsSavedStates()
		Dim state As State = New State(stateID)
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Assert.IsFalse(svc.Contains(stateID), "The storage shouln't have the state persisted yet.")
		svc.Save(state)
		Assert.IsTrue(svc.Contains(stateID), "The storage should have the state persisted now.")
	End Sub

	<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
	Public Sub ThrowsIfStateCannotBeSaved()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = New State(stateID)
		Using sw As StreamWriter = File.CreateText(filename)
			svc.Save(state)
		End Using
	End Sub

	<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
	Public Sub ThrowsIfCannotRemove()
		Dim svc As FileStatePersistenceService = New FileStatePersistenceService()
		Dim state As State = New State(stateID)
		svc.Save(state)
		Using sw As StreamWriter = File.CreateText(filename)
			svc.Remove(stateID)
		End Using
	End Sub


	<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
	Public Sub CryptoFailsWhenCryptoNotAvailable()
		Dim workItem As WorkItem = New TestableRootWorkItem()
		workItem.Services.Remove(GetType(ICryptographyService))

		Dim perSvc As FileStatePersistenceService = New FileStatePersistenceService()
		workItem.Services.Add(GetType(IStatePersistenceService), perSvc)
		Dim settings As NameValueCollection = New NameValueCollection()
		settings("UseCryptography") = "True"
		perSvc.Configure(settings)
		perSvc.Save(New State("junk"))
	End Sub

	<TestMethod()> _
	Public Sub UsesCryptoToSave()
		Dim host As WorkItem = container
		Dim cryptoSvc As ICryptographyService = New DataProtectionCryptographyService()
		host.Services.Add(GetType(ICryptographyService), cryptoSvc)
		Dim perSvc As FileStatePersistenceService = New FileStatePersistenceService()
		host.Services.Add(GetType(IStatePersistenceService), perSvc)
		Dim settings As NameValueCollection = New NameValueCollection()
		settings("UseCryptography") = "True"
		perSvc.Configure(settings)

		Dim id As String = Guid.NewGuid().ToString()
		Dim testState As State = New State(id)
		testState("someValue") = "value"
		perSvc.Save(testState)


		Dim stateData As Byte() = Nothing
		Dim filename As String = String.Format(CultureInfo.InvariantCulture, "{0}.state", id)
		Using stream As FileStream = File.OpenRead(filename)
			Dim cipherData As Byte() = New Byte(CInt(stream.Length) - 1) {}
			stream.Read(cipherData, 0, CInt(stream.Length))
			stateData = ProtectedData.Unprotect(cipherData, Nothing, DataProtectionScope.CurrentUser)
		End Using

		Dim fmt As BinaryFormatter = New BinaryFormatter()
		Dim ms As MemoryStream = New MemoryStream(stateData)
		Dim recovered As State = CType(fmt.Deserialize(ms), State)

		Assert.AreEqual(id, recovered.ID, "The state id is different.")
		Assert.AreEqual("value", recovered("someValue"))

		If File.Exists(filename) Then
			File.Delete(filename)
		End If
	End Sub

	<TestMethod()> _
	Public Sub UsesCryptoToLoad()
		Dim id As String = Guid.NewGuid().ToString()
		Dim testState As State = New State(id)
		testState("someValue") = "value"

		Dim fmt As BinaryFormatter = New BinaryFormatter()
		Dim ms As MemoryStream = New MemoryStream()
		fmt.Serialize(ms, testState)
		Dim cipherData As Byte() = ProtectedData.Protect(ms.GetBuffer(), Nothing, DataProtectionScope.CurrentUser)

		Dim filename As String = String.Format(CultureInfo.InvariantCulture, "{0}.state", id)
		Using stream As FileStream = File.OpenWrite(filename)
			stream.Write(cipherData, 0, cipherData.Length)
		End Using

		Dim host As WorkItem = container
		Dim cryptoSvc As ICryptographyService = New DataProtectionCryptographyService()
		host.Services.Add(GetType(ICryptographyService), cryptoSvc)
		Dim perSvc As FileStatePersistenceService = New FileStatePersistenceService()
		host.Services.Add(GetType(IStatePersistenceService), perSvc)
		Dim settings As NameValueCollection = New NameValueCollection()
		settings("UseCryptography") = "True"
		perSvc.Configure(settings)

		Dim recovered As State = perSvc.Load(id)

		Assert.AreEqual(id, recovered.ID, "The state id is different.")
		Assert.AreEqual("value", recovered("someValue"))

		If File.Exists(filename) Then
			File.Delete(filename)
		End If
	End Sub

	<TestMethod()> _
	Public Sub CanConstructWithFolder()
		Dim basePath As String = Path.GetTempPath()
		Dim tempFile As String = Path.Combine(basePath, String.Format(CultureInfo.InvariantCulture, "{0}.state", stateID))

		Dim service As FileStatePersistenceService = New FileStatePersistenceService(basePath)
		Dim st As State = New State(stateID)

		service.Save(st)

		Assert.AreEqual(basePath, service.BasePath)
		Assert.IsTrue(File.Exists(tempFile))

		File.Delete(tempFile)
	End Sub
End Class
