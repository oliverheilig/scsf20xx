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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.ObjectBuilder


''' <summary>
''' A <see cref="BuilderStrategy"/> that walks the control containment chain looking for child controls that are 
''' either smart parts, placeholders or workspaces, so that they all get 
''' added to the <see cref="WorkItem"/>.
''' </summary>
Public Class ControlSmartPartStrategy : Inherits BuilderStrategy
	Private Function GetWorkItem(ByVal locator As IReadableLocator) As WorkItem
		Return locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
	End Function

	''' <summary>
	''' Walks the control hierarchy and adds the relevant elements to the <see cref="WorkItem"/>.
	''' </summary>
	Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal t As Type, ByVal existing As Object, ByVal id As String) As Object
		If TypeOf existing Is Control Then
			AddControlHierarchy(GetWorkItem(context.Locator), DirectCast(existing, Control))
		End If

		Return MyBase.BuildUp(context, t, existing, id)
	End Function

	''' <summary>
	''' Walks the control hierarchy removing the relevant elements from the <see cref="WorkItem"/>.
	''' </summary>
	Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
		If TypeOf item Is Control Then
			RemoveControlHierarchy(GetWorkItem(context.Locator), TryCast(item, Control))
		End If

		Return MyBase.TearDown(context, item)
	End Function

	Private Sub AddControlHierarchy(ByVal workItem As WorkItem, ByVal control As Control)
		ReplaceIfPlaceHolder(workItem, control)

		For Each child As Control In control.Controls
			If AddControlToWorkItem(workItem, child) = False Then
				AddControlHierarchy(workItem, child)
			End If
		Next child
	End Sub

	Private Sub RemoveControlHierarchy(ByVal workItem As WorkItem, ByVal control As Control)
		If Not control Is Nothing Then
			RemoveNestedControls(workItem, control)
		End If
	End Sub

	Private Function AddControlToWorkItem(ByVal workItem As WorkItem, ByVal control As Control) As Boolean
		If ShouldAddControlToWorkItem(workItem, control) Then
			If control.Name.Length <> 0 Then
				workItem.Items.Add(control, control.Name)
			Else
				workItem.Items.Add(control)
			End If

			Return True
		End If

		Return False
	End Function

	Private Function ShouldAddControlToWorkItem(ByVal workItem As WorkItem, ByVal control As Control) As Boolean
		Return (Not workItem.Items.ContainsObject(control)) AndAlso (IsSmartPart(control) OrElse IsWorkspace(control) OrElse IsPlaceholder(control))
	End Function

	Private Function IsPlaceholder(ByVal control As Control) As Boolean
		Return (TypeOf control Is ISmartPartPlaceholder)
	End Function

	Private Function IsSmartPart(ByVal control As Control) As Boolean
		Return (control.GetType().GetCustomAttributes(GetType(SmartPartAttribute), True).Length > 0)
	End Function

	Private Function IsWorkspace(ByVal control As Control) As Boolean
		Return (TypeOf control Is IWorkspace)
	End Function

	Private Sub RemoveNestedControls(ByVal workItem As WorkItem, ByVal control As Control)
		For Each child As Control In control.Controls
			workItem.Items.Remove(child)
			RemoveNestedControls(workItem, child)
		Next child
	End Sub

	Private Sub ReplaceIfPlaceHolder(ByVal workItem As WorkItem, ByVal control As Control)
		Dim placeholder As ISmartPartPlaceholder = TryCast(control, ISmartPartPlaceholder)

		If Not placeholder Is Nothing Then
			Dim replacement As Control = workItem.Items.Get(Of Control)(placeholder.SmartPartName)

			If Not replacement Is Nothing Then
				placeholder.SmartPart = replacement
			End If
		End If
	End Sub
End Class
