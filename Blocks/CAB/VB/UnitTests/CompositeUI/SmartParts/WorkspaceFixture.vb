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
Imports System.Collections.Generic

Namespace Tests.SmartParts
	<TestClass()> _
	Public Class WorkspaceFixture
		Private Shared workspace As MockWorkspace
		Private Shared workItem As WorkItem

		<TestInitialize()> _
		Public Sub SetUp()
			workItem = New TestableRootWorkItem()
			workspace = workItem.Items.AddNew(Of MockWorkspace)()
		End Sub

#Region "ActiveSmartPart"

		<TestMethod()> _
		Public Sub WorkspaceRemembersActiveSP()
			Dim sp As MockSP = New MockSP()

			workspace.Show(sp)
			workspace.Activate(sp)

			Assert.AreSame(sp, workspace.ActiveSmartPart)
		End Sub

		<TestMethod()> _
		Public Sub DerivedCanSetActiveSmartPart()
			Dim sp1 As MockSP = New MockSP()
			Dim sp2 As MockSP = New MockSP()
			workspace.Show(sp1)
			workspace.Show(sp2)

			workspace.SetActiveSmartPart(sp1)

			Assert.AreSame(sp1, workspace.ActiveSmartPart)
		End Sub

		<TestMethod()> _
		Public Sub DerivedCanSetActiveSmartPartNull()
			Dim sp1 As MockSP = New MockSP()
			Dim sp2 As MockSP = New MockSP()
			workspace.Show(sp1)
			workspace.Show(sp2)

			workspace.SetActiveSmartPart(Nothing)

			Assert.IsNull(workspace.ActiveSmartPart)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfActiveSmartPartSetNotShown()
			Dim sp1 As MockSP = New MockSP()
			workspace.Show(sp1)

			workspace.SetActiveSmartPart(New MockSP())
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfActiveSmartPartNotCompatible()
			Dim sp1 As MockSP = New MockSP()
			workspace.Show(sp1)

			workspace.SetActiveSmartPart(New Object())
		End Sub

#End Region

		<TestMethod()> _
		Public Sub CanEnumerateSPs()
			Dim count As Integer = 0
			Dim sp As MockSP = New MockSP()
			Dim sp1 As MockSP = New MockSP()
			Dim sp2 As MockSP = New MockSP()

			workspace.Show(sp)
			workspace.Show(sp1)
			workspace.Show(sp2)

			For Each mock As MockSP In workspace.SmartParts
				count += 1
			Next mock

			Assert.AreEqual(3, count)
		End Sub

#Region "Activate"

		<TestMethod()> _
		Public Sub ActivateCallsActivateDerived()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)

			workspace.Activate(sp)

			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.AreEqual(1, workspace.ActivateCalls)
			Assert.AreEqual(0, workspace.ApplySPICalls)
		End Sub

#Region "Utility class for EnabledCommandFiresExecutedEvent() and other tests"
		Private Class TestEventHandlerWithFlag
			Public called As Boolean
			Public Sub New(ByVal called As Boolean)
				Me.called = called
			End Sub
			Public Sub Execute(ByVal sender As Object, ByVal e As WorkspaceEventArgs)
				called = True
			End Sub
		End Class
#End Region

		<TestMethod()> _
		Public Sub ActivateFiresActivateEvent()
			Dim eventCalled As Boolean = False
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			Dim testEventHandler As TestEventHandlerWithFlag = New TestEventHandlerWithFlag(eventCalled)
			AddHandler workspace.SmartPartActivated, AddressOf testEventHandler.Execute

			workspace.Activate(sp)

			eventCalled = testEventHandler.called
			Assert.IsTrue(eventCalled)
		End Sub

#Region "Utility class for EnabledCommandFiresExecutedEvent() and other tests"
		Private Class TestEventHandlerWithCounter
			Public count As Integer = 0
			Public Sub Execute(ByVal sender As Object, ByVal e As WorkspaceEventArgs)
				count = count + 1
			End Sub
		End Class
