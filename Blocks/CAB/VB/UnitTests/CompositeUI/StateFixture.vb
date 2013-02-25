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
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.Practices.CompositeUI.EventBroker


<TestClass()> _
Public Class StateFixture
	<TestMethod()> _
	Public Sub StateSerializable()
		Dim st As State = New State()
		st("foo") = "Foodle"
		st("info") = New Info("thekey", DateTime.Now)

		Dim mem As MemoryStream = New MemoryStream()
		Dim fmt As BinaryFormatter = New BinaryFormatter()
		fmt.Serialize(mem, st)
	End Sub

#Region "Utility code for the ChangingRootValue method"

	Private Class ChangingRootValueUtility

		Private innerChanged As Boolean

		Public Property Changed() As Boolean
			Get
				Return innerChanged
			End Get
			Set(ByVal value As Boolean)
				innerChanged = value
			End Set
		End Property

		Public Sub New(ByVal changed As Boolean)
			innerChanged = changed
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As StateChangedEventArgs)
			innerChanged = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub ChangingRootValue()
		Dim s As State = New State()
		Dim changed As Boolean = False
		Dim utilityInstance As ChangingRootValueUtility = New ChangingRootValueUtility(False)
		AddHandler s.StateChanged, AddressOf utilityInstance.EventHandler
		s("Name") = "kzu"
		Assert.IsTrue(utilityInstance.Changed)
	End Sub

	<TestMethod()> _
	Public Sub ChangingChildNoNotification()
		Dim s As State = New State()
		Dim info As Info = New Info("key", New Object())
		s("Complex") = info

		Dim utilityInstance As ChangingRootValueUtility = New ChangingRootValueUtility(False)
		AddHandler s.StateChanged, AddressOf utilityInstance.EventHandler
		' Change complex object.
		info.Key = "changed"
		Assert.IsFalse(utilityInstance.Changed)
	End Sub

	<TestMethod()> _
	Public Sub ChangingChildNotification()
		Dim s As State = New State()
		Dim info As InfoElement = New InfoElement("key", New Object())
		s("Complex") = info

		Dim utilityInstance As ChangingRootValueUtility = New ChangingRootValueUtility(False)
		AddHandler s.StateChanged, AddressOf utilityInstance.EventHandler
		' Change complex object.
		info.Key = "changed"
		Assert.IsTrue(utilityInstance.Changed)
	End Sub

	<TestMethod()> _
	Public Sub StateCanStoreNullValues()
		Dim s As State = New State()
		s("nullvalue") = Nothing
	End Sub

#Region "Utility code for the StateChangedEventHasNewAndOldValues function"

	Private Class StateChangedEventHasNewAndOldValuesUtility

		Private innerEventArgs As StateChangedEventArgs

		Public Property EventArgs() As StateChangedEventArgs
			Get
				Return innerEventArgs
			End Get
			Set(ByVal value As StateChangedEventArgs)
				innerEventArgs = value
			End Set
		End Property

		Public Sub New(ByVal eventArgs As StateChangedEventArgs)
			innerEventArgs = eventArgs
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As StateChangedEventArgs)
			innerEventArgs = args
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub StateChangedEventHasNewAndOldValues()

		Dim s As State = New State()
		Dim utilityInstance As StateChangedEventHasNewAndOldValuesUtility = _
			New StateChangedEventHasNewAndOldValuesUtility(Nothing)
		AddHandler s.StateChanged, AddressOf utilityInstance.EventHandler

		Dim obj1 As Object = New Object()
		s("Test") = obj1
		Dim obj2 As Object = New Object()
		s("Test") = obj2

		Assert.AreSame(obj2, utilityInstance.EventArgs.NewValue)
		Assert.AreSame(obj1, utilityInstance.EventArgs.OldValue)
	End Sub
End Class

Friend Class InfoElement : Inherits StateElement
	Public Sub New(ByVal key As String, ByVal value As Object)
		Me.Key = key
		Me.Value = value
	End Sub

	Public Property Key() As String
		Get
			Return CStr(Me("key"))
		End Get
		Set(ByVal value As String)
			Me("key") = value
		End Set
	End Property

	Public Property Value() As Object
		Get
			Return Me("value")
		End Get
		Set(ByVal value As Object)
			Me("value") = value
		End Set
	End Property
End Class

<Serializable()> _
Friend Class Info
	Public Sub New(ByVal key As String, ByVal value As Object)
		Me.Key = key
		Me.Value = value
	End Sub
	Public Key As String
	Public Value As Object
End Class
