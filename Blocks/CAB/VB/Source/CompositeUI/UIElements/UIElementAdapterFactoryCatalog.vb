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
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization

Namespace UIElements
	''' <summary>
	''' Catalog that keeps track of each <see cref="IUIElementAdapterFactory"/> that is available.
	''' </summary>
	Public Class UIElementAdapterFactoryCatalog
		Implements IUIElementAdapterFactoryCatalog

		Private innerFactories As List(Of IUIElementAdapterFactory) = New List(Of IUIElementAdapterFactory)()

		''' <summary>
		''' Gets the list that includes each registered <see cref="IUIElementAdapterFactory"/> in the catalog.
		''' </summary>
		Public ReadOnly Property Factories() As IList(Of IUIElementAdapterFactory) Implements IUIElementAdapterFactoryCatalog.Factories
			Get
				Return innerFactories.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' Adds a <see cref="IUIElementAdapterFactory"/> to the catalog.
		''' </summary>
		''' <param name="adapterFactory">The factory to add.</param>
		Public Sub RegisterFactory(ByVal adapterFactory As IUIElementAdapterFactory) Implements IUIElementAdapterFactoryCatalog.RegisterFactory
			Guard.ArgumentNotNull(adapterFactory, "adapterFactory")

			innerFactories.Add(adapterFactory)
		End Sub

		''' <summary>
		''' Retrieves a <see cref="IUIElementAdapterFactory"/> for the given UI Element.
		''' </summary>
		''' <param name="element">The UI Element a factory is to be retrieved for.</param>
		''' <returns>The factory for the UI Element.</returns>
		Public Function GetFactory(ByVal element As Object) As IUIElementAdapterFactory Implements IUIElementAdapterFactoryCatalog.GetFactory
			For Each factory As IUIElementAdapterFactory In factories
				If factory.Supports(element) Then
					Return factory
				End If
			Next factory

			Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.NoRegisteredUIElementFactory, element.GetType().ToString()))
		End Function
	End Class
End Namespace
