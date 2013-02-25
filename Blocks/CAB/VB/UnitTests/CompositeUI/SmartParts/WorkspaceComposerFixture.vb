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
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace Tests.SmartParts
	<TestClass()> _
	Public Class WorkspaceComposerFixture
		Private workspace As MockWorkspace
		Private composer As WorkspaceComposer(Of MockSP, SmartPartInfo)

		<TestInitialize()> _
		Public Sub SetUp()
			workspace = New MockWorkspace()
			composer = New WorkspaceComposer(Of MockSP, SmartPartInfo)(workspace)
		End Sub

		<TestMethod()> _
		Public Sub ShowOnComposerCallsComposedWorkspace()
			composer.Show(New MockSP())

			Assert.AreEqual(1, workspace.OnShowCalls)
		End Sub

		<TestMethod()> _
		Public Sub ShowWithSPICallsComposedWorkspace()
			composer.Show(New MockSP(), New SmartPartInfo())

			Assert.AreEqual(1, workspace.OnShowCalls)
		End Sub

		<TestMethod()> _
		Public Sub ActivateCallsComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)

			composer.Activate(sp)

			Assert.AreEqual(1, workspace.OnActivateCalls)
		End Sub

		<TestMethod()> _
		Public Sub HideCallsComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)

			composer.Hide(sp)

			Assert.AreEqual(1, workspace.OnHideCalls)
		End Sub

		<TestMethod()> _
		Public Sub CloseCallsComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)

			composer.Close(sp)

			Assert.AreEqual(1, workspace.OnCloseCalls)
		End Sub

		<TestMethod()> _
		Public Sub ActivateEventCallsRaiseOnComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)

			composer.Activate(sp)

			Assert.AreEqual(1, workspace.RaiseSmartPartActivatedCalls)
		End Sub

		<TestMethod()> _
		Public Sub ClosingEventCallsRaiseOnComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)

			composer.Close(sp)

			Assert.AreEqual(1, workspace.RaiseSmartPartClosingCalls)
		End Sub

#Region "Utility subroutine for CancellingClosingEventDoesNotCallCloseOnComposedWorkspace()"
		Private Sub HandleSmartPartClosing(ByVal sender As Object, ByVal e As WorkspaceCancelEventArgs)
			e.Cancel = True
		End Sub
#End Region

		<TestMethod()> _
		Public Sub CancellingClosingEventDoesNotCallCloseOnComposedWorkspace()
			Dim sp As MockSP = New MockSP()
			composer.Show(sp)
			AddHandler workspace.SmartPartClosing, AddressOf HandleSmartPartClosing

			composer.Close(sp)

			Assert.AreEqual(0, workspace.OnCloseCalls)
		End Sub

		Private Class MockSP
		End Class

		Private Class MockWorkspace : Implements IComposableWorkspace(Of MockSP, SmartPartInfo)
			Public OnActivateCalls As Integer
			Public OnApplySmartPartInfoCalls As Integer
			Public OnShowCalls As Integer
			Public OnHideCalls As Integer
			Public OnCloseCalls As Integer
			Public RaiseSmartPartActivatedCalls As Integer
			Public RaiseSmartPartClosingCalls As Integer

			Private innerSmartParts As List(Of MockSP) = New List(Of MockSP)()

#Region "IComposedWorkspace<MockSP,SmartPartInfo> Members"

			Public Function ConvertFrom(ByVal source As ISmartPartInfo) As SmartPartInfo Implements IComposableWorkspace(Of MockSP, SmartPartInfo).ConvertFrom
				Return SmartPartInfo.ConvertTo(Of SmartPartInfo)(source)
			End Function

			Public Sub OnActivate(ByVal smartPart As MockSP) Implements IComposableWorkspace(Of WorkspaceComposerFixture.MockSP, SmartPartInfo).OnActivate
				OnActivateCalls += 1
			End Sub

			Public Sub OnApplySmartPartInfo(ByVal smartPart As MockSP, ByVal smartPartInfo As SmartPartInfo) Implements IComposableWorkspace(Of WorkspaceComposerFixture.MockSP, SmartPartInfo).OnApplySmartPartInfo
				OnApplySmartPartInfoCalls += 1
			End Sub

			Public Sub OnShow(ByVal smartPart As MockSP, ByVal smartPartInfo As SmartPartInfo) Implements IComposableWorkspace(Of WorkspaceComposerFixture.MockSP, SmartPartInfo).OnShow
				OnShowCalls += 1
				innerSmartParts.Add(smartPart)
			End Sub

			Public Sub OnHide(ByVal smartPart As MockSP) Implements IComposableWorkspace(Of WorkspaceComposerFixture.MockSP, SmartPartInfo).OnHide
				OnHideCalls += 1
			End Sub

			Public Sub OnClose(ByVal smartPart As MockSP) Implements IComposableWorkspace(Of WorkspaceComposerFixture.MockSP, SmartPartInfo).OnClose
				OnCloseCalls += 1
			End Sub

			Public Sub RaiseSmartPartActivated(ByVal e As WorkspaceEventArgs) Implements IComposableWorkspace(Of MockSP, SmartPartInfo).RaiseSmartPartActivated
				RaiseSmartPartActivatedCalls += 1
				If Not SmartPartActivatedEvent Is Nothing Then
					RaiseEvent SmartPartActivated(Me, e)
				End If
			End Sub

			Public Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs) Implements IComposableWorkspace(Of MockSP, SmartPartInfo).RaiseSmartPartClosing
				RaiseSmartPartClosingCalls += 1
				If Not SmartPartClosingEvent Is Nothing Then
					RaiseEvent SmartPartClosing(Me, e)
				End If
			End Sub

#End Region

#Region "IWorkspace Members"

			Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IWorkspace.SmartPartClosing

			Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IWorkspace.SmartPartActivated

			Public Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.Show
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub Show(ByVal smartPart As Object) Implements IWorkspace.Show
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub Hide(ByVal smartPart As Object) Implements IWorkspace.Hide
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub Close(ByVal smartPart As Object) Implements IWorkspace.Close
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub Activate(ByVal smartPart As Object) Implements IWorkspace.Activate
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub ApplySmartPartInfo(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.ApplySmartPartInfo
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object) Implements IWorkspace.SmartParts
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public ReadOnly Property ActiveSmartPart() As Object Implements IWorkspace.ActiveSmartPart
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

#End Region
		End Class
	End Class
End Namespace
