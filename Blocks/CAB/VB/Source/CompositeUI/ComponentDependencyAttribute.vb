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
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder


''' <summary>
''' Indicates that the property or constructor/method parameter is a reference
''' to a component that lives in the parent <see cref="WorkItem"/>s
''' <see cref="WorkItem.Items"/> collection.
''' </summary>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Parameter)> _
Public NotInheritable Class ComponentDependencyAttribute : Inherits OptionalDependencyAttribute
	Private innerCreateIfNotFound As Boolean = False
	Private innerId As String
	Private innerType As Type

	''' <summary>
	''' Initializes a new instance of <see cref="ComponentDependencyAttribute"/> using the
	''' given component ID.
	''' </summary>
	''' <param name="id">The ID of the component. May not be null.</param>
	Public Sub New(ByVal anId As String)
		Guard.ArgumentNotNull(anId, "id")
		innerId = anId
	End Sub

	''' <summary>
	''' The ID of the component to inject.
	''' </summary>
	Public ReadOnly Property ID() As String
		Get
			Return innerId
		End Get
	End Property

	''' <summary>
	''' Optional type of the component to create if not <see cref="CreateIfNotFound"/> is 
	''' true and the component cannot be located in the current container.
	''' </summary>
	Public Property Type() As Type
		Get
			Return innerType
		End Get
		Set(ByVal value As Type)
			innerType = Value
		End Set
	End Property

	''' <summary>
	''' Whether the component should be created automatically if it is not found in the
	''' <see cref="WorkItem"/>. Defaults to false.
	''' </summary>
	Public Property CreateIfNotFound() As Boolean
		Get
			Return innerCreateIfNotFound
		End Get
		Set(ByVal value As Boolean)
			innerCreateIfNotFound = value
		End Set
	End Property

	''' <summary>
	''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
	''' </summary>
	Public Overrides Function CreateParameter(ByVal memberType As Type) As IParameter
		If Not innerType Is Nothing Then
			Return New DependencyParameter(innerType, innerId, Nothing, GetNotPresentBehavior(), SearchMode.Local)
		Else
			Return New DependencyParameter(memberType, innerId, Nothing, GetNotPresentBehavior(), SearchMode.Local)
		End If
	End Function

	Private Function GetNotPresentBehavior() As NotPresentBehavior
		If innerCreateIfNotFound Then
			Return NotPresentBehavior.CreateNew
		End If
		If Required Then
			Return NotPresentBehavior.Throw
		End If
		Return NotPresentBehavior.ReturnNull
	End Function
End Class
