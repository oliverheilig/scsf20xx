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
Imports Microsoft.Practices.ObjectBuilder
Imports System.Diagnostics

Namespace Instrumentation
	<TestClass()> _
	Public Class TraceSourceAttributeFixture
		<TestMethod()> _
		Public Sub AttributeGetsTraceSourceFromCatalogIfAvailable()
			Dim catalog As TraceSourceCatalogService = New TraceSourceCatalogService()
			Dim locator As Locator = New Locator()
			Dim builder As Builder = New Builder()
			locator.Add(New DependencyResolutionLocatorKey(GetType(ITraceSourceCatalogService), Nothing), catalog)

			Dim mock As MockTracedClass = builder.BuildUp(Of MockTracedClass)(locator, Nothing, Nothing)

			Assert.AreEqual(1, catalog.TraceSources.Count)
			Assert.IsNotNull(mock.TraceSource)
		End Sub

		<TestMethod()> _
		Public Sub AttributeInjectsNullIfNoCatalogAvailable()
			Dim locator As Locator = New Locator()
			Dim builder As Builder = New Builder()

			Dim mock As MockTracedClass = builder.BuildUp(Of MockTracedClass)(locator, Nothing, Nothing)

			Assert.IsNull(mock.TraceSource)
		End Sub

#Region "Helper classes"

		Private Class MockTracedClass
			Private innerTraceSource As TraceSource

			<TraceSource("Foo")> _
			Public Property TraceSource() As TraceSource
				Get
					Return innerTraceSource
				End Get
				Set(ByVal value As TraceSource)
					innerTraceSource = Value
				End Set
			End Property
		End Class

#End Region
	End Class
End Namespace
