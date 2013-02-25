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
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts


<TestClass()> _
Public Class SmartPartPlaceholderFixture
	Private Shared placeholder As SmartPartPlaceholder

	<TestInitialize()> _
	Public Sub Setup()
		placeholder = New SmartPartPlaceholder()
	End Sub

	<TestMethod()> _
	Public Sub DefaultSmartPartNameIsSettable()
		placeholder.SmartPartName = "TestSP"

		Assert.AreEqual("TestSP", placeholder.SmartPartName)
	End Sub

	<TestMethod()> _
	Public Sub ControlIsVisibleWhenSmartPartSet()
		Dim sp As MockSmartPart = New MockSmartPart()
		sp.Visible = False

		placeholder.SmartPart = sp

		Assert.IsTrue(sp.Visible)
	End Sub

	<TestMethod()> _
	Public Sub ControlDockFillWhenSmartPartSet()
		Dim sp As MockSmartPart = New MockSmartPart()

		placeholder.SmartPart = sp

		Assert.AreEqual(DockStyle.Fill, sp.Dock)
	End Sub

	<TestMethod()> _
	Public Sub PreviousControlsRemovedWhenSmartPartSet()
		Dim sp As MockSmartPart = New MockSmartPart()
		Dim other As Control = New Control()
		placeholder.Controls.Add(other)

		placeholder.SmartPart = sp

		Assert.IsNull(other.Parent)
	End Sub

	<TestMethod()> _
	Public Sub ControlIsContainedInAreaWhenSmartPartSet()
		Dim sp As MockSmartPart = New MockSmartPart()

		placeholder.SmartPart = sp

		Assert.AreSame(sp, placeholder.Controls(0))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSmartPartNotControl()
		placeholder.SmartPart = New NonControlSmartPart()
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfSmartPartIsNull()
		placeholder.SmartPart = Nothing
	End Sub

	<TestMethod()> _
	Public Sub PlaceholderClearsControlsWhenControlAdded()
		placeholder.SmartPart = New MockSmartPart()
		placeholder.SmartPart = New MockSmartPart()

		Assert.AreEqual(1, placeholder.Controls.Count)
	End Sub

	<TestMethod()> _
	Public Sub PlaceholderFiresSmartShownEvent()
		Dim utilityInstance As StateChangedUtility(Of SmartPartPlaceHolderEventArgs) = _
			New StateChangedUtility(Of SmartPartPlaceHolderEventArgs)(False)
		AddHandler placeholder.SmartPartShown, AddressOf utilityInstance.EventHandler

		placeholder.SmartPart = New MockSmartPart()

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

#Region "Utility code for PlaceHolderPassesCorrectSmartPartWhenShown"

	Private Class PlaceHolderPassesCorrectSmartPartWhenShownUtility

		Private innerArgs As Object

		Public Property Args() As Object
			Get
				Return innerArgs
			End Get
			Set(ByVal value As Object)
				innerArgs = value
			End Set
		End Property

		Public Sub New(ByVal args As Object)
			innerArgs = args
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As SmartPartPlaceHolderEventArgs)
			innerArgs = args.SmartPart
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub PlaceHolderPassesCorrectSmartPartWhenShown()
		Dim utilityinstance As PlaceHolderPassesCorrectSmartPartWhenShownUtility = _
			New PlaceHolderPassesCorrectSmartPartWhenShownUtility(Nothing)
		AddHandler placeholder.SmartPartShown, AddressOf utilityinstance.EventHandler
		Dim smartPart As MockSmartPart = New MockSmartPart()

		placeholder.SmartPart = smartPart

		Assert.AreSame(smartPart, utilityinstance.Args)
	End Sub

	<TestMethod()> _
	Public Sub HolderEventArgsContainsSmartPart()
		Dim smartPart As Object = New Object()
		Dim args As SmartPartPlaceHolderEventArgs = New SmartPartPlaceHolderEventArgs(smartPart)

		Assert.IsNotNull(args.SmartPart)
		Assert.AreSame(smartPart, args.SmartPart)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub HolderEventArgsThrowsIfNullSmartPart()
		Dim args As SmartPartPlaceHolderEventArgs = New SmartPartPlaceHolderEventArgs(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub BackGroundIstransparent()
		Assert.AreEqual(Color.Transparent, placeholder.BackColor)
	End Sub

#Region "Supporting classes"

	<SmartPart()> _
	Private Class NonControlSmartPart : Inherits Object
	End Class

	<SmartPart()> _
	Private Class MockSmartPart : Inherits Control
	End Class

#End Region
End Class
