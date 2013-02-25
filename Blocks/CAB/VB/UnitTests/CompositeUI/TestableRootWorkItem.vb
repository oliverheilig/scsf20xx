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
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.ObjectBuilder

Public Class TestableRootWorkItem : Inherits WorkItem
	Public Sub New()
		InitializeRootWorkItem(CreateBuilder())

		Services.AddNew(Of CommandAdapterMapService, ICommandAdapterMapService)()
		Services.AddNew(Of TraceSourceCatalogService, ITraceSourceCatalogService)()

		TestableAddServices()

		BuildUp()
	End Sub

	Protected Overridable Sub TestableAddServices()
	End Sub

	Public ReadOnly Property Builder() As Builder
		Get
			Return InnerBuilder
		End Get
	End Property

	Public ReadOnly Property Locator() As IReadWriteLocator
		Get
			Return InnerLocator
		End Get
	End Property

	Private Function CreateBuilder() As Builder
		Dim aBuilder As Builder = New Builder()

		aBuilder.Strategies.AddNew(Of EventBrokerStrategy)(BuilderStage.Initialization)
		aBuilder.Strategies.AddNew(Of CommandStrategy)(BuilderStage.Initialization)
		aBuilder.Strategies.AddNew(Of ObjectBuiltNotificationStrategy)(BuilderStage.PostInitialization)

		aBuilder.Policies.SetDefault(Of ObjectBuiltNotificationPolicy)(New ObjectBuiltNotificationPolicy())
		aBuilder.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))

		Return aBuilder
	End Function
End Class
