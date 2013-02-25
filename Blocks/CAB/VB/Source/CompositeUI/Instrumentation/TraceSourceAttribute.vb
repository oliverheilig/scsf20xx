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
Imports System.Diagnostics
Imports Microsoft.Practices.ObjectBuilder

''' <summary>
''' Indicates that a property or parameter will be dependency injected with a
''' <see cref="TraceSource"/>.
''' </summary>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Parameter, AllowMultiple:=False, Inherited:=True)> _
Public Class TraceSourceAttribute
	Inherits ParameterAttribute

	Private innerSourceName As String

	''' <summary>
	''' Name of the <see cref="TraceSource"/> to get.
	''' </summary>
	Public ReadOnly Property SourceName() As String
		Get
			Return innerSourceName
		End Get
	End Property

	''' <summary>
	''' Creates a new instance of the <see cref="TraceSourceAttribute"/> class.
	''' </summary>
	''' <param name="sourceName">Name of the <see cref="TraceSource"/> to get.</param>
	Public Sub New(ByVal sourceName As String)
		Me.innerSourceName = sourceName
	End Sub

	''' <summary>
	''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
	''' </summary>
	Public Overrides Function CreateParameter(ByVal memberType As System.Type) As IParameter
		Return New TraceSourceParameter(memberType, innerSourceName)
	End Function

	Private Class TraceSourceParameter : Inherits KnownTypeParameter
		Private sourceName As String

		Public Sub New(ByVal memberType As Type, ByVal sourceName As String)
			MyBase.New(memberType)
			Me.sourceName = sourceName
		End Sub

		Public Overrides Function GetValue(ByVal context As IBuilderContext) As Object
			Dim key As DependencyResolutionLocatorKey = New DependencyResolutionLocatorKey(GetType(ITraceSourceCatalogService), Nothing)
			Dim svc As ITraceSourceCatalogService = CType(context.Locator.Get(key), ITraceSourceCatalogService)
			If Not svc Is Nothing Then
				Return svc.GetTraceSource(sourceName)
			Else
				Return Nothing
			End If
		End Function
	End Class
End Class