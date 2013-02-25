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
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.ComponentModel

Namespace SmartParts
	''' <summary>
	''' Default base implementation of the <see cref="IWorkspace"/> interface.
	''' </summary>
	''' <remarks>
	''' Implements the common behavior for Workspaces, which includes activating the smart part 
	''' when it is shown, retrieving registered <see cref="ISmartPartInfo"/> components from the 
	''' <see cref="WorkItem"/>, and raising the 
	''' appropriate events when needed.
	''' </remarks>
	''' <typeparam name="TSmartPart">Type of smart parts that the workspace manages.</typeparam>
	''' <typeparam name="TSmartPartInfo">Type of smart part information used by the workspace. 
	''' Must implement <see cref="ISmartPartInfo"/>.</typeparam>
	Public MustInherit Class Workspace(Of TSmartPart, TSmartPartInfo As {ISmartPartInfo, New})
		Implements IWorkspace

		Private innerWorkItem As WorkItem
		Protected innerActiveSmartPart As Object
		Private containedSmartParts As List(Of TSmartPart) = New List(Of TSmartPart)()

		''' <summary>
		''' Dependency injection setter property to get the <see cref="WorkItem"/> where the 
		''' object is contained.
		''' </summary>
		<ServiceDependency()> _
		Public WriteOnly Property WorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerWorkItem = value
			End Set
		End Property

