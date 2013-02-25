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
Imports System.Threading


<TestClass()> _
Public Class StateElementFixture

	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub ThrowsIfIndexerKeyNull()
		Dim element As MockStateElement = New MockStateElement()
		element(Nothing) = New Object()
	End Sub

	<TestMethod()> _
	Public Sub CanAssignNullValue()
		Dim element As MockStateElement = New MockStateElement()
		Dim contains As Boolean = False
		element("foo") = Nothing
		For Each key As String In element.Keys
			If key = "foo" Then
				contains = True
			End If
		Next key

		Assert.IsTrue(contains)
		Assert.IsNull(element("foo"))
	End Sub

#Region "Utility code for the AssignValueRaisesStateChanged sub"

	Private Class AssignValueRaisesStateChangedUtility
		Private innerRaised As Boolean

		Public Property Raised() As Boolean
			Get
				Return innerRaised
			End Get
			Set(ByVal value As Boolean)
				innerRaised = value
			End Set
		End Property

		Public Sub New(ByVal raised As Boolean)
			innerRaised = raised
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As StateChangedEventArgs)
			innerRaised = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub AssignValueRaisesStateChanged()
		Dim element As MockStateElement = New MockStateElement()
		Dim raised As Boolean = False
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler element.StateChanged, AddressOf utilityInstance.EventHandler
		element("foo") = New Object()

		Assert.IsTrue(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub AssignNullValueRaisesStateChanged()
		Dim element As MockStateElement = New MockStateElement()
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler element.StateChanged, AddressOf utilityInstance.EventHandler

		element("foo") = Nothing

		Assert.IsTrue(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub StateChangedRisedOnParentIfChildElementChanges()
		Dim element As MockStateElement = New MockStateElement()
		Dim parent As MockStateElement = New MockStateElement()
		parent("bar") = element
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler parent.StateChanged, AddressOf utilityInstance.EventHandler

		element("foo") = New Object()

		Assert.IsTrue(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub StateChangedNotRisedAfterChildElementIsRemoved()
		Dim element As MockStateElement = New MockStateElement()
		Dim parent As MockStateElement = New MockStateElement()
		parent("bar") = element
		parent("bar") = Nothing
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler parent.StateChanged, AddressOf utilityInstance.EventHandler

		element("foo") = New Object()

		Assert.IsFalse(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub MultiLevelStateChangedRisedOnParentIfChildElementChanges()
		Dim element As MockStateElement = New MockStateElement()
		Dim parent As MockStateElement = New MockStateElement()
		Dim grandparent As MockStateElement = New MockStateElement()
		grandparent("foo") = parent
		parent("bar") = element
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler grandparent.StateChanged, AddressOf utilityInstance.EventHandler

		element("foo") = New Object()

		Assert.IsTrue(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub MultiLevelStateChangedNotRisedAfterChildElementIsRemoved()
		Dim element As MockStateElement = New MockStateElement()
		Dim parent As MockStateElement = New MockStateElement()
		Dim grandparent As MockStateElement = New MockStateElement()
		grandparent("foo") = parent
		parent("bar") = element
		grandparent("foo") = Nothing
		Dim utilityInstance As AssignValueRaisesStateChangedUtility = New AssignValueRaisesStateChangedUtility(False)
		AddHandler grandparent.StateChanged, AddressOf utilityInstance.EventHandler

		element("foo") = New Object()

		Assert.IsFalse(utilityInstance.Raised)
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveValue()
		Dim element As MockStateElement = New MockStateElement()
		element("foo") = New Object()

		element.Remove("foo")

		Assert.IsNull(element("foo"))
	End Sub

#Region "Utility code for the ChangedEventAttachedOnlyOnceIfMultipleAdds method"

	Private Class ChangedEventAttachedOnlyOnceIfMultipleAddsUtility

		Private innerCounter As Integer

		Public Property Counter() As Integer
			Get
				Return innerCounter
			End Get
			Set(ByVal value As Integer)
				innerCounter = value
			End Set
		End Property

		Public Sub New(ByVal counter As Integer)
			innerCounter = counter
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As StateChangedEventArgs)
			innerCounter += 1
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub ChangedEventAttachedOnlyOnceIfMultipleAdds()
		Dim element As MockStateElement = New MockStateElement()
		Dim obj As MockChangedElement = New MockChangedElement()
		obj.Value = "old value"

		element("Test") = obj

		Dim temp As MockChangedElement = CType(element("Test"), MockChangedElement)
		temp.Value = "new value"

		element("Test") = temp

		Dim utilityInstance As ChangedEventAttachedOnlyOnceIfMultipleAddsUtility = New ChangedEventAttachedOnlyOnceIfMultipleAddsUtility(0)
		AddHandler element.StateChanged, AddressOf utilityInstance.EventHandler

		temp = CType(element("Test"), MockChangedElement)
		temp.Value = "final value"
		Assert.AreEqual(1, utilityInstance.Counter)
	End Sub

#Region "Utility code for the CustomChangeNotificationElementRaisesStateChangedEvent function"

	Private Class CustomChangeNotificationElementRaisesStateChangedEventUtility

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
	Public Sub CustomChangeNotificationElementRaisesStateChangedEvent()
		Dim element As MockStateElement = New MockStateElement()
		Dim item As MockChangedElement = New MockChangedElement()
		element("foo") = item
		Dim utilityInstance As CustomChangeNotificationElementRaisesStateChangedEventUtility = _
			New CustomChangeNotificationElementRaisesStateChangedEventUtility(False)
		AddHandler element.StateChanged, AddressOf utilityInstance.EventHandler

		item.Value = "Hello"

		Assert.IsTrue(utilityInstance.Changed)
	End Sub

#Region "Helper classes"

	Private Class MockChangedElement
		Implements IChangeNotification

#Region "IChangeNotification Members"

		Public Event Changed As EventHandler Implements IChangeNotification.Changed

#End Region

		Private innerValue As String

		Public Property Value() As String
			Get
				Return Me.innerValue
			End Get
			Set(ByVal value As String)
				Me.innerValue = value
				If Not ChangedEvent Is Nothing Then
					RaiseEvent Changed(Me, EventArgs.Empty)
				End If
			End Set
		End Property

	End Class

	Private Class MockStateElement : Inherits StateElement
		Default Public Shadows Property Item(ByVal key As String) As Object
			Get
				Return MyBase.Item(key)
			End Get
			Set(ByVal value As Object)
				MyBase.Item(key) = value
			End Set
		End Property
	End Class

#End Region
End Class
