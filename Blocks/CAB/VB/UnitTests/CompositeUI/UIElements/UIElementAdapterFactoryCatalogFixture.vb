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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI.UIElements


<TestClass()> _
Public Class UIElementAdapterFactoryCatalogFixture
	<TestMethod()> _
	Public Sub CanRegisterFactory()
		Dim factory As MockFactory = New MockFactory()
		Dim catalog As UIElementAdapterFactoryCatalog = New UIElementAdapterFactoryCatalog()
		catalog.RegisterFactory(factory)

		Assert.IsTrue(catalog.Factories.Contains(factory))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub RegisteringNullFactoryThrows()
		Dim catalog As UIElementAdapterFactoryCatalog = New UIElementAdapterFactoryCatalog()
		catalog.RegisterFactory(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CanGetRegisteredFactory()
		Dim catalog As UIElementAdapterFactoryCatalog = New UIElementAdapterFactoryCatalog()
		Dim factory As MockFactory = New MockFactory()
		catalog.RegisterFactory(factory)

		Dim uiFactory As IUIElementAdapterFactory = catalog.GetFactory("Foo")

		Assert.AreSame(factory, uiFactory)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub GetFactoryWhenNoAppropriateFactoryRegisteredThrows()
		Dim catalog As UIElementAdapterFactoryCatalog = New UIElementAdapterFactoryCatalog()
		catalog.GetFactory(GetType(Object))
	End Sub

	Private Class MockFactory
		Implements IUIElementAdapterFactory

		Public Function GetAdapter(ByVal managedExtension As Object) As IUIElementAdapter Implements IUIElementAdapterFactory.GetAdapter
			Return New MockManager()
		End Function

		Public Function Supports(ByVal uiElement As Object) As Boolean Implements IUIElementAdapterFactory.Supports
			Return (TypeOf uiElement Is String)
		End Function
	End Class

	Private Class MockManager
		Inherits UIElementAdapter(Of String)

		Public AddCalled As Boolean = False
		Public Strings As List(Of String) = New List(Of String)()

		Protected Overrides Function Add(ByVal uiElement As String) As String
			AddCalled = True
			Strings.Add(uiElement)

			Return uiElement
		End Function

		Protected Overrides Sub Remove(ByVal uiElement As String)
			Strings.Remove(uiElement)
		End Sub

	End Class
End Class