#Region "IWorkspace Members"

		''' <summary>
		''' See <see cref="IWorkspace.SmartPartClosing"/>.
		''' </summary>
		Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IWorkspace.SmartPartClosing

		''' <summary>
		''' See <see cref="IWorkspace.SmartPartActivated"/>.
		''' </summary>
		Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IWorkspace.SmartPartActivated


		''' <summary>
		''' The active smart part in the Workspace, or <c>null</c> (<c>Nothing</c> in Visual Basic .NET).
		''' </summary>
		<Browsable(False)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ActiveSmartPart() As Object Implements IWorkspace.ActiveSmartPart
			Get
				Return innerActiveSmartPart
			End Get
		End Property

		''' <summary>
		''' Allows a derived class to set the Active SmartPart in the Workspace
		''' </summary>
		''' <remarks>
		''' If a derived class is directly setting the value, it must be 
		''' of a supported type, and must have been shown previously.
		''' </remarks>
		''' <param name="value">The SmartPart is set as the <see cref="ActiveSmartPart"/></param>
		Protected Sub SetActiveSmartPart(ByVal value As Object)
			If Not value Is Nothing Then
				ThrowIfUnsupportedSP(value)
				ThrowIfSmartPartNotShownPreviously(DirectCast(value, TSmartPart))
			End If
			innerActiveSmartPart = value
		End Sub

#Region "Utility code for the property SmartParts"
		Public Function ConvertSmartPart(ByVal smartPart As TSmartPart) As Object
			Return CObj(smartPart)
		End Function
#End Region

		''' <summary>
		''' The collection of smart parts contained in the workspace.
		''' </summary>
		<Browsable(False)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object) Implements IWorkspace.SmartParts
			Get
				Return New ReadOnlyCollection(Of Object)(containedSmartParts.ConvertAll(Of Object)(AddressOf ConvertSmartPart))
			End Get
		End Property

		''' <summary>
		''' Activates the smartPart on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to activate.</param>
		''' <exception cref="ArgumentException">The smartPart
		''' was not previously shown in the workspace.</exception>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		''' <remarks>
		''' <see cref="OnActivate"/> and <see cref="SmartPartActivated"/> 
		''' wil only be called if the smartPart is different than the 
		''' <see cref="ActiveSmartPart"/>.
		''' </remarks>
		Public Sub Activate(ByVal smartPart As Object) Implements IWorkspace.Activate
			Guard.ArgumentNotNull(smartPart, "smartPart")
			ThrowIfUnsupportedSP(smartPart)
			ThrowIfSmartPartNotShownPreviously(CType(smartPart, TSmartPart))

			If Not innerActiveSmartPart Is smartPart Then
				OnActivate(CType(smartPart, TSmartPart))
				innerActiveSmartPart = smartPart
				RaiseSmartPartActivated(smartPart)
			End If
		End Sub

		''' <summary>
		''' Applies the smartPartInfo to the smartPart.
		''' </summary>
		''' <exception cref="ArgumentException">The smartPart
		''' was not previously shown in the workspace.</exception>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		''' <remarks>
		''' Applying the smartPartInfo does not cause automatic activation 
		''' of the smartPart.
		''' </remarks>
		Public Sub ApplySmartPartInfo(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.ApplySmartPartInfo
			Guard.ArgumentNotNull(smartPart, "smartPart")
			Guard.ArgumentNotNull(smartPartInfo, "smartPartInfo")
			ThrowIfUnsupportedSP(smartPart)
			ThrowIfSmartPartNotShownPreviously(CType(smartPart, TSmartPart))

			Dim typedInfo As TSmartPartInfo = GetSupportedSPI(smartPartInfo)

			OnApplySmartPartInfo(CType(smartPart, TSmartPart), typedInfo)
		End Sub

		''' <summary>
		''' Shows SmartPart using the given <see cref="ISmartPartInfo"/>.
		''' </summary>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		''' <remarks>
		''' If the smartPart was previously shown, 
		''' <see cref="ApplySmartPartInfo"/> and <see cref="Activate"/> will be called. 
		''' Otherwise, <see cref="OnShow"/> is called.
		''' </remarks>
		Public Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.Show
			Guard.ArgumentNotNull(smartPart, "smartPart")
			Guard.ArgumentNotNull(smartPartInfo, "smartPartInfo")
			ThrowIfUnsupportedSP(smartPart)

			Dim typedInfo As TSmartPartInfo = GetSupportedSPI(smartPartInfo)
			Dim typedSmartPart As TSmartPart = CType(smartPart, TSmartPart)

			If containedSmartParts.Contains(typedSmartPart) Then
				ApplySmartPartInfo(smartPart, smartPartInfo)
				Activate(smartPart)
			Else
				containedSmartParts.Add(typedSmartPart)
				OnShow(typedSmartPart, typedInfo)
			End If
		End Sub

		''' <summary>
		''' Locates an appropriate <see cref="ISmartPartInfo"/> compatible with the type 
		''' TSmartPartInfo, and calls <see cref="Show(object, ISmartPartInfo)"/>.
		''' </summary>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		''' <remarks>
		''' If <see cref="WorkItem"/> is not null, <see cref="Microsoft.Practices.CompositeUI.WorkItem.GetSmartPartInfo{TSmartPartInfo}(object)"/> will 
		''' be called for the TSmartPartInfo concrete type. If no value is returned, 
		''' it will be called again for a generic <see cref="SmartPartInfo"/>. Finally, if no generic info 
		''' is registered either, a new default instance of the TSmartPartInfo will 
		''' be used by calling the <see cref="CreateDefaultSmartPartInfo"/> method and finally passed to 
		''' the <see cref="Show(object, ISmartPartInfo)"/> overload.
		''' </remarks>
		Public Sub Show(ByVal smartPart As Object) Implements IWorkspace.Show
			Guard.ArgumentNotNull(smartPart, "smartPart")
			ThrowIfUnsupportedSP(smartPart)

			Dim typedSmartPart As TSmartPart = CType(smartPart, TSmartPart)

			' Behavior is slightly different than the other overload, as we don't want to 
			' reapply the SPI in this case.
			If containedSmartParts.Contains(typedSmartPart) Then
				Activate(smartPart)
			Else
				Dim info As ISmartPartInfo = Nothing
				Dim provider As ISmartPartInfoProvider = TryCast(smartPart, ISmartPartInfoProvider)

				If Not innerWorkItem Is Nothing Then
					info = innerWorkItem.GetSmartPartInfo(Of TSmartPartInfo)(smartPart)
					If info Is Nothing AndAlso Not provider Is Nothing Then
						info = provider.GetSmartPartInfo(GetType(TSmartPartInfo))
					End If
					If info Is Nothing Then
						info = innerWorkItem.GetSmartPartInfo(Of SmartPartInfo)(smartPart)
					End If
					If info Is Nothing AndAlso Not provider Is Nothing Then
						info = provider.GetSmartPartInfo(GetType(SmartPartInfo))
					End If
				ElseIf Not provider Is Nothing Then
					info = provider.GetSmartPartInfo(GetType(TSmartPartInfo))
					If info Is Nothing Then
						info = provider.GetSmartPartInfo(GetType(SmartPartInfo))
					End If
				End If

				If info Is Nothing Then
					info = CreateDefaultSmartPartInfo(typedSmartPart)
				End If

				Show(typedSmartPart, info)
			End If
		End Sub

		''' <summary>
		''' Hides the smart part and resets (sets to null) the <see cref="ActiveSmartPart"/>.
		''' </summary>
		''' <exception cref="ArgumentException">The smartPart
		''' was not previously shown in the workspace.</exception>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		Public Sub Hide(ByVal smartPart As Object) Implements IWorkspace.Hide
			Guard.ArgumentNotNull(smartPart, "smartPart")
			ThrowIfUnsupportedSP(smartPart)
			ThrowIfSmartPartNotShownPreviously(CType(smartPart, TSmartPart))

			If innerActiveSmartPart Is smartPart Then
				innerActiveSmartPart = Nothing
			End If
			OnHide(CType(smartPart, TSmartPart))
		End Sub

		''' <summary>
		''' Closes the smart part and resets (sets to null) the <see cref="ActiveSmartPart"/>.
		''' </summary>
		''' <exception cref="ArgumentException">The smartPart
		''' was not previously shown in the workspace.</exception>
		''' <exception cref="ArgumentException">The smartPart cannot be 
		''' assigned to TSmartPart.</exception>
		Public Sub Close(ByVal smartPart As Object) Implements IWorkspace.Close
			Guard.ArgumentNotNull(smartPart, "smartPart")
			ThrowIfUnsupportedSP(smartPart)
			ThrowIfSmartPartNotShownPreviously(CType(smartPart, TSmartPart))

			Dim cancelArgs As WorkspaceCancelEventArgs = RaiseSmartPartClosing(smartPart)
			If cancelArgs.Cancel = False Then
				CloseInternal(DirectCast(smartPart, TSmartPart))
			End If
		End Sub

#End Region

#Region "Private methods"

		Private Function GetSupportedSPI(ByVal smartPartInfo As ISmartPartInfo) As TSmartPartInfo
			If GetType(TSmartPartInfo).IsAssignableFrom(CObj(smartPartInfo).GetType()) = False Then
				Return ConvertFrom(smartPartInfo)
			Else
				Return CType(smartPartInfo, TSmartPartInfo)
			End If
		End Function

		Private Sub ThrowIfUnsupportedSP(ByVal smartPart As Object)
			If GetType(TSmartPart).IsAssignableFrom(smartPart.GetType()) = False Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.UnsupportedSmartPartType, smartPart.GetType(), GetType(TSmartPart)), "smartPart")
			End If
		End Sub

		Private Sub ThrowIfSmartPartNotShownPreviously(ByVal smartPart As TSmartPart)
			If containedSmartParts.Contains(smartPart) = False Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.SmartPartNotInWorkspace, smartPart, Me))
			End If
		End Sub

		''' <summary>
		''' Raises the <see cref="SmartPartActivated"/> event.
		''' </summary>
		''' <param name="smartPart">The smart part that was activated.</param>
		Protected Sub RaiseSmartPartActivated(ByVal smartPart As Object)
			If Not Me.SmartPartActivatedEvent Is Nothing Then
				Dim args As WorkspaceEventArgs = New WorkspaceEventArgs(smartPart)
				RaiseEvent SmartPartActivated(Me, args)
			End If
		End Sub

		''' <summary>
		''' Raises the <see cref="SmartPartClosing"/> event.
		''' </summary>
		''' <param name="smartPart">The smart part that is being closed.</param>
		Protected Function RaiseSmartPartClosing(ByVal smartPart As Object) As WorkspaceCancelEventArgs
			Dim cancelArgs As WorkspaceCancelEventArgs = New WorkspaceCancelEventArgs(smartPart)
			RaiseSmartPartClosing(cancelArgs)

			Return cancelArgs
		End Function

		''' <summary>
		''' Raises the <see cref="SmartPartClosing"/> event using the specified 
		''' event argument.
		''' </summary>
		''' <param name="e">The arguments to pass to the event.</param>
		Protected Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs)
			If Not Me.SmartPartClosingEvent Is Nothing Then
				RaiseEvent SmartPartClosing(Me, e)
			End If
		End Sub

#End Region

#Region "Abstract members"

		''' <summary>
		''' When overridden in a derived class, activates the smartPart
		''' on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to activate.</param>
		Protected MustOverride Sub OnActivate(ByVal smartPart As TSmartPart)

		''' <summary>
		''' When overridden in a derived class, applies the smartPartInfo
		''' to the smartPart that lives in the workspace.
		''' </summary>
		Protected MustOverride Sub OnApplySmartPartInfo(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)

		''' <summary>
		''' When overridden in a derived class, shows the smartPart
		''' on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to show.</param>
		''' <param name="smartPartInfo">The information to apply to the smart part.</param>
		Protected MustOverride Sub OnShow(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)

		''' <summary>
		''' When overridden in a derived class, hides the smartPart
		''' on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to hide.</param>
		Protected MustOverride Sub OnHide(ByVal smartPart As TSmartPart)

		''' <summary>
		''' When overridden in a derived class, closes and removes the smartPart
		''' from the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to close.</param>
		Protected MustOverride Sub OnClose(ByVal smartPart As TSmartPart)

