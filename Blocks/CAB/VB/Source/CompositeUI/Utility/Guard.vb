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
Imports System.Globalization

Namespace Utility
	''' <summary>
	''' Common guard clauses
	''' </summary>
	Public Class Guard

		Private Sub New()
		End Sub

		''' <summary>
		''' Checks a string argument to ensure it isn't null or empty
		''' </summary>
		''' <param name="argumentValue">The argument value to check.</param>
		''' <param name="argumentName">The name of the argument.</param>
		Public Shared Sub ArgumentNotNullOrEmptyString(ByVal argumentValue As String, ByVal argumentName As String)
			ArgumentNotNull(argumentValue, argumentName)

			If argumentValue.Length = 0 Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.StringCannotBeEmpty, argumentName))
			End If
		End Sub

		''' <summary>
		''' Checks an argument to ensure it isn't null
		''' </summary>
		''' <param name="argumentValue">The argument value to check.</param>
		''' <param name="argumentName">The name of the argument.</param>
		Public Shared Sub ArgumentNotNull(ByVal argumentValue As Object, ByVal argumentName As String)
			If argumentValue Is Nothing Then
                Throw New ArgumentNullException(argumentName)
			End If
		End Sub

		''' <summary>
		''' Checks an Enum argument to ensure that its value is defined by the specified Enum type.
		''' </summary>
		''' <param name="enumType">The Enum type the value should correspond to.</param>
		''' <param name="value">The value to check for.</param>
		''' <param name="argumentName">The name of the argument holding the value.</param>
		Public Shared Sub EnumValueIsDefined(ByVal enumType As Type, ByVal value As Object, ByVal argumentName As String)
			If System.Enum.IsDefined(enumType, value) = False Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidEnumValue, argumentName, enumType.ToString()))
			End If
		End Sub

		''' <summary>
		''' Verifies that an argument type is assignable from the provided type (meaning
		''' interfaces are implemented, or classes exist in the base class hierarchy).
		''' </summary>
		''' <param name="assignee">The argument type.</param>
		''' <param name="providedType">The type it must be assignable from.</param>
		''' <param name="argumentName">The argument name.</param>
		Public Shared Sub TypeIsAssignableFromType(ByVal assignee As Type, ByVal providedType As Type, ByVal argumentName As String)
			If (Not providedType.IsAssignableFrom(assignee)) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, _
				 My.Resources.TypeNotCompatible, assignee, providedType), argumentName)
			End If
		End Sub

		''' <summary>
		''' Verifies that an argument type implements the provided type.
		''' </summary>
		''' <param name="argumentType">The argument type.</param>
		''' <param name="implementsType">The type it must implement.</param>
		''' <param name="argumentName">The argument name.</param>
		Public Shared Sub TypeImplementsType(ByVal argumentType As Type, ByVal implementsType As Type, ByVal argumentName As String)
			If (Not implementsType.IsAssignableFrom(argumentType)) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, _
							My.Resources.TypeNotCompatible, argumentType, implementsType), argumentName)
			End If
		End Sub

	End Class
End Namespace
