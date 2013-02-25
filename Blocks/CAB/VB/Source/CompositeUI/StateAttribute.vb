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


''' <summary>
''' Indicates that a property or parameter should be injected with a value from
''' the <see cref="State"/> of the <see cref="WorkItem"/>.
''' </summary>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Parameter, AllowMultiple:=False, Inherited:=True)> _
Public NotInheritable Class StateAttribute : Inherits ParameterAttribute
	Private id As String

	''' <summary>
	''' Initializes a new instance of the <see cref="StateAttribute"/> class. Will
	''' cause dependency injection to select the first item in the <see cref="State"/>
	''' container with a compatible type.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="StateAttribute"/> class using the
	''' provided state ID. This causes the dependency injection to select the item
	''' in the <see cref="State"/> container with the given ID.
	''' </summary>
	Public Sub New(ByVal id As String)
		Me.id = id
	End Sub

	''' <summary>
	''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
	''' </summary>
	Public Overrides Function CreateParameter(ByVal memberType As Type) As IParameter
		Return New StateParameter(memberType, id)
	End Function

	Private Class StateParameter : Inherits KnownTypeParameter
		Private id As String

		Public Sub New(ByVal type As Type, ByVal id As String)
			MyBase.New(type)
			Me.id = id
		End Sub

		Public Overrides Function GetValue(ByVal context As IBuilderContext) As Object
			Dim key As DependencyResolutionLocatorKey = New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing)
			Dim wi As WorkItem = CType(context.Locator.Get(key), WorkItem)

			If Not id Is Nothing Then
				Return wi.State(id)
			End If

			For Each stateID As String In wi.State.Keys
				If type.IsAssignableFrom(wi.State(stateID).GetType()) Then
					Return wi.State(stateID)
				End If
			Next stateID

			Return Nothing
		End Function
	End Class
End Class