#End Region

		<TestMethod()> _
		Public Sub CallingActivateOnActiveSmartPartDoesNotCallDerivedOrFireEvent()
			Dim eventCalls As Integer = 0
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			Dim testEventHandler As TestEventHandlerWithCounter = New TestEventHandlerWithCounter()
			AddHandler workspace.SmartPartActivated, AddressOf testEventHandler.Execute

			workspace.Activate(sp)
			workspace.Activate(sp)

			eventCalls = testEventHandler.count
			Assert.AreEqual(1, eventCalls)
		End Sub

		<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
		Public Sub ThrowsIfActivateNullSP()
			workspace.Activate(Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfActivateUnsupportedSP()
			workspace.Activate(New Object())
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfActivateSPNotShown()
			workspace.Activate(New MockSP())
		End Sub

#End Region

#Region "ApplySmartPartInfo"

		<TestMethod()> _
		Public Sub ApplyCallsApplySPIDerived()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)

			workspace.ApplySmartPartInfo(sp, New MockSPI())

			Assert.AreEqual(1, workspace.ApplySPICalls)
		End Sub

		<TestMethod()> _
		Public Sub ApplyDoesNotActivate()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)

			workspace.ApplySmartPartInfo(sp, New MockSPI())

			Assert.AreEqual(0, workspace.ActivateCalls)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfApplyNullSP()
			workspace.ApplySmartPartInfo(Nothing, New MockSPI())
		End Sub

		<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
		Public Sub ThrowsIfApplyNullSPI()
			workspace.ApplySmartPartInfo(New MockSP(), Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfApplyUnsupportedSP()
			workspace.ApplySmartPartInfo(New Object(), New MockSPI())
		End Sub

		<TestMethod()> _
		Public Sub CanApplyUnsupportedSPI()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.ApplySmartPartInfo(sp, New SmartPartInfo())
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfApplySPNotShown()
			workspace.ApplySmartPartInfo(New MockSP(), New MockSPI())
		End Sub

#End Region

#Region "Show"

		<TestMethod()> _
		Public Sub ResolveToProviderIfNoWorkItem()
			Dim spi As MockSPI = New MockSPI()
			Dim sp As MockSPProvider = New MockSPProvider(spi)
			Dim ws As MockWorkspace = New MockWorkspace()

			ws.Show(sp)

			Assert.AreSame(spi, ws.LastSPI)
		End Sub

		<TestMethod()> _
		Public Sub ResolveToWorkItemIfConcreteSPIRegistered()
			Dim spprovider As MockSPI = New MockSPI()
			Dim spworkitem As MockSPI = New MockSPI()
			Dim sp As MockSPProvider = New MockSPProvider(spprovider)
			workItem.RegisterSmartPartInfo(sp, spworkitem)

			workspace.Show(sp)

			Assert.AreSame(spworkitem, workspace.LastSPI)
		End Sub

		<TestMethod()> _
		Public Sub ResolveToProviderIfGenericSPIRegistered()
			Dim spprovider As MockSPI = New MockSPI()
			Dim spworkitem As SmartPartInfo = New SmartPartInfo()
			Dim sp As MockSPProvider = New MockSPProvider(spprovider)
			workItem.RegisterSmartPartInfo(sp, spworkitem)

			workspace.Show(sp)

			Assert.AreSame(spprovider, workspace.LastSPI)
		End Sub

		<TestMethod()> _
		Public Sub ResolveToGenericInWorkItem()
			Dim spprovider As SmartPartInfo = New SmartPartInfo("foo", "")
			Dim spworkitem As SmartPartInfo = New SmartPartInfo("bar", "")
			Dim sp As MockSPProvider = New MockSPProvider(spprovider)
			workItem.RegisterSmartPartInfo(sp, spworkitem)

			workspace.Show(sp)

			Assert.AreEqual("bar", workspace.LastSPI.Title)
		End Sub

		<TestMethod()> _
		Public Sub ShowWithGenericSPIRegisteredCallsConvertToConcreteType()
			Dim sp As MockSP = New MockSP()
			workItem.RegisterSmartPartInfo(sp, New SmartPartInfo("foo", "bar"))

			workspace.Show(sp)

			Assert.AreEqual(1, workspace.ConvertCalls)
			Assert.AreEqual("foo", workspace.LastSPI.Title)
			Assert.AreEqual("bar", workspace.LastSPI.Description)
		End Sub

		<TestMethod()> _
		Public Sub ShowWithNoSPIAndNoWorkItemSetCreatesNew()
			Dim ws As MockWorkspace = New MockWorkspace()
			ws.Show(New MockSP())

			Assert.AreEqual(1, ws.ShowCalls)
			Assert.IsNotNull(ws.LastSPI)
		End Sub

		<TestMethod()> _
		Public Sub ShowWithNoConcreteOrGenericSPIAndWorkItemSetCreatesNew()
			workspace.Show(New MockSP())

			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.IsNotNull(workspace.LastSPI)
		End Sub

		<TestMethod()> _
		Public Sub NewSPIForShowCanBeOverriden()
			workspace.Show(New MockSP())

			Assert.IsTrue(workspace.LastSPI.Custom)
		End Sub

		<TestMethod()> _
		Public Sub ShowCallsOnShowDerived()
			workspace.Show(New MockSP())

			Assert.AreEqual(1, workspace.ShowCalls)
		End Sub

		<TestMethod()> _
		Public Sub ShowWithSPICallsShowDerived()
			workspace.Show(New MockSP(), New MockSPI())

			Assert.AreEqual(1, workspace.ShowCalls)
		End Sub

		<TestMethod()> _
		Public Sub ShowTwiceActivatesButNotShowsOrAppliesInfo()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.AreEqual(0, workspace.ActivateCalls)

			workspace.Show(sp)

			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.AreEqual(1, workspace.ActivateCalls)
			Assert.AreEqual(0, workspace.ApplySPICalls)
		End Sub

		<TestMethod()> _
		Public Sub ShowTwiceWithSPIActivatesAndAppliesInfoButNotShows()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.AreEqual(0, workspace.ActivateCalls)

			workspace.Show(sp, New MockSPI())

			Assert.AreEqual(1, workspace.ShowCalls)
			Assert.AreEqual(1, workspace.ActivateCalls)
			Assert.AreEqual(1, workspace.ApplySPICalls)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfShowNullSP()
			workspace.Show(Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
		Public Sub ThrowsIfShowNullSPI()
			workspace.Show(New MockSP(), Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfShowUnsupportedSP()
			workspace.Show(New Object())
		End Sub

		<TestMethod()> _
		Public Sub CanShowWithUnsupportedSPI()
			workspace.Show(New MockSP(), New SmartPartInfo())
		End Sub

#End Region

#Region "Hide"

		<TestMethod()> _
		Public Sub HideResetsActiveSmartPartIfHidingActiveSmartPart()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.Activate(sp)

			workspace.Hide(sp)

			Assert.IsNull(workspace.ActiveSmartPart)
		End Sub

		<TestMethod()> _
		Public Sub HideDoesNotResetsActiveSmartPartIfHidingNonActiveSmartPart()
			Dim sp As MockSP = New MockSP()
			Dim sp1 As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.Show(sp1)
			workspace.Activate(sp1)

			workspace.Hide(sp)

			Assert.AreSame(workspace.ActiveSmartPart, sp1)
		End Sub

		<TestMethod()> _
		Public Sub HideCallsHideDerived()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)

			workspace.Hide(sp)

			Assert.AreEqual(1, workspace.HideCalls)
		End Sub

		<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
		Public Sub ThrowsIfHideNullSP()
			workspace.Hide(Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfHideUnsupportedSP()
			workspace.Hide(New Object())
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfHideSPNotShown()
			workspace.Hide(New MockSP())
		End Sub

#End Region

#Region "Close"

		<TestMethod()> _
		Public Sub CloseRemovesFromSmartPartsDictionary()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.Show(New MockSP())

			workspace.Close(sp)

			Assert.AreEqual(1, workspace.SmartParts.Count)
		End Sub

		<TestMethod()> _
		Public Sub CloseResetsActiveSmartPartIfClosingActiveSmartPart()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.Activate(sp)

			workspace.Close(sp)

			Assert.IsNull(workspace.ActiveSmartPart)
		End Sub

		<TestMethod()> _
		Public Sub CloseDoesNotResetsActiveSmartPartIfClosingNonActiveSmartPart()
			Dim sp As MockSP = New MockSP()
			Dim sp1 As MockSP = New MockSP()
			workspace.Show(sp)
			workspace.Show(sp1)
			workspace.Activate(sp1)

			workspace.Close(sp)

			Assert.IsNotNull(workspace.ActiveSmartPart)
			Assert.AreSame(workspace.ActiveSmartPart, sp1)
		End Sub

		<TestMethod()> _
		Public Sub CloseCallsCloseDerived()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)

			workspace.Close(sp)

			Assert.AreEqual(1, workspace.CloseCalls)
		End Sub

#Region "Utility class for EnabledCommandFiresExecutedEvent() and other tests"
		Private Class TestCancelEventHandlerWithFlag
			Public called As Boolean
			Public Sub New(ByVal called As Boolean)
				Me.called = called
			End Sub
			Public Sub Execute(ByVal sender As Object, ByVal e As WorkspaceCancelEventArgs)
				called = True
			End Sub
		End Class
#End Region

		<TestMethod()> _
		Public Sub CloseFiresSmartPartClosingEvent()
			Dim eventCalled As Boolean = False
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			Dim testEventHandler As TestCancelEventHandlerWithFlag = New TestCancelEventHandlerWithFlag(eventCalled)
			AddHandler workspace.SmartPartClosing, AddressOf testEventHandler.Execute

			workspace.Close(sp)

			eventCalled = testEventHandler.called
			Assert.IsTrue(eventCalled)
		End Sub

#Region "Utility subroutine for CancellingClosingEventDoesNotCallCloseOnComposedWorkspace()"
		Private Sub HandleSmartPartClosing(ByVal sender As Object, ByVal e As WorkspaceCancelEventArgs)
			e.Cancel = True
		End Sub
#End Region

		<TestMethod()> _
		Public Sub DerivedOnCloseCalledIfClosingNotCancelled()
			Dim sp As MockSP = New MockSP()
			workspace.Show(sp)
			AddHandler workspace.SmartPartClosing, AddressOf HandleSmartPartClosing

			workspace.Close(sp)

			Assert.AreEqual(0, workspace.CloseCalls)
		End Sub

		<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
		Public Sub ThrowsIfCloseNullSP()
			workspace.Close(Nothing)
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfCloseUnsupportedSP()
			workspace.Close(New Object())
		End Sub

		<ExpectedException(GetType(ArgumentException)), TestMethod()> _
		Public Sub ThrowsIfCloseSPNotShown()
			workspace.Close(New MockSP())
		End Sub

#End Region



		<TestMethod()> _
		Public Sub SmartPartShowInChildWorkItemParentWorkspaceUsesChildSmartPartInfo()
			Dim workItemA As WorkItem = New TestableRootWorkItem()
			Dim workItemB As WorkItem = workItemA.WorkItems.AddNew(Of WorkItem)()
			Dim sp As MockSP = New MockSP()
			Dim spi As MockSPI = New MockSPI("Title", "Description")

			Dim workspace As MockWorkspace = workItemA.Workspaces.AddNew(Of MockWorkspace)()
			workItemB.RegisterSmartPartInfo(sp, spi)

			workspace.Show(sp)

			Assert.AreSame(spi, workspace.LastSPI)
		End Sub


		<TestMethod()> _
		Public Sub SmartPartInfoIsRemovedWhenChildWorkItemIsDisposed()
			Dim workItemA As WorkItem = New TestableRootWorkItem()
			Dim workItemB As WorkItem = workItemA.WorkItems.AddNew(Of WorkItem)()
			Dim sp As MockSP = New MockSP()
			Dim spi As MockSPI = New MockSPI("Title", "Description")

			Dim workspace As MockWorkspace = workItemA.Workspaces.AddNew(Of MockWorkspace)()
			workItemB.RegisterSmartPartInfo(sp, spi)

			workItemB.Dispose()

			workspace.Show(sp)

			Assert.IsFalse(spi Is workspace.LastSPI)
		End Sub

#Region "Helper classes"

		Private Class MockSPProvider : Inherits MockSP : Implements ISmartPartInfoProvider
			Private spi As ISmartPartInfo

			Public Sub New(ByVal spi As ISmartPartInfo)
				Me.spi = spi
			End Sub

#Region "ISmartPartInfoProvider Members"

			Public Function GetSmartPartInfo(ByVal smartPartInfoType As Type) As ISmartPartInfo Implements ISmartPartInfoProvider.GetSmartPartInfo
				If smartPartInfoType.IsAssignableFrom(CObj(spi).GetType()) Then
					Return spi
				End If

				Return Nothing
			End Function

#End Region
		End Class



		Private Class MockSP
		End Class

		Private Class MockSPI : Implements ISmartPartInfo
			Public Sub New()
			End Sub

			Public Sub New(ByVal aTitle As String, ByVal aDescription As String)
				Me.innerTitle = aTitle
				Me.innerDescription = aDescription
			End Sub

			Private innerDescription As String

			Public Property Description() As String Implements ISmartPartInfo.Description
				Get
					Return innerDescription
				End Get
				Set(ByVal value As String)
					innerDescription = value
				End Set
			End Property

			Private innerTitle As String

			Public Property Title() As String Implements ISmartPartInfo.Title
				Get
					Return innerTitle
				End Get
				Set(ByVal value As String)
					innerTitle = value
				End Set
			End Property

			Private innerCustom As Boolean = False

			Public Property Custom() As Boolean
				Get
					Return innerCustom
				End Get
				Set(ByVal value As Boolean)
					innerCustom = value
				End Set
			End Property
		End Class

		Private Class MockWorkspace : Inherits Workspace(Of MockSP, MockSPI)
			Public ActivateCalls As Integer
			Public ApplySPICalls As Integer
			Public ShowCalls As Integer
			Public HideCalls As Integer
			Public CloseCalls As Integer
			Public ConvertCalls As Integer
			Public LastSPI As MockSPI

			Private activeSP As MockSP

			Private mockSmartParts As List(Of MockSP) = New List(Of MockSP)()

			Protected Overrides Sub OnApplySmartPartInfo(ByVal smartPart As MockSP, ByVal smartPartInfo As MockSPI)
				ApplySPICalls += 1
				LastSPI = smartPartInfo
			End Sub

			Protected Overrides Sub OnActivate(ByVal smartPart As MockSP)
				ActivateCalls += 1
				activeSP = smartPart
			End Sub

			Protected Overrides Sub OnShow(ByVal smartPart As MockSP, ByVal smartPartInfo As MockSPI)
				ShowCalls += 1
				mockSmartParts.Add(smartPart)
				LastSPI = smartPartInfo
			End Sub

			Protected Overrides Sub OnHide(ByVal smartPart As MockSP)
				HideCalls += 1
			End Sub

			Protected Overrides Sub OnClose(ByVal smartPart As MockSP)
				CloseCalls += 1
			End Sub

			Protected Overrides Function ConvertFrom(ByVal source As ISmartPartInfo) As MockSPI
				ConvertCalls += 1
				Return MyBase.ConvertFrom(source)
			End Function

			Overloads Sub SetActiveSmartPart(ByVal smartPart As Object)
				MyBase.SetActiveSmartPart(smartPart)
			End Sub

			Protected Overrides Function CreateDefaultSmartPartInfo(ByVal forSmartPart As MockSP) As MockSPI
				Dim spi As MockSPI = MyBase.CreateDefaultSmartPartInfo(forSmartPart)

				spi.Custom = True

				Return spi
			End Function
		End Class

#End Region
	End Class
End Namespace
