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
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	''' <summary>
	''' A <see cref="BuilderStrategy"/> which is used to help the application startup sequence properly 
	''' initialize the root <see cref="WorkItem"/> when it is created.
	''' </summary>
	Friend Class RootWorkItemInitializationStrategy : Inherits BuilderStrategy
		''' <summary>
		''' The delegate which is called just as the <see cref="WorkItem"/> is finishing initialization.
		''' </summary>
		Public Delegate Sub RootWorkItemInitializationCallback()

		Private callback As RootWorkItemInitializationCallback

		''' <summary>
		''' Creates a new instance of the RootWorkItemInitializationStrategy class using the given
		''' callback method.
		''' </summary>
		''' <param name="callback">The <see cref="RootWorkItemInitializationCallback"/> callback method
		''' to call when the <see cref="WorkItem"/> is finishing its initialization.</param>
		Public Sub New(ByVal callback As RootWorkItemInitializationCallback)
			Me.callback = callback
		End Sub

		''' <summary>
		''' See <see cref="BuilderStrategy.BuildUp"/> for more information.
		''' </summary>
		Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal typeToBuild As Type, ByVal existing As Object, ByVal idToBuild As String) As Object
			Dim wi As WorkItem = TryCast(existing, WorkItem)

			If Not wi Is Nothing AndAlso wi.Parent Is Nothing Then
				callback()
			End If

			Return MyBase.BuildUp(context, typeToBuild, existing, idToBuild)
		End Function
	End Class
End Namespace
