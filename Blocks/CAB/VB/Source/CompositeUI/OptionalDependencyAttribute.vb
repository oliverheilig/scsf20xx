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
''' Base class for dependency attributes that can be made optional.
''' </summary>
Public MustInherit Class OptionalDependencyAttribute
	Inherits ParameterAttribute

	Private innerRequired As Boolean = True

	''' <summary>
	''' Whether the dependency is required. Defaults to true.
	''' </summary>
	Public Property Required() As Boolean
		Get
			Return innerRequired
		End Get
		Set(ByVal value As Boolean)
			innerRequired = value
		End Set
	End Property

	''' <summary>
	''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
	''' </summary>
	Public MustOverride Overrides Function CreateParameter(ByVal memberType As Type) As IParameter
End Class
