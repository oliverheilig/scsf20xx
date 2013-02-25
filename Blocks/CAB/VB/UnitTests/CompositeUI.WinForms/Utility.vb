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
Imports Microsoft.Practices.CompositeUI.SmartParts

Public Class Utility
	Public Sub EventHandler(ByVal sender As Object, ByVal args As WorkspaceCancelEventArgs)
		args.Cancel = True
	End Sub
End Class

Public Class FiresSmartPartUtility(Of TEventArgs As WorkspaceEventArgs)
	Private innerSmartPart As Object

	Public Property SmartPart() As Object
		Get
			Return innerSmartPart
		End Get
		Set(ByVal value As Object)
			innerSmartPart = value
		End Set
	End Property

	Public Sub New(ByVal smartPart As Object)
		innerSmartPart = smartPart
	End Sub

	Public Sub EventHandler(ByVal sender As Object, ByVal args As TEventArgs)
		innerSmartPart = args.SmartPart
	End Sub
End Class

Public Class FiresCounterUtility(Of TEventArgs As WorkspaceEventArgs)

	Protected innerActivated As Integer

	Public Property Activated() As Integer
		Get
			Return innerActivated
		End Get
		Set(ByVal value As Integer)
			innerActivated = value
		End Set
	End Property

	Public Sub New(ByVal activated As Integer)
		innerActivated = activated
	End Sub

	Public Sub EventHandler(ByVal sender As Object, ByVal args As TEventArgs)
		innerActivated += 1
	End Sub

End Class

Public Class FiresCounterWithSmartPartCheckUtility(Of TEventArgs As WorkspaceEventArgs)
	Inherits FiresCounterUtility(Of TEventArgs)
	Private innerSmartPart As Object
	Public Property SmartPart() As Object
		Get
			Return innerSmartPart
		End Get
		Set(ByVal value As Object)
			innerSmartPart = value
		End Set
	End Property
	Public Sub New(ByVal activated As Integer, ByVal smartPart As Object)
		MyBase.new(activated)
		Me.innerSmartPart = smartPart
	End Sub
	Public Overloads Sub EventHandler(ByVal sender As Object, ByVal args As TEventArgs)
		innerActivated += 1
		Assert.AreSame(args.SmartPart, innerSmartPart)
	End Sub
End Class

Public Class StateChangedUtility(Of TEventArgs As EventArgs)

	Protected innerStateChanged As Boolean

	Public Property StateChanged() As Boolean
		Get
			Return innerStateChanged
		End Get
		Set(ByVal value As Boolean)
			innerStateChanged = value
		End Set
	End Property

	Public Sub New(ByVal stateChanged As Boolean)
		innerStateChanged = stateChanged
	End Sub

	Public Sub EventHandler(ByVal sender As Object, ByVal args As TEventArgs)
		innerStateChanged = True
	End Sub

End Class

Public Class StateChangedWithSmartPartCheckUtility(Of TEventArgs As WorkspaceEventArgs)
	Inherits StateChangedUtility(Of TEventArgs)

	Private innerSmartPart As Object
	Public Property SmartPart() As Object
		Get
			Return innerSmartPart
		End Get
		Set(ByVal value As Object)
			innerSmartPart = value
		End Set
	End Property
	Public Sub New(ByVal stateChanged As Boolean)
		MyBase.New(stateChanged)
		innerSmartPart = Nothing
	End Sub

	Public Overloads Sub EventHandler(ByVal sender As Object, ByVal args As TEventArgs)
		innerStateChanged = True
		innerSmartPart = args.SmartPart
	End Sub

End Class