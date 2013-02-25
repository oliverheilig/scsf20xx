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
Imports Microsoft.Practices.CompositeUI.Configuration
Imports System.Collections.Generic


<TestClass()> _
Public Class CabVisualizerFixture
	<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
	Public Sub InitializingVisualizerTwiceThrowsException()
		Dim visualizer As CabVisualizer = New CabVisualizer()

		visualizer.Initialize(New WorkItem(), New Builder())
		visualizer.Initialize(New WorkItem(), New Builder())
	End Sub

	<TestMethod()> _
	Public Sub VisualizationCanGetVisualizerAsService()
		Dim visualizer As TestableVisualizer = CreateVisualizer()

		Dim visualization As MockVisualization = visualizer.AddNewVisualization(Of MockVisualization)()

		Assert.AreSame(visualizer, visualization.Visualizer)
	End Sub

	<TestMethod()> _
	Public Sub VisualizerLoadsVisualizationsFromConfiguration()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim visualizer As TestableVisualizer = New TestableVisualizer()

		visualizer.AddVisualizersFromConfig = True
		visualizer.Initialize(wi, wi.Builder)

		Assert.AreEqual(1, visualizer.Visualizations.Count)
		For Each vis As Object In visualizer.Visualizations
			Assert.IsTrue(TypeOf vis Is MockVisualization)
		Next vis
	End Sub

	Private Function CreateVisualizer() As TestableVisualizer
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim visualizer As TestableVisualizer = New TestableVisualizer()
		visualizer.Initialize(wi, wi.Builder)

		Return visualizer
	End Function

	Private Class TestableVisualizer : Inherits CabVisualizer
		Public AddVisualizersFromConfig As Boolean = False

		Protected Overrides ReadOnly Property Configuration() As VisualizationElementCollection
			Get
				Dim result As VisualizationElementCollection = New VisualizationElementCollection()

				If AddVisualizersFromConfig Then
					Dim elt As VisualizationElement = New VisualizationElement()
					elt.Type = GetType(MockVisualization)
					result.Add(elt)
				End If

				Return result
			End Get
		End Property

		Public Shadows Function AddNewVisualization(Of TVisualization)() As TVisualization
			Return MyBase.AddNewVisualization(Of TVisualization)()
		End Function

		Public Shadows ReadOnly Property Visualizations() As ICollection(Of Object)
			Get
				Return MyBase.Visualizations
			End Get
		End Property
	End Class

	Private Class MockVisualization
		Public visualizer As IVisualizer

		<InjectionConstructor()> _
		Public Sub New(<ServiceDependency()> ByVal visualizer As IVisualizer)
			Me.visualizer = visualizer
		End Sub
	End Class
End Class

