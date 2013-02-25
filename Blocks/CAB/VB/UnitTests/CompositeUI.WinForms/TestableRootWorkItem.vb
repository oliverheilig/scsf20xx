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
		Services.AddNew(Of ControlActivationService, IControlActivationService)()
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
		Dim newBuilder As Builder = New Builder()

		newBuilder.Strategies.AddNew(Of WinFormServiceStrategy)(BuilderStage.Initialization)
		newBuilder.Strategies.AddNew(Of EventBrokerStrategy)(BuilderStage.Initialization)
		newBuilder.Strategies.AddNew(Of CommandStrategy)(BuilderStage.Initialization)
		newBuilder.Strategies.AddNew(Of ControlActivationStrategy)(BuilderStage.Initialization)
		newBuilder.Strategies.AddNew(Of ControlSmartPartStrategy)(BuilderStage.Initialization)
		newBuilder.Strategies.AddNew(Of ObjectBuiltNotificationStrategy)(BuilderStage.PostInitialization)

		newBuilder.Policies.SetDefault(Of ObjectBuiltNotificationPolicy)(New ObjectBuiltNotificationPolicy())
		newBuilder.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))

		Return newBuilder
	End Function
End Class
