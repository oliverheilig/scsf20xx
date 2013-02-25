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
''' A <see cref="BuilderStrategy"/> that hooks up a <see cref="Control"/> with the 
''' <see cref="IControlActivationService"/>.
''' </summary>
Public Class ControlActivationStrategy : Inherits BuilderStrategy
	''' <summary>
	''' Hooks the element if it is a <see cref="Control"/>.
	''' </summary>
	Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal t As Type, ByVal existing As Object, ByVal id As String) As Object
		Dim control As Control = TryCast(existing, Control)

		If Not control Is Nothing Then
			Dim wi As WorkItem = context.Locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
			wi.Services.Get(Of IControlActivationService)(True).ControlAdded(control)
		End If

		Return MyBase.BuildUp(context, t, existing, id)
	End Function

	''' <summary>
	''' Notifies the <see cref="IControlActivationService"/> to remove the <see cref="Control"/>.
	''' </summary>
	Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
		Dim control As Control = TryCast(item, Control)
		If Not control Is Nothing Then
			Dim wi As WorkItem = context.Locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
			wi.Services.Get(Of IControlActivationService)(True).ControlRemoved(control)

		End If
		Return MyBase.TearDown(context, item)
	End Function
End Class
