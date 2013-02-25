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
''' Indicates that property or parameter is a dependency on a service and
''' should be dependency injected when the class is put into a <see cref="WorkItem"/>.
''' </summary>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Parameter)> _
Public NotInheritable Class ServiceDependencyAttribute
	Inherits OptionalDependencyAttribute

	Private innerType As Type

	''' <summary>
	''' Initializes a new instance of the <see cref="ServiceDependencyAttribute"/> class.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Gets or sets the type of the service the property expects.
	''' </summary>
	Public Property Type() As Type
		Get
			Return innerType
		End Get
		Set(ByVal value As Type)
			innerType = value
		End Set
	End Property

	''' <summary>
	''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
	''' </summary>
	Public Overrides Function CreateParameter(ByVal memberType As Type) As IParameter
		If Not innerType Is Nothing Then
			Return New ServiceDependencyParameter(innerType, Required)
		Else
			Return New ServiceDependencyParameter(memberType, Required)
		End If
	End Function

	Private Class ServiceDependencyParameter : Implements IParameter
		Private serviceType As Type
		Private ensureExists As Boolean

		Public Sub New(ByVal serviceType As Type, ByVal ensureExists As Boolean)
			Me.serviceType = serviceType
			Me.ensureExists = ensureExists
		End Sub

		Public Function GetParameterType(ByVal context As IBuilderContext) As Type Implements IParameter.GetParameterType
			Return serviceType
		End Function

		Public Function GetValue(ByVal context As IBuilderContext) As Object Implements IParameter.GetValue
			Dim workItem As WorkItem = CType(context.Locator.Get(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing)), WorkItem)
			Return workItem.Services.Get(serviceType, ensureExists)
		End Function
	End Class
End Class
