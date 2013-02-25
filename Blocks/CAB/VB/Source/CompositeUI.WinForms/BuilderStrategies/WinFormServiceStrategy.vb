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
Imports System.Text
Imports Microsoft.Practices.ObjectBuilder


''' <summary>
''' A <see cref="BuilderStrategy"/> that adds services that are needed by Windows Forms 
''' applications into the <see cref="WorkItem"/>.
''' </summary>
Public Class WinFormServiceStrategy : Inherits BuilderStrategy
	''' <summary>
	''' Adds in the needed services for Windows Forms applications.
	''' </summary>
	Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal t As Type, ByVal existing As Object, ByVal id As String) As Object
		Dim workItem As WorkItem = TryCast(existing, WorkItem)

		If Not workItem Is Nothing AndAlso workItem.Services.ContainsLocal(GetType(IControlActivationService)) = False Then
			workItem.Services.Add(Of IControlActivationService)(New ControlActivationService())
		End If

		Return MyBase.BuildUp(context, t, existing, id)
	End Function

End Class

