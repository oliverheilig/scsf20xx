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
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography
Imports Microsoft.Practices.CompositeUI.Services

Namespace Tests
	<TestClass()> _
	Public Class StreamStatePersistenceServiceTestFixture
		Private stateID As String = "DummyStateID"

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub InvalidCryptographySettingThrowsStatePersistenceException()
			Dim service As MemoryStreamPersistence = New MemoryStreamPersistence()
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("UseCryptography") = "abc"

			service.Configure(settings)
		End Sub

		<TestMethod()> _
		Public Sub CanSaveAndLoadState()
			Dim state As State = New State(stateID)
			state("somekey") = "somevalue"
			Dim svc As MemoryStreamPersistence = New MemoryStreamPersistence()
			svc.Save(state)

			Dim state2 As State = svc.Load(stateID)

			Assert.AreEqual(state("somekey"), state2("somekey"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub CannotSaveNull()
			Dim svc As MemoryStreamPersistence = New MemoryStreamPersistence()
			svc.Save(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub DoesNotDisposeStreamIfNotToldTo()
			Dim svc As MemoryStreamPersistence = New MemoryStreamPersistence()
			Dim state As State = New State(stateID)
			svc.Save(state)

			Assert.IsTrue(svc.Stream.CanSeek)
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub LoadingInexistentState()
			Dim svc As NonExisingPersistence = New NonExisingPersistence()
			Dim state As State = svc.Load("junk")
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub ThrowsIfStreamNull()
			Dim svc As NullStreamPersistence = New NullStreamPersistence()
			svc.Load(stateID)
		End Sub

		<TestMethod()> _
		Public Sub SaveStateRemovesPreviousState()
			Dim svc As RemovesPersistence = New RemovesPersistence()

			svc.Save(New State(stateID))

			Assert.IsTrue(svc.RemoveCalled)
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub ThrowsIfStateCannotBeSaved()
			Dim svc As LockedFilePersistence = New LockedFilePersistence()
			Dim state As State = New State(stateID)
			svc.Save(state)
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub ThrowsIfCannotRemove()
			Dim svc As LockedFilePersistence = New LockedFilePersistence()
			svc.Remove(stateID)
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub CryptoFailsWhenCryptoNotAvailable()
			Dim svc As MemoryStreamPersistence = New MemoryStreamPersistence()
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("UseCryptography") = "True"
			svc.Configure(settings)
			svc.Save(New State(stateID))
		End Sub

		<TestMethod()> _
		Public Sub UsesCryptoToSave()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim cryptoSvc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			Dim perSvc As MemoryStreamPersistence = New MemoryStreamPersistence()
			container.Services.Add(GetType(ICryptographyService), cryptoSvc)
			container.Services.Add(GetType(IStatePersistenceService), perSvc)
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("UseCryptography") = "True"
			perSvc.Configure(settings)

			Dim id As Guid = Guid.NewGuid()
			Dim testState As State = New State(id.ToString())
			testState("someValue") = "value"
			perSvc.Save(testState)

			Dim stateData As Byte() = Nothing
			perSvc.Stream.Position = 0
			Dim stream As Stream = perSvc.Stream
			Dim cipherData As Byte() = New Byte(CInt(stream.Length) - 1) {}
			stream.Read(cipherData, 0, CInt(stream.Length))
			stateData = ProtectedData.Unprotect(cipherData, Nothing, DataProtectionScope.CurrentUser)

			Dim fmt As BinaryFormatter = New BinaryFormatter()
			Dim ms As MemoryStream = New MemoryStream(stateData)
			Dim recovered As State = CType(fmt.Deserialize(ms), State)

			Assert.AreEqual(id.ToString(), recovered.ID, "The state id is different.")
			Assert.AreEqual("value", recovered("someValue"))
		End Sub

		<TestMethod()> _
		Public Sub UsesCryptoToLoad()
			Dim id As String = "id"
			Dim testState As State = New State(id)
			testState("someValue") = "value"

			Dim fmt As BinaryFormatter = New BinaryFormatter()
			Dim ms As MemoryStream = New MemoryStream()
			fmt.Serialize(ms, testState)
			Dim cipherData As Byte() = ProtectedData.Protect(ms.GetBuffer(), Nothing, DataProtectionScope.CurrentUser)

			Dim stream As MemoryStream = New MemoryStream()
			stream.Write(cipherData, 0, cipherData.Length)

			Dim host As WorkItem = New TestableRootWorkItem()
			Dim cryptoSvc As DataProtectionCryptographyService = New DataProtectionCryptographyService()
			Dim perSvc As MemoryStreamPersistence = New MemoryStreamPersistence()
			perSvc.Stream = stream

			host.Services.Add(GetType(ICryptographyService), cryptoSvc)
			host.Services.Add(GetType(IStatePersistenceService), perSvc)
			Dim settings As NameValueCollection = New NameValueCollection()
			settings("UseCryptography") = "True"
			perSvc.Configure(settings)

			Dim recovered As State = perSvc.Load(id)

			Assert.AreEqual(id, recovered.ID, "The state id is different.")
			Assert.AreEqual("value", recovered("someValue"))
		End Sub

#Region "Supporting Classes"

		Private Class MemoryStreamPersistence
			Inherits StreamStatePersistenceService

			Public Stream As MemoryStream = New MemoryStream()

			Public Overrides Sub RemoveStream(ByVal id As String)
			End Sub

			Public Overrides Function Contains(ByVal id As String) As Boolean
				Return True
			End Function

			Protected Overrides Function GetStream(ByVal id As String) As Stream
				Stream.Position = 0
				Return Stream
			End Function

			Protected Overrides Function GetStream(ByVal id As String, <System.Runtime.InteropServices.Out()> ByRef shouldDispose As Boolean) As Stream
				shouldDispose = False
				Return GetStream(id)
			End Function
		End Class

		Private Class NonExisingPersistence
			Inherits StreamStatePersistenceService

			Public Overrides Sub RemoveStream(ByVal id As String)
			End Sub

			Public Overrides Function Contains(ByVal id As String) As Boolean
				Return False
			End Function

			Protected Overrides Function GetStream(ByVal id As String) As Stream
				Return New MemoryStream()
			End Function
		End Class

		Private Class NullStreamPersistence
			Inherits StreamStatePersistenceService

			Public Overrides Sub RemoveStream(ByVal id As String)
			End Sub

			Public Overrides Function Contains(ByVal id As String) As Boolean
				Return False

			End Function

			Protected Overrides Function GetStream(ByVal id As String) As Stream
				Return Nothing
			End Function
		End Class

		Private Class RemovesPersistence
			Inherits StreamStatePersistenceService

			Public RemoveCalled As Boolean = False

			Public Overrides Sub RemoveStream(ByVal id As String)
				RemoveCalled = True
			End Sub

			Public Overrides Function Contains(ByVal id As String) As Boolean
				Return True
			End Function

			Protected Overrides Function GetStream(ByVal id As String) As Stream
				Return New MemoryStream()
			End Function
		End Class

		Private Class LockedFilePersistence
			Inherits StreamStatePersistenceService

			Public Overrides Sub RemoveStream(ByVal id As String)
				Dim file As String = Path.GetTempFileName()
				Dim locked As Stream = System.IO.File.OpenWrite(file)

				System.IO.File.Delete(file)
			End Sub

			Public Overrides Function Contains(ByVal id As String) As Boolean
				Return False
			End Function

			Protected Overrides Function GetStream(ByVal id As String) As Stream
				Dim file As String = Path.GetTempFileName()
				Dim locked As Stream = System.IO.File.OpenWrite(file)

				Return System.IO.File.OpenRead(file)
			End Function
		End Class

#End Region
	End Class
End Namespace