#End Region

#Region "Protected Methods"

		''' <summary>
		''' The list of smart parts shown in the workspace.
		''' </summary>
		Protected ReadOnly Property InnerSmartParts() As ICollection(Of TSmartPart)
			Get
				Return containedSmartParts
			End Get
		End Property

		''' <summary>
		''' By default uses the conversion implemented in <see cref="SmartPartInfo.ConvertTo{TSmartPartInfo}"/>. 
		''' A derived class can implement a different conversion scheme.
		''' </summary>
		Protected Overridable Function ConvertFrom(ByVal source As ISmartPartInfo) As TSmartPartInfo
			Return SmartPartInfo.ConvertTo(Of TSmartPartInfo)(source)
		End Function

		''' <summary>
		''' Creates an instance of the TSmartPartInfo to use to show 
		''' the SmartPart when no information has been 
		''' provided. By default creates a new instance of the TSmartPartInfo.
		''' </summary>
		''' <param name="forSmartPart">The smart part to create the default information for.</param>
		''' <returns>A new instance of the information to use to show the smart part.</returns>
		Protected Overridable Function CreateDefaultSmartPartInfo(ByVal forSmartPart As TSmartPart) As TSmartPartInfo
			Return New TSmartPartInfo()
		End Function

		''' <summary>
		''' Closes the smart part without raising the <see cref="SmartPartClosing"/> event.
		''' </summary>
		Protected Sub CloseInternal(ByVal smartPart As TSmartPart)
			OnClose(DirectCast(smartPart, TSmartPart))
			If (Not ActiveSmartPart Is Nothing) AndAlso ActiveSmartPart.Equals(smartPart) Then
				innerActiveSmartPart = Nothing
			End If
			containedSmartParts.Remove(DirectCast(smartPart, TSmartPart))
		End Sub

#End Region
	End Class
End Namespace
